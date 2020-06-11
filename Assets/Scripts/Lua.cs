using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Lua : MonoBehaviour
{
    public static LuaEnv LuaEnv;

    private Action m_LuaUpdate;
    // Start is called before the first frame update
    void Awake()
    {
        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(CustomLoader);
        var hotLoader = GetComponent<LuaHotLoader>();
        if (hotLoader != null)
            hotLoader.Init();
        LuaEnv.DoString("require('Boot')");
        m_LuaUpdate = LuaEnv.Global.Get<Action>("Update");
    }

    private void Update()
    {
        m_LuaUpdate();
    }

    private void OnDestroy()
    {
        LuaEnv = null;
    }

    public byte[] CustomLoader(ref string filepath)
    {
        var path = Path.Combine(Application.dataPath, "Lua", filepath.Replace(".", "/")) + ".lua";
        if (File.Exists(path))
            return File.ReadAllBytes(path);
        else
            return null;
    }
}
