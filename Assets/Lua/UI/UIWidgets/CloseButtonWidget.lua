local CloseButtonWidget = Class()

function CloseButtonWidget:Bind(widget, panel)
    self.widget = widget
    self.panel = panel
    return self
end

function CloseButtonWidget:Release()
    self.widget = nil
    self.panel = nil
end

function CloseButtonWidget:OnCloseClick()
    self.panel:Release()
end

return CloseButtonWidget