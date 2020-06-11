local UIHelper = require("UI.UICommon.UIHelper")

local SimplePanel = BaseClass()
local _views, _config, _panel
local _required, _widgets = {}, {}

_views = {
    --#ViewName# = "SimplePanel.Generated.#ViewName#",
}

local function _require(moduleName)
    _required[moduleName] = require(moduleName)
    return _required[moduleName]
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

function SimplePanel:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("SimplePanel")
    self.root = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform).transform
    UIHelper.InitUITable(self.root, self)
    _config = _require("UI.SimplePanel.Private.SimplePanelConfig")
    self.db = _require("UI.SimplePanel.Private.SimplePanelDataBridge")
    self.views = setmetatable(_views, {__index=_loadView})
    _initGenericWidgets()

    self.baseview = _require("UI.SimplePanel.Private.SimplePanelBaseView").New():Load(self, self.root)
end

function SimplePanel:Release()
    _releaseGenericWidgets()
    for k, v in pairs(self.views) do
        v:Release()
    end
    self.baseview:Release()
    for k, v in pairs(_required) do
        package.loaded[k] = nil
    end
    if IsNull(self.root) == false then
        CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
    end
    self.root = nil
end

return SimplePanel