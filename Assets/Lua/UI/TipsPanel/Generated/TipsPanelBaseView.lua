local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local TipsPanelBaseView = BaseClass(nil, "TipsPanelBaseView")
local CloseButtonWidget = require("UI.UIWidgets.CloseButtonWidget")

function TipsPanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or nil
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/TipsPanelBaseView")
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

    local events = self:RegisterRefreshEvents()
    if events then
        for i = 1, #events do 
            GEventManager:ListenEvent(events[i][1], self, events[i][2])
        end
    end

    self.et:ListenEvent("OnTipsClose", Bind(self.OnTipsClose, self))
    self.et:ListenEvent("OnLoadInfoViewClick", Bind(self.OnLoadInfoViewClick, self))

    self.CloseButtonWidget = CloseButtonWidget.New():Bind(self.ot.CloseButtonWidgetCloseButtonWidget, self, panel.config.TipsPanelBaseView.CloseButtonWidget) 

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! TipsPanelBaseView") end

    self:RefreshAll()
    return self
end

function TipsPanelBaseView:Release()
    self.et:ClearAllEvents()

    self.CloseButtonWidget:UnBind()
    self.CloseButtonWidget = nil

    local events = self:RegisterRefreshEvents()
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

function TipsPanelBaseView:Show()

end

function TipsPanelBaseView:Hide()

end

return TipsPanelBaseView