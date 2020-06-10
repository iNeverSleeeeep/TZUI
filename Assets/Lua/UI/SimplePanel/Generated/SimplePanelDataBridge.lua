local SimplePanelDataBridge = Class()

function SimplePanelDataBridge:Load()
    self.temp = {}
end

function SimplePanelDataBridge:Release()
    self.temp = nil
end

return SimplePanelDataBridge