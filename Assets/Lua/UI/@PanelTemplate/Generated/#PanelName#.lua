local UIHelper = require("UI.UICommon.UIHelper")

local #PanelName# = BaseClass(nil, "#PanelName#")
local _panel
local _required, _views = {}, {}

local function _require(moduleName)
    _required[moduleName] = require(moduleName)
    return _required[moduleName]
end

local function _loadView(t, k)
    local view = _require("UI.#PanelName#.Private.#PanelName#"..k).New()
    rawset(_views, k, view)
    view:Load(_panel, nil)
    return view
end

function #PanelName#:Load()
    _panel = self
    local prefab = CS.UnityEngine.Resources.Load("Output/#PanelName#")
    local go = CS.UnityEngine.GameObject.Instantiate(prefab, UIRoot.transform)
    go.name = prefab.name
    self.root = go.transform
    UIHelper.InitUITable(self.root, self)
    self.config = _require("UI.#PanelName#.Private.#PanelName#Config")
    self.db = _require("UI.#PanelName#.Private.#PanelName#DataBridge").New():Load()
    self.views = setmetatable(_views, {__index = _loadView})

    self.baseview = _require("UI.#PanelName#.Private.#PanelName#BaseView").New():Load(self, UIRoot.transform, self.root)
    return self
end

function #PanelName#:Release()
    for k, v in pairs(self.views) do
        v:Release()
    end
    self.baseview:Release()
    self.db:Release()
    self.db = nil
    for k, v in pairs(_required) do
        release(k)
    end
    release("UI.#PanelName#.Generated.#PanelName#")
    if IsNull(self.root) == false then
        CS.UnityEngine.GameObject.Destroy(self.root.gameObject)
    end
    self.root = nil
end

return #PanelName#