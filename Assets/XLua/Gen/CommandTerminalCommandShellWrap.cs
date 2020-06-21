#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class CommandTerminalCommandShellWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CommandTerminal.CommandShell);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterCommands", _m_RegisterCommands);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RunCommand", _m_RunCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddCommand", _m_AddCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IssueErrorMessage", _m_IssueErrorMessage);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IssuedErrorMessage", _g_get_IssuedErrorMessage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Commands", _g_get_Commands);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CommandTerminal.CommandShell gen_ret = new CommandTerminal.CommandShell();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CommandTerminal.CommandShell constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterCommands(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RegisterCommands(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _line = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RunCommand( _line );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CommandTerminal.CommandArg[]>(L, 3)) 
                {
                    string _command_name = LuaAPI.lua_tostring(L, 2);
                    CommandTerminal.CommandArg[] _arguments = (CommandTerminal.CommandArg[])translator.GetObject(L, 3, typeof(CommandTerminal.CommandArg[]));
                    
                    gen_to_be_invoked.RunCommand( _command_name, _arguments );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CommandTerminal.CommandShell.RunCommand!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CommandTerminal.CommandInfo>(L, 3)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    CommandTerminal.CommandInfo _info;translator.Get(L, 3, out _info);
                    
                    gen_to_be_invoked.AddCommand( _name, _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CommandTerminal.CommandArg[]>>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& (LuaAPI.lua_isnil(L, 6) || LuaAPI.lua_type(L, 6) == LuaTypes.LUA_TSTRING)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    System.Action<CommandTerminal.CommandArg[]> _proc = translator.GetDelegate<System.Action<CommandTerminal.CommandArg[]>>(L, 3);
                    int _min_arg_count = LuaAPI.xlua_tointeger(L, 4);
                    int _max_arg_count = LuaAPI.xlua_tointeger(L, 5);
                    string _help = LuaAPI.lua_tostring(L, 6);
                    
                    gen_to_be_invoked.AddCommand( _name, _proc, _min_arg_count, _max_arg_count, _help );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CommandTerminal.CommandArg[]>>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    System.Action<CommandTerminal.CommandArg[]> _proc = translator.GetDelegate<System.Action<CommandTerminal.CommandArg[]>>(L, 3);
                    int _min_arg_count = LuaAPI.xlua_tointeger(L, 4);
                    int _max_arg_count = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.AddCommand( _name, _proc, _min_arg_count, _max_arg_count );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CommandTerminal.CommandArg[]>>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    System.Action<CommandTerminal.CommandArg[]> _proc = translator.GetDelegate<System.Action<CommandTerminal.CommandArg[]>>(L, 3);
                    int _min_arg_count = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.AddCommand( _name, _proc, _min_arg_count );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CommandTerminal.CommandArg[]>>(L, 3)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    System.Action<CommandTerminal.CommandArg[]> _proc = translator.GetDelegate<System.Action<CommandTerminal.CommandArg[]>>(L, 3);
                    
                    gen_to_be_invoked.AddCommand( _name, _proc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CommandTerminal.CommandShell.AddCommand!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IssueErrorMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object[] _message = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.IssueErrorMessage( _format, _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IssuedErrorMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.IssuedErrorMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Commands(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommandTerminal.CommandShell gen_to_be_invoked = (CommandTerminal.CommandShell)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Commands);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
