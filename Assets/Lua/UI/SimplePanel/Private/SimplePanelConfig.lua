local SimplePanelConfig = nil

-- 页面总配置
SimplePanelConfig = {
    TestOne = 2,
    -- AAA = 3, 废弃
    SubConfig = {
        a = 4,
        b = 090909,
    },
}

SimplePanelConfig.SimplePanelBaseView = {}

-- 哦哦哦
SimplePanelConfig.SimplePanelBaseView.CloseButtonWidget = {
    CloseDelay = 10, -- 延时关闭
}

return SimplePanelConfig