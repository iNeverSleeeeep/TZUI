local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local CloseButtonWidget = BaseClass()

function CloseButtonWidget:Bind(root, view, config)
    self.root = root
    self.view = view
    self.panel = view.panel
    self.config = config
    
    UIHelper.InitUITable(self.root, self)

    self.et:ListenEvent("OnCloseClick", Bind(self.OnCloseClick, self))
    return self
end

function CloseButtonWidget:UnBind()
    self.et:ClearAllEvents()
    self.root = nil
    self.panel = nil
end

function CloseButtonWidget:OnCloseClick()
    LogD("OnCloseClick")
    GUIManager:Close(self.panel.name)
end

return CloseButtonWidget