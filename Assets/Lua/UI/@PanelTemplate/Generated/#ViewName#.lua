local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local #ViewName# = BaseClass(nil, "#ViewName#")
@foreach WidgetType@ local #WidgetType# = require("UI.UIWidgets.#WidgetType#") @end@

function #ViewName#:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or CS.UnityEngine.Resources.Load("#ViewName#").transform
    self.root.localScale = {x=1,y=1,z=1}
    self.root.anchorMin = {x=0,y=0}
    self.root.anchorMax = {x=1,y=1}

    UIHelper.InitUITable(self.root, self)

    @foreach EventName@ self.et:ListenEvent("#EventName#", Bind(self.#EventName#, self)) @end@

    @foreach Widget@ self.#WidgetName# = #WidgetType#.New():Bind(self.ot.#WidgetName#, self, panel.config.#ViewName#.#WidgetName#)  @end@

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! #ViewName#") end
    return self
end

function #ViewName#:Release()
    self.et:ClearAllEvents()

    @foreach Widget@ self.#WidgetName#:UnBind()
    self.#WidgetName# = nil @end@

    if self.root ~= self.panel.root then
        if IsNull(self.root) == false then
            CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
        end
    end
    self.root = nil
end

function #ViewName#:Show()

end

function #ViewName#:Hide()

end

return #ViewName#