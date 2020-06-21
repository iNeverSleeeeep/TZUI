local SimplePanelDataBridge = BaseClass(nil, "SimplePanelDataBridge")

function SimplePanelDataBridge:Load()
    self.player = GDataManager.LocalPlayer
    self.role = GDataManager.LocalRole
    self.temp = {}
    return self
end

function SimplePanelDataBridge:Release()
    self.temp = nil
end

return SimplePanelDataBridge