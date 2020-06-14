local SimplePanelConfig = nil

-- 页面总配置
SimplePanelConfig = {
    TestOne = 3,
    -- AAA = 3, 废弃
    SubConfig = {
        a = 4,
        b = 090909,
    },
}

SimplePanelConfig.SimplePanelBaseView = {}

-- 哦哦哦
SimplePanelConfig.SimplePanelBaseView.CloseButton = {
    CloseDelay = 10, -- 延时关闭
}

-- 哦哦哦
SimplePanelConfig.SimplePanelBaseView.CloseButton2 = {
    CloseDelay = 0, -- 延时关闭
}

-- 哦哦哦
SimplePanelConfig.SimplePanelBaseView.CloseButton3 = {
    CloseDelay = 0, -- 延时关闭
}

return setmetatable(SimplePanelConfig, {__newindex = function() end})