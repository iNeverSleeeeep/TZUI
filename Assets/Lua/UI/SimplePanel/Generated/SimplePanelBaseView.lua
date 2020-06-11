local Bind = require('Common.HelperFunctions').Bind
local SimplePanelBaseView = BaseClass()

function SimplePanelBaseView:Load(panel, root)
    self.panel = panel
    self.views = panel.views

    self.root = root or CS.UnityEngine.Resources.Load("SimplePanelBaseView")

    UIHelper.InitUITable(self.root, self)

    self.et:ListenEvent("abcde", Bind(self.abcde, self))
end

function SimplePanelBaseView:Release()

end

function SimplePanelBaseView:Show()

end

function SimplePanelBaseView:Hide()

end

return SimplePanelBaseView