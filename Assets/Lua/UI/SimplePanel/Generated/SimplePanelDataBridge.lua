local SimplePanelDataBridge = BaseClass()

function SimplePanelDataBridge:Load()
    self.temp = {}
    return self
end

function SimplePanelDataBridge:Release()
    self.temp = nil
end

return SimplePanelDataBridge