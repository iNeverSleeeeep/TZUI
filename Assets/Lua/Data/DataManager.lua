local DataManager = BaseClass(nil, "DataManager")

local DataEvents = require("Data.DataEvents")

local cache = {}
local Trace = nil
Trace = function(stack)
    return {
        __index = function(t, k)
            local value = rawget(stack[#stack][2], k)
            stack[#stack+1] = {k, value}
            if type(value) == "table" then
                return setmetatable(cache, Trace(stack))
            else
                return value
            end
        end,
        __newindex = function(t, k, v)
            rawset(stack[#stack][2], k, v)
            local builder = {}
            for i = 1, #stack do
                builder[i] = stack[i][1]
            end
            builder[#stack+1] = k
            local path = table.concat(builder, ".")
            if DataEvents[path] then
                LogD(path)
                GEventManager:DispatchEvent(DataEvents[path])
            end
        end,
    }
end

function DataManager:__init()
    self.LocalPlayer = require("Data.LocalPlayer")
    self.LocalRole = require("Data.LocalRole")

    self.Mutable = setmetatable({}, {
        __index = function(t, k)
            return setmetatable(cache, Trace({{k, rawget(self, k)}}))
        end
    })
end

return DataManager