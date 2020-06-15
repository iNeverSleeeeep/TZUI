using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Collections;
using XLua;

public class LuaHotLoader : MonoBehaviour
{
    public string ScriptPath;
    public string Pattern;

    private Regex regex;
    private Dictionary<string, DateTime> fileInfo;
    private LuaEnv m_LuaEnv;
    private LuaEnv LuaEnv
    {
        get
        {
            return m_LuaEnv;
        }
    }
    public string FullScriptPath
    {
        get
        {
            if (string.IsNullOrEmpty(ScriptPath))
                return Application.dataPath;
            else if (Path.IsPathRooted(ScriptPath) == false)
                return Path.Combine(Application.dataPath, ScriptPath);
            return ScriptPath;
        }
    }

    public void Init()
    {
        if (m_LuaEnv == null)
        {
            m_LuaEnv = Lua.LuaEnv;
            m_LuaEnv.AddBuildin("hot", XLua.LuaDLL.Lua.LoadHotLib);
            m_LuaEnv.DoString(reload);
        }
    }

    void Start()
    {
#if UNITY_EDITOR
        regex = new Regex(Pattern, RegexOptions.Compiled);
        var files = new DirectoryInfo(FullScriptPath).GetFiles("*.lua", SearchOption.AllDirectories);
        fileInfo = new Dictionary<string, DateTime>();
        foreach (var file in files)
        {
            if (regex.IsMatch(file.FullName))
            {
                fileInfo.Add(file.FullName, file.LastWriteTime);
            }
        }
#endif
    }

    public void CheckAndReload()
    {
#if UNITY_EDITOR
        if (LuaEnv == null)
            return;
        if (enabled == false)
            return;
        if (regex == null)
            return;

        var files = new DirectoryInfo(FullScriptPath).GetFiles("*.lua", SearchOption.AllDirectories);
        var reloadList = new List<string>();
        foreach (var file in files)
        {
            if (regex.IsMatch(file.FullName) == false)
                continue;

            if (fileInfo.ContainsKey(file.FullName) == false)
            {
                fileInfo.Add(file.FullName, file.LastWriteTime);
            }
            else if (fileInfo[file.FullName] != file.LastWriteTime)
            {
                reloadList.Add(file.FullName);
                fileInfo[file.FullName] = file.LastWriteTime;
            }
        }
        if (reloadList.Count > 0)
            Reload(reloadList);
#endif
    }

#if UNITY_EDITOR
    private void OnApplicationFocus(bool focus)
    {
        if (Application.isPlaying == false)
            return;
        if (focus == false)
            return;
        CheckAndReload();
    }
#endif

    private void Reload(List<string> reloadList)
    {
        var moduleList = new List<string>();
        foreach (var fullname in reloadList)
        {
            var moduleFileName = fullname.Substring(FullScriptPath.Length).Replace("\\", ".").Replace("/", ".");
            moduleFileName = moduleFileName.Substring(0, moduleFileName.Length - 4);
            moduleList.Add(moduleFileName);
        }

        LuaEnv.DoString(string.Format("hotreload('{0}')", string.Join("', '", moduleList)));
    }

    private readonly string reload = @"
local hot = require('hot')
local rawrequire = require
local cache = {}

function release(path)
    package.loaded[path] = nil
    cache[path] = nil
end

function require(path)
    if package.loaded[path] ~= nil then
        return package.loaded[path]
    end
    if cache[path] == nil then
        local ret = rawrequire(path)
        if type(ret) == 'table' then
            cache[path] = ret
            return cache[path]
        else
            return ret
        end
    else
        local raw = cache[path]
        local functions = {}
        for k, v in pairs(raw) do
            if type(v) == 'function' then
                functions[k] = v
            end
        end
        local ret = rawrequire(path)
        if type(ret) == 'table' then
            for k, v in pairs(ret) do
                if type(v) == 'function' then
                    if functions[k] ~= nil then
                        hot.setlfunc(functions[k], v)
                    else
                        raw[k] = v
                    end
                end
            end
            package.loaded[path] = raw
            return raw
        else
            return ret
        end
    end
end
function hotreload(...)
    collectgarbage('collect')
    collectgarbage('stop')
    for i, path in ipairs({...}) do
        package.loaded[path] = nil
    end
    for i, path in ipairs({...}) do
        require(path)
    end
    collectgarbage('restart')
end
";
}