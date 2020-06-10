local UIHelper = require("UICommon.UIHelper")

local #PanelNamel# = Class()
local _views, _config, _panel
local _required, _widgets = {}, {}

_views = {
    #ViewName# = "#PanelNamel#.Generated.#ViewName#",
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
    _widgets[#_widgets+1] = require("UIWidgets.#WidgetName#").New():Bind(_panel.ui.#WidgetName#, _panel)
end

local function _releaseGenericWidgets()
    for i = 1, #_widgets do
        _widgets[i]:Release()
    end
end

function #PanelNamel#:Load()
    _panel = self
    self.root = Resources.Load("#PanelNamel#.prefab")
    UIHelper.InitUITable(self.root, self)
    _config = _require("#PanelNamel#.Private.#PanelNamel#Config")
    self.db = _require("#PanelNamel#.Private.#PanelNamel#DataBridge")
    self.views = setmetatable(_views, {__index=_loadView})
    _initGenericWidgets()
end

function #PanelNamel#:Release()
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

return #PanelNamel#