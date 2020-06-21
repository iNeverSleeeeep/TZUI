local TipsPanelDataBridge = BaseClass(nil, "TipsPanelDataBridge")

function TipsPanelDataBridge:Load()
    self.player = GDataManager.LocalPlayer
    self.role = GDataManager.LocalRole
    self.temp = {}
    return self
end

function TipsPanelDataBridge:Release()
    self.temp = nil
end

return TipsPanelDataBridge