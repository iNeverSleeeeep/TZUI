local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local SimplePanelBaseView = BaseClass(nil, "SimplePanelBaseView")
local CloseButtonWidget = require("UI.UIWidgets.CloseButtonWidget")

function SimplePanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or nil
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/SimplePanelBaseView")
        local go = CS.UnityEngine.GameObject.Instantiate(prefab, panel.ot.xRectTransform)
        go.name = prefab.name
        self.root = go.transform
    end
    self.ownroot = self.root ~= root
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}
    self.root.anchoredPosition = {x=0,y=0}
    self.root.sizeDelta = {x=0,y=0}

    UIHelper.InitUITable(self.root, self)

    local events = self:OnGetEvents()
    for i = 1, #events do 
        GEventManager:ListenEvent(events[i][1], self, events[i][2])
    end

    self.et:ListenEvent("OnButtonClick", Bind(self.OnButtonClick, self))
    self.et:ListenEvent("OnButtonClick2", Bind(self.OnButtonClick2, self))
    self.et:ListenEvent("OnButtonClick3", Bind(self.OnButtonClick3, self))

    self.CloseButton = CloseButtonWidget.New():Bind(self.ot.CloseButtonCloseButtonWidget, self, panel.config.SimplePanelBaseView.CloseButton) 
    self.CloseButton2 = CloseButtonWidget.New():Bind(self.ot.CloseButton2CloseButtonWidget, self, panel.config.SimplePanelBaseView.CloseButton2) 
    self.CloseButton3 = CloseButtonWidget.New():Bind(self.ot.CloseButton3CloseButtonWidget, self, panel.config.SimplePanelBaseView.CloseButton3) 

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! SimplePanelBaseView") end

    self:RefreshAll()
    return self
end

function SimplePanelBaseView:Release()
    self.et:ClearAllEvents()

    self.CloseButton:UnBind()
    self.CloseButton = nil
    self.CloseButton2:UnBind()
    self.CloseButton2 = nil
    self.CloseButton3:UnBind()
    self.CloseButton3 = nil

    local events = self:OnGetEvents()
    if events then
        for i = 1, #events do 
            GEventManager:StopListenEvent(events[i][1], self) 
        end
    end

    if self.ownroot then
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