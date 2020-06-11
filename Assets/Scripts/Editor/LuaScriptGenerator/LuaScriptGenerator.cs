using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                return Path.Combine(Application.dataPath, "Lua", "UI", "@Template");
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
            var templateFile = Path.Combine(TemplatePath, "Generated", "#PanelName#.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@Template", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            var outputContents = templateContents.Replace("#PanelName#", master.name);
            WriteAllText(outputFile, outputContents);
        }

        private static void GeneratePanelConfig(UIMaster master)
        {
            var templateFile = Path.Combine(TemplatePath, "Private", "#PanelName#Config.lua");
            var templateContents = File.ReadAllText(templateFile);
            var outputFile = templateFile.Replace("@Template", master.name).Replace("#PanelName#", master.name);
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
            var outputFile = templateFile.Replace("@Template", master.name).Replace("#ViewName#", viewName);
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            var outputContents = templateContents.Replace("#ViewName#", viewName).Replace("#PanelName#", master.name);
            WriteAllText(outputFile, outputContents);

            var privateFile = Path.Combine(TemplatePath, "Private", "#ViewName#.lua");
            var privateContents = File.ReadAllText(privateFile);
            var outputPrivateFile = privateFile.Replace("@Template", master.name).Replace("#ViewName#", viewName);
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
            var outputFile = templateFile.Replace("@Template", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            var outputContents = templateContents.Replace("#PanelName#", master.name);
            WriteAllText(outputFile, outputContents);

            var privateFile = Path.Combine(TemplatePath, "Private", "#PanelName#DataBridge.lua");
            var privateContents = File.ReadAllText(privateFile);
            var outputPrivateFile = privateFile.Replace("@Template", master.name).Replace("#PanelName#", master.name);
            if (File.Exists(outputPrivateFile) == false)
            {
                var outputPrivateContents = privateContents.Replace("#PanelName#", master.name);
                WriteAllText(outputPrivateFile, outputPrivateContents);
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
