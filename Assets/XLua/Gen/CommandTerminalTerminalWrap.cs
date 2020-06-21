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
    public class CommandTerminalTerminalWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CommandTerminal.Terminal);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetState", _m_SetState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToggleState", _m_ToggleState);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsClosed", _g_get_IsClosed);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 5, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Log", _m_Log_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Buffer", _g_get_Buffer);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Shell", _g_get_Shell);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "History", _g_get_History);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Autocomplete", _g_get_Autocomplete);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IssuedError", _g_get_IssuedError);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CommandTerminal.Terminal gen_ret = new CommandTerminal.Terminal();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CommandTerminal.Terminal constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object[] _message = translator.GetParams<object>(L, 2);
                    
                    CommandTerminal.Terminal.Log( _format, _message );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count >= 2&& translator.Assignable<CommandTerminal.TerminalLogType>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || translator.Assignable<object>(L, 3))) 
                {
                    CommandTerminal.TerminalLogType _type;translator.Get(L, 1, out _type);
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object[] _message = translator.GetParams<object>(L, 3);
                    
                    CommandTerminal.Terminal.Log( _type, _format, _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CommandTerminal.Terminal.Log!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.Terminal gen_to_be_invoked = (CommandTerminal.Terminal)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CommandTerminal.TerminalState _new_state;translator.Get(L, 2, out _new_state);
                    
                    gen_to_be_invoked.SetState( _new_state );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToggleState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CommandTerminal.Terminal gen_to_be_invoked = (CommandTerminal.Terminal)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CommandTerminal.TerminalState _new_state;translator.Get(L, 2, out _new_state);
                    
                    gen_to_be_invoked.ToggleState( _new_state );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Buffer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CommandTerminal.Terminal.Buffer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Shell(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CommandTerminal.Terminal.Shell);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_History(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CommandTerminal.Terminal.History);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Autocomplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CommandTerminal.Terminal.Autocomplete);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IssuedError(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, CommandTerminal.Terminal.IssuedError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommandTerminal.Terminal gen_to_be_invoked = (CommandTerminal.Terminal)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsClosed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
