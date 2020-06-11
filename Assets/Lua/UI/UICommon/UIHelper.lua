local UIHelper = BaseClass()
local UINodeType = typeof(CS.TZUI.UINode)

function UIHelper.InitUITable(go, panel)
    local node = go:GetComponent(UINodeType)
    local ot, vt
    ot = {
        __index = function (t, k)
            local obj = node:FindObject(k)
            rawset(ot, k, obj or false)
            return obj
        end,
        __newIndex = function() end
    }
    vt = {
        __index = function (t, k)
            local variable = node:FindVariable(k)
            rawset(vt, k, variable or false)
            return variable
        end,
        __newIndex = function() end
    }
    panel.ot = setmetatable(ot, ot)
    panel.vt = setmetatable(vt, vt)
    panel.et = node
end

return UIHelper