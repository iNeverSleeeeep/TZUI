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

#define NONVALIDVALUE		cast(TValue *, luaO_nilobject)
#define ispseudo(i)		((i) <= LUA_REGISTRYINDEX)
static TValue *index2addr (lua_State *L, int idx) {
  CallInfo *ci = L->ci;
  if (idx > 0) {
    TValue *o = ci->func + idx;
    api_check(L, idx <= ci->top - (ci->func + 1), "unacceptable index");
    if (o >= L->top) return NONVALIDVALUE;
    else return o;
  }
  else if (!ispseudo(idx)) {  /* negative index */
    api_check(L, idx != 0 && -idx <= L->top - (ci->func + 1), "invalid index");
    return L->top + idx;
  }
  else if (idx == LUA_REGISTRYINDEX)
    return &G(L)->l_registry;
  else {  /* upvalues */
    idx = LUA_REGISTRYINDEX - idx;
    api_check(L, idx <= MAXUPVAL + 1, "upvalue index too large");
    if (ttislcf(ci->func))  /* light C function? */
      return NONVALIDVALUE;  /* it has no upvalues */
    else {
      CClosure *func = clCvalue(ci->func);
      return (idx <= func->nupvalues) ? &func->upvalue[idx-1] : NONVALIDVALUE;
    }
  }
}


static int hot_swaplfunc(lua_State *L)
{
    StkId o1, o2;
    Proto *p1, *p2;

    luaL_checktype(L, 1, LUA_TFUNCTION);
    luaL_checktype(L, 2, LUA_TFUNCTION);

    o1 = index2addr(L, 1);
    o2 = index2addr(L, 2);

    p1 = getproto(o1);
    p2 = getproto(o2);
    getproto(o1) = p2;
    getproto(o2) = p1;
    
    lua_settop(L, 0);
    return 0;
}
#endif

static const struct luaL_Reg hot_funcs[] = 
{
    { "swaplfunc", hot_swaplfunc},
	{ NULL, NULL }
};

LUALIB_API int luaL_openhot(lua_State *L)
{  
    lua_newtable(L);
    luaL_newlib(L, hot_funcs);
    return 1;
}