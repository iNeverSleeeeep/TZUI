﻿using UnityEngine;
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
            if (m_LuaEnv == null)
            {
                m_LuaEnv = Lua.LuaEnv;
                m_LuaEnv.AddBuildin("hot", XLua.LuaDLL.Lua.LoadHotLib);
                m_LuaEnv.DoString(reload);
            }
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
            var moduleFileName = fullname.Substring(FullScriptPath.Length).Replace("/", ".");
            moduleFileName = moduleFileName.Substring(0, moduleFileName.Length - 4);
            moduleList.Add(moduleFileName);
        }

        LuaEnv.DoString(string.Format("hotreload('{0}')", string.Join("', '", moduleList)));
    }

    private readonly string reload = @"
local rawrequire = require
local cache = {}
local loaded = package.loaded
function require(path)
    if loaded[path] ~= nil then
        return loaded[path]
    end
    local ret = rawrequire(path)
    if cache[path] == nil then
        if type(ret) == 'string' then
            cache[path] = _G[ret]
            return cache[path]
        elseif type(ret) == 'table' then
            cache[path] = ret
            return cache[path]
        else
            return ret
        end
    else
        local raw = cache[path]
        if type(ret) == 'string' then
            local t = _G[ret]
            if type(t) == 'table' then
                for k, v in pairs(t) do
                    if type(v) == 'function' then
                        if raw[k] == nil then
                            raw[k] = v
                        else
                            hot.swaplfunc(raw[k], v)
                        end
                    end
                end
                _G[ret] = raw
            end
            loaded[path] = raw
            return raw
        elseif type(ret) == 'table' then
            for k, v in pairs(ret) do
                if type(v) == 'function' then
                    if raw[k] == nil then
                        raw[k] = v
                    else
                        hot.swaplfunc(raw[k], v)
                    end
                end
            end
            loaded[path] = raw
            return raw
        else
            return ret
        end
    end
end
function hotreload(...)
    for i, path in ipairs({...}) do
        package.loaded[path] = nil
    end
    for i, path in ipairs({...}) do
        require(path)
    end
end
";
}