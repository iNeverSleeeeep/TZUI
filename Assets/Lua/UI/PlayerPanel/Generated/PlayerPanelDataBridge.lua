local PlayerPanelDataBridge = BaseClass(nil, "PlayerPanelDataBridge")

function PlayerPanelDataBridge:Load()
    self.player = GDataManager.LocalPlayer
    self.role = GDataManager.LocalRole
    self.temp = {}
    return self
end

function PlayerPanelDataBridge:Release()
    self.temp = nil
end

return PlayerPanelDataBridge