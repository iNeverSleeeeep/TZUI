using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Lua : MonoBehaviour
{
    public static LuaEnv LuaEnv;

    private Action m_LuaStart;
    private Action m_LuaUpdate;
    private Action m_LuaDestroy;
    // Start is called before the first frame update
    void Start()
    {
        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(CustomLoader);
        var hotLoader = GetComponent<LuaHotLoader>();
        if (hotLoader != null)
            hotLoader.Init();
        LuaTable gameLogic = LuaEnv.DoString("return require('Boot')")[0] as LuaTable;
        m_LuaStart = gameLogic.Get<Action>("Start");
        m_LuaUpdate = gameLogic.Get<Action>("Update");
        m_LuaDestroy = gameLogic.Get<Action>("Destroy");

        m_LuaStart?.Invoke();
    }

    private void OnEnable()
    {
        m_LuaStart?.Invoke();
    }

    private void OnDisable()
    {
        m_LuaDestroy?.Invoke();
    }

    private void Update()
    {
        m_LuaUpdate?.Invoke();
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
