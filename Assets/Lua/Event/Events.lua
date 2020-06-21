local id = 0
local function GenName()
    id = id + 1
    return id
end

local Events = {
    PlayerAttribute = {
        HP = GenName()
    }
}

return Events