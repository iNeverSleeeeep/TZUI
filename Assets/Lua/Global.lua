-- 所有的全局变量必须在Global里面声明
setmetatable(_G, {__newindex=function(t,k,v) LogE("Global New Index Forbidden! Key:" .. tostring(k) .. " Value:" .. tostring(v)) end})

rawset(_G, "LogD", LogD or require("Common.Log").LogD)
rawset(_G, "LogE", LogE or require("Common.Log").LogE)
rawset(_G, "UIRoot", CS.UnityEngine.GameObject.Find("UIRoot"))

rawset(_G, "BaseClass", BaseClass or require("Common.BaseClass").BaseClass)
rawset(_G, "GEventManager", GEventManager or require("Event.EventManager").New())
rawset(_G, "GUIManager", GUIManager or require("UI.UIManager").New())