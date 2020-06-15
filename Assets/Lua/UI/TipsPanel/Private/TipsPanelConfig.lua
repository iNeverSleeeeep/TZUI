local TipsPanelConfig = nil

-- 页面总配置
TipsPanelConfig = {
    TestOne = 1,
    -- AAA = 3, 废弃
    SubConfig = {
        a = 4,
        b = 765,
    },
}

TipsPanelConfig.TipsPanelBaseView = {}

return setmetatable(TipsPanelConfig, {__newindex = function() end})