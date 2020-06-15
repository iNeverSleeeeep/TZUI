local TipsPanelDataBridge = BaseClass(nil, "TipsPanelDataBridge")

function TipsPanelDataBridge:Load()
    self.temp = {}
    return self
end

function TipsPanelDataBridge:Release()
    self.temp = nil
end

return TipsPanelDataBridge