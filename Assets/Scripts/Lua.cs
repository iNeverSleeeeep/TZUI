using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Lua : MonoBehaviour
{
    public static LuaEnv LuaEnv;
    // Start is called before the first frame update
    void Awake()
    {
        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(CustomLoader);
        LuaEnv.DoString("require('Boot')");
    }

    private void OnDestroy()
    {
        LuaEnv.Dispose();
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
