local TipsPanelInfoView = BaseClass(require("UI.TipsPanel.Generated.TipsPanelInfoView"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function TipsPanelInfoView:OnGetEvents()
end

-- 刷新全部显示
function TipsPanelInfoView:RefreshAll()
    --LogD("TipsPanelInfoView:RefreshAll")
end


-- 事件响应
function TipsPanelInfoView:OnGetEventListeners()
end

return TipsPanelInfoView