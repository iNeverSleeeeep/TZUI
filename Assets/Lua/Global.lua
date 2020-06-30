-- 所有的全局变量必须在Global里面声明
local __G = {}
local lock = false
local tostring = tostring
local rawget = rawget
local xpcall = xpcall
local require = require

function LockG() lock = true end
function UnlockG() lock = false end
function NOGCall(func) lock = true xpcall(func, __G.LogE) lock = false end

setmetatable(_G, {
    __newindex = function(t,k,v) 
        __G.LogE("Global New Index Forbidden! Key:" .. tostring(k) .. " Value:" .. tostring(v)) 
    end,
    __index = function(t, k)
        if lock then
            return nil
        end
        return rawget(__G, k)
    end
})


__G.LogD = require("Common.Log").LogD
__G.LogE = require("Common.Log").LogE
__G.UIRoot = CS.UnityEngine.GameObject.Find("UIRoot")
__G.BaseClass = require("Common.BaseClass").BaseClass
__G.EEvent = require("Event.Events")
__G.GEventManager = require("Event.EventManager").New()
__G.GDataManager = require("Data.DataManager").New()
__G.GUIManager = require("UI.UIManager").New()
__G.GCommandManager = require("Debug.CommandManager").New()