using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace TZUI
{
    public static class LuaScriptGenerator
    {
        public static string PanelTemplatePath
        {
            get
            {
                return Path.Combine(Application.dataPath, "Lua", "UI", "@PanelTemplate");
            }
        }
        public static string WidgetTemplatePath
        {
            get
            {
                return Path.Combine(Application.dataPath, "Lua", "UI", "@WidgetTemplate");
            }
        }

        public static void Generate(UIMaster master)
        {
            if (master.name.EndsWith("Panel") == false)
                throw new System.Exception("没有符合命名规范 UI必须命名为XXXPanel");

            GeneratePanel(master);
            GeneratePanelConfig(master);
            GenerateViews(master);
            GenerateDataBridge(master);
        }

        private static void GeneratePanel(UIMaster master)
        {
            var templateFile = Path.Combine(PanelTemplatePath, "Generated", "#PanelName#.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputFile))
            {
                File.SetAttributes(outputFile, FileAttributes.Normal);
                File.Delete(outputFile);
            }
            var outputContents = templateContents.Replace("#PanelName#", master.name);
            WriteAllText(outputFile, outputContents);
            File.SetAttributes(outputFile, FileAttributes.ReadOnly);
        }

        private static void GeneratePanelConfig(UIMaster master)
        {
            var templateFile = Path.Combine(PanelTemplatePath, "Private", "#PanelName#Config.lua");
            var outputContents = new List<string>(File.ReadAllLines(templateFile));
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            List<string> currentContents = new List<string>();
            if (File.Exists(outputFile))
                currentContents.AddRange(File.ReadAllLines(outputFile));

            for (var i = outputContents.Count - 1; i >= 0; --i)
            {
                outputContents[i] = outputContents[i].Replace("#PanelName#", master.name);
                var line = outputContents[i];
                if (line.StartsWith(master.name) == false)
                    continue;
                var index = currentContents.IndexOf(line);
                if (index > 0)
                {
                    for (var j = 1; j < currentContents.Count - index; ++j)
                    {
                        var current = j + index;
                        if (outputContents[i + j].StartsWith("}"))
                            break;
                        if (outputContents[i + j].Contains("}"))
                            continue;
                        var field = outputContents[i + j].Substring(0, outputContents[i + j].IndexOf("=") - 1);
                        if (currentContents[current].StartsWith(field))
                            outputContents[i + j] = currentContents[current];
                    }
                }

            }

            outputContents.Insert(outputContents.Count - 1, string.Format("{0}Config.{0}{1} = {{}}\n", master.name, "BaseView"));
            foreach (var view in master.GetComponentsInChildren<UIView>(true))
                outputContents.Insert(outputContents.Count - 1, string.Format("{0}Config.{0}{1} = {{}}\n", master.name, view.name));

            outputContents.Insert(outputContents.Count - 1, "");
            GenerateWidgets(master, outputContents, currentContents);
            WriteAllText(outputFile, string.Join("\n", outputContents));
        }

        private static void GenerateViews(UIMaster master)
        {
            GenerateView(master, null);
            foreach (var view in master.GetComponentsInChildren<UIView>(true))
            {
                GenerateView(master, view);
            }
        }

        private static void GenerateView(UIMaster master, UIView view)
        {
            var viewName = master.name + (view == null ? "BaseView" : view.name);
            var templateFile = Path.Combine(PanelTemplatePath, "Generated", "#ViewName#.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#ViewName#", viewName);
            if (File.Exists(outputFile))
            {
                File.SetAttributes(outputFile, FileAttributes.Normal);
                File.Delete(outputFile);
            }
            var rect = view == null ? master.GetComponent<RectTransform>() : view.GetComponent<RectTransform>();

            var outputContents = templateContents.Replace("#ViewName#", viewName).Replace("#PanelName#", master.name);
            outputContents = outputContents.Replace("#ViewRoot#", view == null ? "nil" : (view.LoadMode == UIViewLoadMode.LoadOnUse ? "nil" : "panel.ot." + view.name + "RectTransform"));
            outputContents = outputContents.Replace("#ParentName#", view == null ? "x" : rect.parent.name);
            outputContents = outputContents.Replace("#LocalScale#", view != null ? $"{{x={rect.localScale.x}, y={rect.localScale.y}, z={rect.localScale.z}}}" : "{x=1,y=1,z=1}");
            outputContents = outputContents.Replace("#AnchorMin#", view != null ? $"{{x={rect.anchorMin.x}, y={rect.anchorMin.y}}}" : "{x=0,y=0}");
            outputContents = outputContents.Replace("#AnchorMax#", view != null ? $"{{x={rect.anchorMax.x}, y={rect.anchorMax.y}}}" : "{x=1,y=1}");
            outputContents = outputContents.Replace("#AnchoredPosition#", view != null ? $"{{x={rect.anchoredPosition.x}, y={rect.anchoredPosition.y}}}" : "{x=0,y=0}");
            outputContents = outputContents.Replace("#SizeDelta#", view != null ? $"{{x={rect.sizeDelta.x}, y={rect.sizeDelta.y}}}" : "{x=0,y=0}");

            #region foreach events
            {
                var foreachEvent = new Regex(@"\n([ \t]*)(@foreach EventName@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var removeForeachEvent = new Regex(@"\n--([ \t]*)(@foreach EventName@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var replaceEvent = new Regex(@"\n([^@]*)#EventName#([^@]*\n)", RegexOptions.Compiled);

                while (foreachEvent.IsMatch(outputContents))
                {
                    var events = view == null ? master.Events : view.Events;
                    for (var i = events.Count - 1; i >= 0; --i)
                    {
                        var eventName = events[i];
                        if (string.IsNullOrEmpty(eventName))
                            continue;
                        outputContents = foreachEvent.Replace(outputContents, "\n$1$2$3$4\n$1$3");
                        while (replaceEvent.IsMatch(outputContents))
                            outputContents = replaceEvent.Replace(outputContents, "\n$1" + eventName + "$2");
                    }
                    outputContents = foreachEvent.Replace(outputContents, "\n--$1$2$3$4");
                }
                while (removeForeachEvent.IsMatch(outputContents))
                    outputContents = removeForeachEvent.Replace(outputContents, "");
            }
            #endregion

            #region foreach widgets
            {
                var foreachWidgetType = new Regex(@"\n([ \t]*)(@foreach WidgetType@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var foreachWidget = new Regex(@"\n([ \t]*)(@foreach Widget@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var removeForeachWidgetType = new Regex(@"\n--([ \t]*)(@foreach WidgetType@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var removeForeachWidget = new Regex(@"\n--([ \t]*)(@foreach Widget@ )([^@]*)( @end@)", RegexOptions.Compiled);
                var replaceWidgetType = new Regex(@"\n([^@]*)#WidgetType#([^@]*\n)", RegexOptions.Compiled);
                var replaceWidgetName = new Regex(@"\n([^@]*)#WidgetName#([^@]*\n)", RegexOptions.Compiled);
                var root = view == null ? master.gameObject : view.gameObject;
                while (foreachWidgetType.IsMatch(outputContents))
                {
                    var widgetsTypes = new  List<Type>();
                    var widgets = new List<UIWidget>(root.GetComponentsInChildren<UIWidget>(true));
                    foreach (var widget in widgets)
                    {
                        if (widgetsTypes.Contains(widget.GetType()) == false)
                            widgetsTypes.Add(widget.GetType());
                    }
                    foreach (var type in widgetsTypes)
                    {
                        outputContents = foreachWidgetType.Replace(outputContents, "\n$1$2$3$4\n$1$3");
                        while (replaceWidgetType.IsMatch(outputContents))
                            outputContents = replaceWidgetType.Replace(outputContents, "\n$1" + type.Name + "$2");
                    }
                    outputContents = foreachWidgetType.Replace(outputContents, "\n--$1$2$3$4");
                }
                while (removeForeachWidgetType.IsMatch(outputContents))
                    outputContents = removeForeachWidgetType.Replace(outputContents, "");
                while (foreachWidget.IsMatch(outputContents))
                {
                    var widgets = new List<UIWidget>(root.GetComponentsInChildren<UIWidget>(true));
                    widgets.Reverse();
                    foreach (var widget in widgets)
                    {
                        var parentView = widget.GetComponentInParentHard<UIView>();
                        if (parentView == null || parentView == view)
                        {
                            outputContents = foreachWidget.Replace(outputContents, "\n$1$2$3$4\n$1$3");
                            while (replaceWidgetType.IsMatch(outputContents))
                                outputContents = replaceWidgetType.Replace(outputContents, "\n$1" + widget.GetType().Name + "$2");
                            while (replaceWidgetName.IsMatch(outputContents))
                                outputContents = replaceWidgetName.Replace(outputContents, "\n$1" + widget.name + "$2");
                        }
                    }
                    outputContents = foreachWidget.Replace(outputContents, "\n--$1$2$3$4");
                }
                while (removeForeachWidget.IsMatch(outputContents))
                    outputContents = removeForeachWidget.Replace(outputContents, "");
            }
            #endregion

            WriteAllText(outputFile, outputContents);
            File.SetAttributes(outputFile, FileAttributes.ReadOnly);

            #region private file
            {
                var privateFile = Path.Combine(PanelTemplatePath, "Private", "#ViewName#.lua");
                var privateContents = new List<string>(File.ReadAllLines(privateFile));
                var outputPrivateFile = privateFile.Replace("@PanelTemplate", master.name).Replace("#ViewName#", viewName);
                var currentContents = new List<string>();
                if (File.Exists(outputPrivateFile))
                    currentContents = new List<string>(File.ReadAllLines(outputPrivateFile));

                var usedFuncs = new List<string>();
                for (var i = privateContents.Count - 1; i >= 0; --i)
                {
                    privateContents[i] = privateContents[i].Replace("#ViewName#", viewName).Replace("#PanelName#", master.name);
                    if (privateContents[i].StartsWith("function") == false)
                        continue;
                    var index = currentContents.IndexOf(privateContents[i]);
                    if (index > 0)
                    {
                        usedFuncs.Add(currentContents[index]);
                        var endIndex = index + 1;
                        for (var j = 1; j < currentContents.Count - index; ++j)
                        {
                            var current = j + index;
                            if (currentContents[current].StartsWith("end"))
                            {
                                endIndex = current;
                                break;
                            }
                        }
                        privateContents.InsertRange(i + 1, currentContents.GetRange(index + 1, endIndex - index - 1));
                    }
                }
                var events = view == null ? master.Events : view.Events;
                for (var i = 0; i < events.Count; ++i)
                {
                    var eventFunc = string.Format("function {0}{1}:{2}({3})", master.name, view == null ? "BaseView" : view.name, events[i], string.Empty);
                    var index = currentContents.IndexOf(eventFunc);
                    if (index > 0)
                    {
                        usedFuncs.Add(currentContents[index]);
                        var endIndex = index + 1;
                        for (var j = 1; j < currentContents.Count - index; ++j)
                        {
                            var current = j + index;
                            if (currentContents[current].StartsWith("end"))
                            {
                                endIndex = current;
                                break;
                            }
                        }
                        privateContents.InsertRange(privateContents.Count - 1, currentContents.GetRange(index, endIndex - index + 1));
                    }
                    else
                    {
                        privateContents.Insert(privateContents.Count - 1, eventFunc);
                        privateContents.Insert(privateContents.Count - 1, "end");
                    }
                    privateContents.Insert(privateContents.Count - 1, "");
                }
                for (var i = 0; i < currentContents.Count; ++i)
                {
                    if (currentContents[i].StartsWith("function") == false)
                        continue;
                    if (usedFuncs.Contains(currentContents[i]))
                        continue;
                    var startIndex = i;
                    var endIndex = i + 1;
                    for (var j = i - 1; j >= 0; --j)
                    {
                        if (currentContents[j].StartsWith("--") == false)
                        {
                            startIndex = j;
                            break;
                        }
                    }
                    for (var j = i + 1; j < currentContents.Count; ++j)
                    {
                        if (currentContents[j].StartsWith("end"))
                        {
                            endIndex = j;
                            break;
                        }
                    }
                    privateContents.InsertRange(privateContents.Count - 1, currentContents.GetRange(startIndex, endIndex - startIndex + 1));
                }
                privateContents.Insert(privateContents.Count - 1, "");
                if (File.Exists(outputPrivateFile))
                    File.Delete(outputPrivateFile);
                WriteAllText(outputPrivateFile, string.Join("\n", privateContents));
            }
            #endregion
        }

        private static void GenerateDataBridge(UIMaster master)
        {
            var templateFile = Path.Combine(PanelTemplatePath, "Generated", "#PanelName#DataBridge.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputFile))
            {
                File.SetAttributes(outputFile, FileAttributes.Normal);
                File.Delete(outputFile);
            }
            var outputContents = templateContents.Replace("#PanelName#", master.name);
            WriteAllText(outputFile, outputContents);
            File.SetAttributes(outputFile, FileAttributes.ReadOnly);

            var privateFile = Path.Combine(PanelTemplatePath, "Private", "#PanelName#DataBridge.lua");
            var privateContents = File.ReadAllText(privateFile);
            var outputPrivateFile = privateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputPrivateFile) == false)
            {
                var outputPrivateContents = privateContents.Replace("#PanelName#", master.name);
                WriteAllText(outputPrivateFile, outputPrivateContents);
            }
        }

        private static void GenerateWidgets(UIMaster master, List<string> outputContents, List<string> currentContents)
        {
            foreach (var widget in master.GetComponentsInChildren<UIWidget>(true))
            {
                var widgetType = widget.GetType().Name;
                var template = new List<string>(File.ReadAllLines(Path.Combine(WidgetTemplatePath, widgetType + ".lua")));
                var view = widget.GetComponentInParentHard<UIView>() as UINode;
                if (view == null)
                    view = master;

                for (var i = template.Count - 1; i >= 0; --i)
                {
                    template[i] = template[i].Replace("#PanelName#", master.name);
                    template[i] = template[i].Replace("#ViewName#", view == master ? master.name + "BaseView" : master.name + view.name);
                    template[i] = template[i].Replace("#WidgetName#", widget.name);
                    var line = template[i];
                    if (line.StartsWith(master.name) == false)
                        continue;
                    var index = currentContents.IndexOf(line);
                    if (index > 0)
                    {
                        for (var j = 1; j < currentContents.Count - index; ++j)
                        {
                            var current = j + index;
                            if (template[i + j].StartsWith("}"))
                                break;
                            if (template[i + j].Contains("}"))
                                continue;
                            var field = template[i + j].Substring(0, template[i + j].IndexOf("=") - 1);
                            if (currentContents[current].StartsWith(field))
                                template[i + j] = currentContents[current];
                        }
                    }
                }
                outputContents.InsertRange(outputContents.Count - 1, template);
                // 添加一个换行
                outputContents.Insert(outputContents.Count-1, "");
            }
        }

        private static void WriteAllText(string path, string contents)
        {
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
            File.WriteAllText(path, contents);
        }
    }
}
