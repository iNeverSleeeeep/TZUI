local SimplePanelBaseView = require("UI.SimplePanel.Generated.SimplePanelBaseView")

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()

end

function SimplePanelBaseView:OnButtonClick()
    self.vt.bbb:SetString(self.panel.config.TestOne)
end

function SimplePanelBaseView:OnButtonClick2()
    local a = 1
end


-- heihei
function SimplePanelBaseView:Test()
    local b = 2
end

return SimplePanelBaseView