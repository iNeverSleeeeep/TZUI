local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local TipsPanelInfoView = BaseClass(nil, "TipsPanelInfoView")

function TipsPanelInfoView:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or nil
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/TipsPanelInfoView")
        local go = CS.UnityEngine.GameObject.Instantiate(prefab, panel.ot.TipsPanelRectTransform)
        go.name = prefab.name
        self.root = go.transform
    end
    self.ownroot = self.root ~= root
    self.root.localScale = {x=1, y=1, z=1}
    self.root.anchorMin = {x=0.5, y=0.5}
    self.root.anchorMax = {x=0.5, y=0.5}
    self.root.anchoredPosition = {x=-261.1, y=9.1}
    self.root.sizeDelta = {x=681.7734, y=429.7552}

    UIHelper.InitUITable(self.root, self)



    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! TipsPanelInfoView") end

    self:RefreshAll()
    return self
end

function TipsPanelInfoView:Release()
    self.et:ClearAllEvents()


    if self.ownroot then
        if IsNull(self.root) == false then
            CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
        end
    end
    self.root = nil
end

function TipsPanelInfoView:Show()

end

function TipsPanelInfoView:Hide()

end

return TipsPanelInfoView