local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local PlayerPanelBaseView = BaseClass(nil, "PlayerPanelBaseView")

function PlayerPanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or nil
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/PlayerPanelBaseView")
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



    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! PlayerPanelBaseView") end

    self:RefreshAll()
    return self
end

function PlayerPanelBaseView:Release()
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

function PlayerPanelBaseView:Show()

end

function PlayerPanelBaseView:Hide()

end

return PlayerPanelBaseView