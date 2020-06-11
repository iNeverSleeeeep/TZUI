local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local SimplePanelBaseView = BaseClass()

function SimplePanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views

    self.root = root or CS.UnityEngine.Resources.Load("SimplePanelBaseView").transform
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}

    UIHelper.InitUITable(self.root, self)

    self.et:ListenEvent("OnButtonClick", Bind(self.OnButtonClick, self))
end

function SimplePanelBaseView:Release()

end

function SimplePanelBaseView:Show()

end

function SimplePanelBaseView:Hide()

end

return SimplePanelBaseView