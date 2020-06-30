local SimplePanelBaseView = BaseClass(require("UI.SimplePanel.Generated.SimplePanelBaseView"))

-- 事件响应注册，返回数组{{event1, func1}, {event2, func2}}
function SimplePanelBaseView:RegisterRefreshEvents()
    return {
        { EEvent.LocalRoleAttribute.HP, self.RefreshHPValue }
    }
end

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()
    self.vt.bbb:SetString(self.db.player.hp)
end

function SimplePanelBaseView:OnButtonClick()
    GUIManager:Open("PlayerPanel")
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

function SimplePanelBaseView:RefreshHPValue()
    self.vt.bbb:SetString(self.db.player.hp)
end

return SimplePanelBaseView