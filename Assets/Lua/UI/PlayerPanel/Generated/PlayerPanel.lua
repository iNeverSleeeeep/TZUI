local UIHelper = require("UI.UICommon.UIHelper")

local PlayerPanel = BaseClass(nil, "PlayerPanel")
local _panel
local _required, _views = {}, {}

local function _require(moduleName)
    _required[moduleName] = require(moduleName)
    return _required[moduleName]
end

local function _loadView(t, k)
    local view = _require("UI.PlayerPanel.Private.PlayerPanel"..k).New()
    rawset(_views, k, view)
    view:Load(_panel)
    return view
end

function PlayerPanel:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("Output/PlayerPanel")
    local go = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform)
    go.name = prefab.name
    self.root = go.transform
    UIHelper.InitUITable(self.root, self)
    self.config = _require("UI.PlayerPanel.Private.PlayerPanelConfig")
    self.db = _require("UI.PlayerPanel.Private.PlayerPanelDataBridge").New():Load()
    self.views = setmetatable(_views, {__index = _loadView})

    self.baseview = _require("UI.PlayerPanel.Private.PlayerPanelBaseView").New():Load(self, self.root)
    return self
end

function PlayerPanel:Release()
    for k, v in pairs(self.views) do
        v:Release()
    end
    self.baseview:Release()
    self.db:Release()
    self.db = nil
    for k, v in pairs(_required) do
        release(k)
    end
    release("UI.PlayerPanel.Generated.PlayerPanel")
    if IsNull(self.root) == false then
        CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
    end
    self.root = nil
end

return PlayerPanel