local PlayerPanelBaseView = BaseClass(require("UI.PlayerPanel.Generated.PlayerPanelBaseView"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function PlayerPanelBaseView:RegisterRefreshEvents()
end

-- 刷新全部显示
function PlayerPanelBaseView:RefreshAll()
end


return PlayerPanelBaseView