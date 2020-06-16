local SimplePanelBaseView = BaseClass(require("UI.SimplePanel.Generated.SimplePanelBaseView"))
local y = 1
local z = 200

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()
    self.vt.bbb:SetString(self.panel.config.TestOne + 1)
end

function SimplePanelBaseView:OnButtonClick()
    GUIManager:Open("TipsPanel")
    y = y - 200
    z = z + y
    LogD(z)
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