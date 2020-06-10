local UIHelper = require("UICommon.UIHelper")

local PlayerPanel = Class()
local _views, _config, _panel
local _required, _widgets = {}, {}

_views = {
    PlayerView = "PlayerPanel.Generated.PlayerView",
    BagView = "PlayerPanel.Generated.BagView",
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
    _widgets[#_widgets+1] = require("UIWidgets.CloseButtonWidget").New():Bind(_panel.ui.CloseButtonWidget, _panel)
end

local function _releaseGenericWidgets()
    for i = 1, #_widgets do
        _widgets[i]:Release()
    end
end

function PlayerPanel:Load()
    _panel = self
    self.root = Resources.Load("PlayerPanel.prefab")
    UIHelper.InitUITable(self.root, self)
    _config = _require("PlayerPanel.Private.PlayerPanelConfig")
    self.db = _require("PlayerPanel.Private.PlayerPanelDataBridge")
    self.views = setmetatable(_views, {__index=_loadView})
    _initGenericWidgets()
end

function PlayerPanel:Release()
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

return PlayerPanel