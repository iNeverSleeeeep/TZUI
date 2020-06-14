local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local SimplePanelBaseView = BaseClass(nil, "SimplePanelBaseView")
local CloseButtonWidget = require("UI.UIWidgets.CloseButtonWidget")
local CloseButtonWidget = require("UI.UIWidgets.CloseButtonWidget")

function SimplePanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or CS.UnityEngine.Resources.Load("SimplePanelBaseView").transform
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}

    UIHelper.InitUITable(self.root, self)

    self.et:ListenEvent("OnButtonClick", Bind(self.OnButtonClick, self))
    self.et:ListenEvent("OnButtonClick2", Bind(self.OnButtonClick2, self))

    self.CloseButton = CloseButtonWidget.New():Load(self.ot.CloseButton, self, panel.config.SimplePanelSimplePanelBaseView.CloseButton) 
    self.CloseButton2 = CloseButtonWidget.New():Load(self.ot.CloseButton2, self, panel.config.SimplePanelSimplePanelBaseView.CloseButton2) 

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index!") end
    return self
end

function SimplePanelBaseView:Release()
    self.et:ClearAllEvents()

    self.CloseButton:Release() 
    self.CloseButton2 = nil
    self.CloseButton2:Release() 
    self.CloseButton2 = nil

    if self.root ~= self.panel.root then
        if IsNull(self.root) == false then
            CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
        end
    end
    self.root = nil
end

function SimplePanelBaseView:Show()

end

function SimplePanelBaseView:Hide()

end

return SimplePanelBaseView