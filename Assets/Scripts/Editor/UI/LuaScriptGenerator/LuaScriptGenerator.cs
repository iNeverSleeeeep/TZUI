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
        public static string TemplatePath
        {
            get
            {
                return Path.Combine(Application.dataPath, "Lua", "UI", "@PanelTemplate");
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
            GenerateWidgets(master);
        }

        private static void GeneratePanel(UIMaster master)
        {
            var templateFile = Path.Combine(TemplatePath, "Generated", "#PanelName#.lua");
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
            var templateFile = Path.Combine(TemplatePath, "Private", "#PanelName#Config.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputFile) == false)
            {
                var outputContents = templateContents.Replace("#PanelName#", master.name);
                WriteAllText(outputFile, outputContents);
            }
        }

        private static void GenerateBaseView(UIMaster master)
        {
            var viewName = master.name + "BaseView";
            var templateFile = Path.Combine(TemplatePath, "Generated", "#ViewName#.lua");
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

            var privateFile = Path.Combine(TemplatePath, "Private", "#ViewName#.lua");
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
            var templateFile = Path.Combine(TemplatePath, "Generated", "#PanelName#DataBridge.lua");
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

            var privateFile = Path.Combine(TemplatePath, "Private", "#PanelName#DataBridge.lua");
            var privateContents = File.ReadAllText(privateFile);
            var outputPrivateFile = privateFile.Replace("@PanelTemplate", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputPrivateFile) == false)
            {
                var outputPrivateContents = privateContents.Replace("#PanelName#", master.name);
                WriteAllText(outputPrivateFile, outputPrivateContents);
            }
        }

        private static void GenerateWidgets(UIMaster master)
        {
            foreach (var widget in master.GetComponentsInChildren<UIWidget>(true))
            {
                var widgetType = widget.GetType().Name;
                Debug.Log(widgetType);
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
