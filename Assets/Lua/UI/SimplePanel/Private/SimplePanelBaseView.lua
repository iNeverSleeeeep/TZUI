local SimplePanelBaseView = BaseClass(require("UI.SimplePanel.Generated.SimplePanelBaseView"))

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()
    self.vt.bbb:SetString(self.panel.config.TestOne + 1)
end

function SimplePanelBaseView:OnButtonClick()
    GUIManager:Open("TipsPanel")
    self.vt.bbb:SetString(y or "nil")
end

function SimplePanelBaseView:OnButtonClick2()
    local a = 1
end

function SimplePanelBaseView:OnButtonClick3()
end


-- heihei
function SimplePanelBaseView:Test()
    local b = 2
end

return SimplePanelBaseView