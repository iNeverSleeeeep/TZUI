#include "lua.h"
#include "lauxlib.h"

#if USING_LUAJIT
#include "lj_obj.h"
#else
#include "lobject.h"
#include "lstate.h"
#endif

#if USING_LUAJIT
static int hot_swaplfunc(lua_State *L)
{
    TValue *o1, *o2;
    MRef ptr1, ptr2;

    luaL_checktype(L, 1, LUA_TFUNCTION);
    luaL_checktype(L, 2, LUA_TFUNCTION);
    
    o1 = L->base;
    o2 = L->base + 1;
    
    ptr2 = (&gcval(o2)->fn)->l.pc;
    ptr1 = (&gcval(o1)->fn)->l.pc;
    (&gcval(o1)->fn)->l.pc = ptr2;
    (&gcval(o2)->fn)->l.pc = ptr1;

    lua_settop(L, 0);
    return 0;
}
#else

static int hot_swaplfunc(lua_State *L)
{
    StkId o1, o2;
    Proto *p1, *p2;

    luaL_checktype(L, 1, LUA_TFUNCTION);
    luaL_checktype(L, 2, LUA_TFUNCTION);

    o1 = L->ci->func + 1;
    o2 = L->ci->func + 2;

    p1 = getproto(o1);
    p2 = getproto(o2);
    getproto(o1) = p2;
    getproto(o2) = p1;
    
    lua_settop(L, 0);
    return 0;
}

static int hot_setlfunc(lua_State *L)
{
    StkId o1, o2;
    Proto *p1, *p2;

    luaL_checktype(L, 1, LUA_TFUNCTION);
    luaL_checktype(L, 2, LUA_TFUNCTION);

    o1 = L->ci->func + 1;
    o2 = L->ci->func + 2;

    getproto(o1) = getproto(o2);
    
    lua_settop(L, 0);
    return 0;
}
#endif

static const struct luaL_Reg hot_funcs[] = 
{
    { "swaplfunc", hot_swaplfunc},
    { "setlfunc", hot_setlfunc},
	{ NULL, NULL }
};

LUALIB_API int luaL_openhot(lua_State *L)
{  
    lua_newtable(L);
    luaL_newlib(L, hot_funcs);
    return 1;
}