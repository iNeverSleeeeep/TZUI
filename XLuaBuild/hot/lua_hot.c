#include "lua.h"
#include "lauxlib.h"

#if USING_LUAJIT
#include "lj_obj.h"
#else
#include "lobject.h"
#include "lstate.h"
#include "lfunc.h"
#include "lgc.h"
#endif

#if USING_LUAJIT
// FIXME
static int hot_setlfunc(lua_State *L)
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

static int hot_setlfunc(lua_State *L)
{
    StkId o1, o2;
    LClosure *f1, *f2;
    int i;
    
    luaL_checktype(L, 1, LUA_TFUNCTION);
    luaL_checktype(L, 2, LUA_TFUNCTION);

    o1 = L->ci->func + 1;
    o2 = L->ci->func + 2;
    f1 = clLvalue(o1);
    f2 = clLvalue(o2);

    for (i = 0; i < f1->nupvalues; i++) {
        UpVal *uv = f1->upvals[i];
        if (uv)
            luaC_upvdeccount(L, uv);
        f1->upvals[i] = NULL;
    }
    luaM_freemem(L, f1->upvals, cast(int, sizeof(UpVal *)*(f1->nupvalues)));
    f1->nupvalues = f2->nupvalues;
    f1->upvals = cast(UpVal **, luaM_malloc(L, sizeof(UpVal *)*(f1->nupvalues)));

    for (i = 0; i < f1->nupvalues; i++) {
        UpVal **up1 = &(f1->upvals[i]);
        UpVal **up2 = &(f2->upvals[i]);
        *up1 = *up2;
        (*up1)->refcount++;
        if (upisopen(*up1)) (*up1)->u.open.touched = 1;
        luaC_upvalbarrier(L, *up1);
    }

    getproto(o1) = getproto(o2);
    
    lua_settop(L, 0);
    return 0;
}
#endif

static const struct luaL_Reg hot_funcs[] = 
{
    { "setlfunc", hot_setlfunc},
	{ NULL, NULL }
};

LUALIB_API int luaL_openhot(lua_State *L)
{  
    lua_newtable(L);
    luaL_newlib(L, hot_funcs);
    return 1;
}