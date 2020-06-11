local UIHelper = require("UI.UICommon.UIHelper")

local #PanelName# = BaseClass()
local _views, _config, _panel
local _required, _widgets = {}, {}

_views = {
    --#ViewName# = "#PanelName#.Generated.#ViewName#",
}

local function _require(moduleName)
    required[moduleName] = require(moduleName)
    return required[moduleName]
end

local function _loadView(t, k)
    if _views[k] then
        local view = _require(_views[k]).New()
        rawset(t, k, view)
        view:Load(_panel)
        return view
    end
    return nil
end

local function _initGenericWidgets()
    --_widgets[#_widgets+1] = require("UIWidgets.#WidgetName#").New():Bind(_panel.ui.#WidgetName#, _panel)
end

local function _releaseGenericWidgets()
    for i = 1, #_widgets do
        _widgets[i]:Release()
    end
end

function #PanelName#:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("#PanelName#")
    self.root = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform)
    UIHelper.InitUITable(self.root, self)
    _config = _require("UI.#PanelName#.Private.#PanelName#Config")
    self.db = _require("UI.#PanelName#.Private.#PanelName#DataBridge")
    self.views = setmetatable(_views, {__index=_loadView})
    _initGenericWidgets()
end

function #PanelName#:Release()
    _releaseGenericWidgets()
    for k, v in pairs(self.views) do
        v:Release()
    end
    for k, v in pairs(_required) do
        package.loaded[k] = nil
    end
    GameObject.Destroy(self.root)
    self.root = nil
end

return #PanelName#