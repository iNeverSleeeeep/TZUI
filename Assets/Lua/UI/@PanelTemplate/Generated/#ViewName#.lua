local UIHelper = require("UI.UICommon.UIHelper")
local Bind = require('Common.HelperFunctions').Bind
local #ViewName# = BaseClass(nil, "#ViewName#")
@foreach WidgetType@ local #WidgetType# = require("UI.UIWidgets.#WidgetType#") @end@

function #ViewName#:Load(panel, root)
    self.panel = panel
    self.views = panel.views
    self.db = panel.db

    self.root = root or #ViewRoot#
    if self.root == nil then
        local prefab = CS.UnityEngine.Resources.Load("Output/#ViewName#")
        local go = CS.UnityEngine.GameObject.Instantiate(prefab, panel.ot.#ParentName#RectTransform)
        go.name = prefab.name
        self.root = go.transform
    end
    self.ownroot = self.root ~= root
    self.root.localScale = #LocalScale#
    self.root.anchorMin = #AnchorMin#
    self.root.anchorMax = #AnchorMax#
    self.root.anchoredPosition = #AnchoredPosition#
    self.root.sizeDelta = #SizeDelta#

    UIHelper.InitUITable(self.root, self)

    @foreach EventName@ self.et:ListenEvent("#EventName#", Bind(self.#EventName#, self)) @end@

    @foreach Widget@ self.#WidgetName# = #WidgetType#.New():Bind(self.ot.#WidgetName##WidgetType#, self, panel.config.#ViewName#.#WidgetName#)  @end@

    self.__newindex = function() LogE("This Class Is Logic Only, Dont New Index! #ViewName#") end

    self:RefreshAll()
    return self
end

function #ViewName#:Release()
    self.et:ClearAllEvents()

    @foreach Widget@ self.#WidgetName#:UnBind()
    self.#WidgetName# = nil @end@

    if self.ownroot then
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