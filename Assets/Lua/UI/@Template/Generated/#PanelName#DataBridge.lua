local #PanelName#DataBridge = Class()

function #PanelName#DataBridge:Load()
    self.temp = {}
end

function #PanelName#DataBridge:Release()
    self.temp = nil
end

return #PanelName#DataBridge