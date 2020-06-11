using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Lua : MonoBehaviour
{
    LuaEnv m_LuaEnv;
    // Start is called before the first frame update
    void Start()
    {
        m_LuaEnv = new LuaEnv();
        m_LuaEnv.AddLoader(CustomLoader);
        m_LuaEnv.DoString("require('Boot')");
    }

    private void OnDestroy()
    {
        m_LuaEnv.Dispose();
        m_LuaEnv = null;
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
