local PlayerView = Class()

function PlayerView:Load(panel)
    self.panel = panel
    self.views = panel.views
end

function PlayerView:Release()

end

function BagView:Show()

end

function BagView:Hide()

end

return PlayerView