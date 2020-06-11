require("BaseClass")
CS.UnityEngine.Debug.Log("ok")
UIRoot = CS.UnityEngine.GameObject.Find("UIRoot")
local SimplePanel = require("UI.SimplePanel.Generated.SimplePanel")
local panel = SimplePanel.New()
panel:Load()

local ctrl = require("UI.SimplePanel.SimplePanelCtrl").New()
local hello = ctrl.Hello

function Update()
    hello(ctrl)
end