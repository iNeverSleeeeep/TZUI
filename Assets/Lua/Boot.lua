require("Common.Log")
require("Common.BaseClass")
CS.UnityEngine.Debug.Log("ok")
UIRoot = CS.UnityEngine.GameObject.Find("UIRoot")
local SimplePanel = require("UI.SimplePanel.Generated.SimplePanel")
local panel = SimplePanel.New()

local ctrl = require("UI.SimplePanel.SimplePanelCtrl").New()
local hello = ctrl.Hello

function Start()
    panel:Load()
end

function Update()
    hello(ctrl)
end

function Destroy()
    panel:Release()
end