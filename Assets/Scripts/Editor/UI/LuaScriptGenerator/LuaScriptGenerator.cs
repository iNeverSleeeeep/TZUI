﻿using System.Collections;
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
            GenerateBaseView(master);
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

            GenerateWidgets(master, outputContents, currentContents);
            WriteAllText(outputFile, string.Join("\n", outputContents));
        }

        private static void GenerateBaseView(UIMaster master)
        {
            var viewName = master.name + "BaseView";
            var templateFile = Path.Combine(PanelTemplatePath, "Generated", "#ViewName#.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#ViewName#", viewName);
            if (File.Exists(outputFile))
            {
                File.SetAttributes(outputFile, FileAttributes.Normal);
                File.Delete(outputFile);
            }
            var outputContents = templateContents.Replace("#ViewName#", viewName).Replace("#PanelName#", master.name);

            var foreachEvent = new Regex(@"\n([ \t]*)(@foreach EventName@ )([\s\S]*)( @end@)", RegexOptions.Compiled);
            var removeForeachEvent = new Regex(@"\n--([ \t]*)(@foreach EventName@ )([\s\S]*)( @end@)", RegexOptions.Compiled);
            var replaceEvent = new Regex(@"\n([^@]*)#EventName#([\s\S]*)", RegexOptions.Compiled);

            while (foreachEvent.IsMatch(outputContents))
            {
                var events = master.Events;
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
            WriteAllText(outputFile, outputContents);
            File.SetAttributes(outputFile, FileAttributes.ReadOnly);

            var privateFile = Path.Combine(PanelTemplatePath, "Private", "#ViewName#.lua");
            var privateContents = File.ReadAllText(privateFile);
            var outputPrivateFile = privateFile.Replace("@PanelTemplate", master.name).Replace("#ViewName#", viewName);
            if (File.Exists(outputPrivateFile) == false)
            {
                var outputPrivateContents = privateContents.Replace("#ViewName#", viewName).Replace("#PanelName#", master.name);
                WriteAllText(outputPrivateFile, outputPrivateContents);
            }
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
