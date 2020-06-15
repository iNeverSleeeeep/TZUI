require("Common.Log")
require("Common.BaseClass")
CS.UnityEngine.Debug.Log("ok")
UIRoot = CS.UnityEngine.GameObject.Find("UIRoot")
GUIManager = require("UI.UIManager").New()

local ctrl = require("UI.SimplePanel.SimplePanelCtrl").New()
local hello = ctrl.Hello

function Start()
    GUIManager:Open("SimplePanel")
end

function Update()
    hello(ctrl)
end

function Destroy()
    GUIManager:Close("SimplePanel")
end