local TipsPanelBaseView = BaseClass(require("UI.TipsPanel.Generated.TipsPanelBaseView"))

-- 刷新全部显示
function TipsPanelBaseView:RefreshAll()
end

function TipsPanelBaseView:OnTipsClose()
    GUIManager:Close("TipsPanel")
end


return TipsPanelBaseView