require("Global")

local ctrl = require("UI.SimplePanel.SimplePanelCtrl").New()
local hello = ctrl.Hello

local GameLogic = {}

function GameLogic.Start()
    GUIManager:Open("SimplePanel")
end

function GameLogic.Update()
    GDataManager.Mutable.LocalPlayer.hp = GDataManager.LocalPlayer.hp + 1
    hello(ctrl)
end

function GameLogic.Destroy()
    GUIManager:Release()
end

return GameLogic