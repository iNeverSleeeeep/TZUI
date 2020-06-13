local SimplePanelDataBridge = BaseClass(nil, "SimplePanelDataBridge")

function SimplePanelDataBridge:Load()
    self.temp = {}
    return self
end

function SimplePanelDataBridge:Release()
    self.temp = nil
end

return SimplePanelDataBridge