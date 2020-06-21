local #PanelName#DataBridge = BaseClass(nil, "#PanelName#DataBridge")

function #PanelName#DataBridge:Load()
    self.player = GDataManager.LocalPlayer
    self.role = GDataManager.LocalRole
    self.temp = {}
    return self
end

function #PanelName#DataBridge:Release()
    self.temp = nil
end

return #PanelName#DataBridge