local SimplePanelBaseView = require("UI.SimplePanel.Generated.SimplePanelBaseView")

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()

end

function SimplePanelBaseView:OnButtonClick()
    self.vt.bbb:SetString(self.panel.config.TestOne)
end

return SimplePanelBaseView