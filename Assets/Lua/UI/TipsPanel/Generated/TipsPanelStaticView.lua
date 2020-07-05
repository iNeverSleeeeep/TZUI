local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local TipsPanelStaticView = BaseClass(nil, "TipsPanelStaticView")

local _gWhiteList = {GUIManager=true,LogD=true,LogE=true,LogW=true}

function TipsPanelStaticView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or panel.ot.StaticViewRectTransform
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/TipsPanelStaticView")
        local go = CS.UnityEngine.GameObject.Instantiate(prefab, panel.ot.TipsPanelRectTransform)
        go.name = prefab.name
        self.root = go.transform
    end
    self.ownroot = self.root ~= root
    self.root.localScale = {x=1, y=1, z=1}
    self.root.anchorMin = {x=0.5, y=0.5}
    self.root.anchorMax = {x=0.5, y=0.5}
    self.root.anchoredPosition = {x=0, y=0}
    self.root.sizeDelta = {x=100, y=100}

    UIHelper.InitUITable(self.root, self)

    local events = self:RegisterRefreshEvents()
    if events then
        for i = 1, #events do 
            GEventManager:ListenEvent(events[i][1], self, function(s) LimitGCall(function() events[i][2](s) end, _gWhiteList) end)
        end
    end



    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! TipsPanelStaticView") end

    LimitGCall(Bind(self.RefreshAll, self))
    return self
end

function TipsPanelStaticView:Release()
    self.et:ClearAllEvents()


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

function TipsPanelStaticView:Show()

end

function TipsPanelStaticView:Hide()

end

return TipsPanelStaticView