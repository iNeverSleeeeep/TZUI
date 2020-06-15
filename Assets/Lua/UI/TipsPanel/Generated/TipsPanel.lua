local UIHelper = require("UI.UICommon.UIHelper")

local TipsPanel = BaseClass(nil, "TipsPanel")
local _views, _panel
local _required = {}

_views = {
    --#ViewName# = "TipsPanel.Generated.#ViewName#",
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

function TipsPanel:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("TipsPanel")
    self.root = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform).transform
    UIHelper.InitUITable(self.root, self)
    self.config = _require("UI.TipsPanel.Private.TipsPanelConfig")
    self.db = _require("UI.TipsPanel.Private.TipsPanelDataBridge").New():Load()
    self.views = setmetatable(_views, {__index=_loadView})

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