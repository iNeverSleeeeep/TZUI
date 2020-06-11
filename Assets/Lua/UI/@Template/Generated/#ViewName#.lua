local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local #ViewName# = BaseClass()

function #ViewName#:Load(panel, root)
    self.panel = panel
    self.views = panel.views

    self.root = root or CS.UnityEngine.Resources.Load("#ViewName#").transform
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}

    UIHelper.InitUITable(self.root, self)

    @foreach EventName@ self.et:ListenEvent("#EventName#", Bind(self.#EventName#, self)) @end@
end

function #ViewName#:Release()

end

function #ViewName#:Show()

end

function #ViewName#:Hide()

end

return #ViewName#