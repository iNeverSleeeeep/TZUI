local UIHelper = require("UI.UICommon.UIHelper")

local TipsPanel = BaseClass(nil, "TipsPanel")
local _panel
local _required, _views = {}, {}
local _gWhiteList = {BaseClass=true,UIHelper=true}

local function _require(moduleName)
    PushGWhiteList(_gWhiteList)
    local ret, module = pcall(require, moduleName)
    if not ret then
        PushGWhiteList({LogE=true})
        LogE(module)
        PopGWhiteList()
    else
        _required[moduleName] = module
    end
    PopGWhiteList()
    return _required[moduleName]
end

local function _loadView(t, k)
    local view = _require("UI.TipsPanel.Private.TipsPanel"..k).New()
    rawset(_views, k, view)
    view:Load(_panel)
    return view
end

function TipsPanel:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("Output/TipsPanel")
    local go = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform)
    go.name = prefab.name
    self.root = go.transform
    UIHelper.InitUITable(self.root, self)
    self.config = _require("UI.TipsPanel.Private.TipsPanelConfig")
    self.db = _require("UI.TipsPanel.Private.TipsPanelDataBridge").New():Load()
    self.views = setmetatable(_views, {__index = _loadView})

    self.baseview = _require("UI.TipsPanel.Private.TipsPanelBaseView").New():Load(self, self.root)
    return self
end

function TipsPanel:Release()
    for k, v in pairs(self.views) do
        v:Release()
    end
    self.baseview:Release()
    self.db:Release()
    self.db = nil
    for k, v in pairs(_required) do
        release(k)
    end
    release("UI.TipsPanel.Generated.TipsPanel")
    if IsNull(self.root) == false then
        CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
    end
    self.root = nil
end

return TipsPanel