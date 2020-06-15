local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local CloseButtonWidget = BaseClass()

function CloseButtonWidget:Bind(root, panel, config)
    self.root = root
    self.panel = panel
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
    self.panel:Release()
end

return CloseButtonWidget