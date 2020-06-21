local TipsPanelStaticView = BaseClass(require("UI.TipsPanel.Generated.TipsPanelStaticView"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function TipsPanelStaticView:OnGetEvents()
end

-- 刷新全部显示
function TipsPanelStaticView:RefreshAll()
end


-- 事件响应
function TipsPanelStaticView:OnGetEventListeners()
end

return TipsPanelStaticView