local SimplePanelBaseView = Class()

function SimplePanelBaseView:Load(panel)
    self.panel = panel
    self.views = panel.views
end

function SimplePanelBaseView:Release()

end

function SimplePanelBaseView:Show()

end

function SimplePanelBaseView:Hide()

end

return SimplePanelBaseView