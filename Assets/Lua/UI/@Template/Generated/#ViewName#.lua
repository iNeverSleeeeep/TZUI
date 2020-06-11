local Bind = require('Common.HelperFunctions').Bind
local #ViewName# = BaseClass()

function #ViewName#:Load(panel, root)
    self.panel = panel
    self.views = panel.views

    self.root = root or CS.UnityEngine.Resources.Load("#ViewName#")

    UIHelper.InitUITable(self.root, self)

    @foreach #EventName#@ self.et:ListenEvent("#EventName#", Bind(self.#EventName#, self)) @end@
end

function #ViewName#:Release()

end

function #ViewName#:Show()

end

function #ViewName#:Hide()

end

return #ViewName#