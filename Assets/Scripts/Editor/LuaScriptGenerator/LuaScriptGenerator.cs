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
            var files = Directory.GetFiles(TemplatePath, "*.lua", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (file.Contains("Generated"))
                {
                    if (file.Contains("#PanelName#"))
                    {

                    }
                }
            }
        }
    }
}
