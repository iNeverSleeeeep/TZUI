local UIHelper = require("UI.UICommon.UIHelper")

local SimplePanel = BaseClass(nil, "SimplePanel")
local _panel
local _required, _views = {}, {}

local function _require(moduleName)
    _required[moduleName] = require(moduleName)
    return _required[moduleName]
end

local function _loadView(t, k)
    local view = _require("UI.SimplePanel.Private.SimplePanel"..k).New()
    rawset(_views, k, view)
    view:Load(_panel)
    return view
end

function SimplePanel:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("Output/SimplePanel")
    local go = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform)
    go.name = prefab.name
    self.root = go.transform
    UIHelper.InitUITable(self.root, self)
    self.config = _require("UI.SimplePanel.Private.SimplePanelConfig")
    self.db = _require("UI.SimplePanel.Private.SimplePanelDataBridge").New():Load()
    self.views = setmetatable(_views, {__index = _loadView})

    self.baseview = _require("UI.SimplePanel.Private.SimplePanelBaseView").New():Load(self, self.root)
    return self
end

function SimplePanel:Release()
    for k, v in pairs(self.views) do
        v:Release()
    end
    self.baseview:Release()
    self.db:Release()
    self.db = nil
    for k, v in pairs(_required) do
        release(k)
    end
    release("UI.SimplePanel.Generated.SimplePanel")
    if IsNull(self.root) == false then
        CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
    end
    self.root = nil
end

return SimplePanel