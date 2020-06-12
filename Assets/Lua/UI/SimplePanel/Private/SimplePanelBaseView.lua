local SimplePanelBaseView = require("UI.SimplePanel.Generated.SimplePanelBaseView")

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()

end

function SimplePanelBaseView:OnButtonClick()
    self.db.temp.index = self.db.temp.index or 0
    self.db.temp.index = self.db.temp.index + 1
    self.vt.bbb:SetString(self.db.temp.index)
end

return SimplePanelBaseView