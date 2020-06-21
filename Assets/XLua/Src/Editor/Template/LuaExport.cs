using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using XLua;

public static class LuaExport
{
    [LuaCallCSharp]
    public static List<Type> TZUITypes = new List<Type>
    {
        typeof(TZUI.UIBinder),
    };

    [LuaCallCSharp]
    public static List<Type> ThirdPartyTypes = new List<Type>
    {
        typeof(CommandTerminal.Terminal),
        typeof(CommandTerminal.CommandShell),
        typeof(CommandTerminal.CommandInfo),
        typeof(CommandTerminal.CommandArg),
    };
}
