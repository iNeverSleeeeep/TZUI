local PlayerPanelConfig = nil

-- 页面总配置
PlayerPanelConfig = {
    TestOne = 1,
    -- AAA = 3, 废弃
    SubConfig = {
        a = 4,
        b = 765,
    },
}

PlayerPanelConfig.PlayerPanelBaseView = {}


-- 哦哦哦
PlayerPanelConfig.PlayerPanelBaseView.CloseButtonWidget = {
    CloseDelay = 0, -- 延时关闭
}

return setmetatable(PlayerPanelConfig, {__newindex = function() end})