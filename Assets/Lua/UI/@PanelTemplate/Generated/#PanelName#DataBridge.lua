local #PanelName#DataBridge = BaseClass(nil, "#PanelName#DataBridge")

function #PanelName#DataBridge:Load()
    self.temp = {}
    return self
end

function #PanelName#DataBridge:Release()
    self.temp = nil
end

return #PanelName#DataBridge