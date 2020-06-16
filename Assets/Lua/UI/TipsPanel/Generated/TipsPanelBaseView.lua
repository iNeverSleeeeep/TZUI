local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local TipsPanelBaseView = BaseClass(nil, "TipsPanelBaseView")
local CloseButtonWidget = require("UI.UIWidgets.CloseButtonWidget")

function TipsPanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or CS.UnityEngine.Resources.Load("TipsPanelBaseView").transform
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}

    UIHelper.InitUITable(self.root, self)

    self.et:ListenEvent("OnTipsClose", Bind(self.OnTipsClose, self))

    self.CloseButtonWidget = CloseButtonWidget.New():Bind(self.ot.CloseButtonWidget, self, panel.config.TipsPanelBaseView.CloseButtonWidget) 

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! TipsPanelBaseView") end
    return self
end

function TipsPanelBaseView:Release()
    self.et:ClearAllEvents()

    self.CloseButtonWidget:UnBind()
    self.CloseButtonWidget = nil

    if self.root ~= self.panel.root then
        if IsNull(self.root) == false then
            CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
        end
    end
    self.root = nil
end

function TipsPanelBaseView:Show()

end

function TipsPanelBaseView:Hide()

end

return TipsPanelBaseView