local TipsPanelBaseView = BaseClass(require("UI.TipsPanel.Generated.TipsPanelBaseView"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function TipsPanelBaseView:OnGetEvents()
end

-- 刷新全部显示
function TipsPanelBaseView:RefreshAll()
end

function TipsPanelBaseView:OnTipsClose()
    -- GUIManager:Close("TipsPanel")
end

function TipsPanelBaseView:OnLoadInfoViewClick()
    LogD("OnLoadInfoViewClick")
    self.views.InfoView:RefreshAll()
end


return TipsPanelBaseView