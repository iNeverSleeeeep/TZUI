local id = 0
local function GenName()
    id = id + 1
    return id
end

local Events = {
    LocalRoleAttribute = {
        HP = GenName()
    }
}

return Events