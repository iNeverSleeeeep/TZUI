local #ViewName# = BaseClass(require("UI.#PanelName#.Generated.#ViewName#"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function #ViewName#:RegisterRefreshEvents()
end

-- 刷新全部显示
function #ViewName#:RefreshAll()
end

return #ViewName#