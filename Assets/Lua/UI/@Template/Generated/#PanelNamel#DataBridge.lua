local #PanelNamel#DataBridge = Class()

function #PanelNamel#DataBridge:Load()
    self.temp = {}
end

function #PanelNamel#DataBridge:Release()
    self.temp = nil
end

return #PanelNamel#DataBridge