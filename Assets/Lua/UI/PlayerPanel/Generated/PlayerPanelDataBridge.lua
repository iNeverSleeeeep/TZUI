local PlayerPanelDataBridge = Class()

function PlayerPanelDataBridge:Load()
    self.temp = {}
end

function PlayerPanelDataBridge:Release()
    self.temp = nil
end

return PlayerPanelDataBridge