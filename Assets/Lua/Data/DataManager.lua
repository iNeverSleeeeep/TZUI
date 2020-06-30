local DataManager = BaseClass(nil, "DataManager")

local DataEvents = require("Data.DataEvents")

local cache = {}
local TraceImpl = nil
TraceImpl = function(stack, deep)
    return {
        __index = function(t, k)
            local value = rawget(stack[deep][2], k)
            stack[deep+1] = {k, value}
            if type(value) == "table" then
                return setmetatable(cache, TraceImpl(stack, deep+1))
            else
                return value
            end
        end,
        __newindex = function(t, k, v)
            rawset(stack[deep][2], k, v)
            local builder = {}
            for i = 1, deep do
                builder[i] = stack[i][1]
            end
            builder[deep+1] = k
            local path = table.concat(builder, ".")
            if DataEvents[path] then
                GEventManager:DispatchEvent(DataEvents[path])
            end
        end,
    }
end
local function Trace(data)
    return setmetatable({}, {
        __index = function(t, k)
            return setmetatable(cache, TraceImpl({{k, rawget(data, k)}}, 1))
        end
    })
end

local ReadOnlyImpl
ReadOnlyImpl = function(stack, deep)
    return {
        __index = function(t, k)
            local value = rawget(stack[deep], k)
            stack[deep+1] = value
            if type(value) == "table" then
                return setmetatable(cache, ReadOnlyImpl(stack, deep+1))
            else
                return value
            end
        end,
        __newindex = function(t, k, v)
            LogE("Read only!")
        end
    }
end
local function ReadOnly(data)
    return setmetatable({}, ReadOnlyImpl({data}, 1))
end

function DataManager:__init()
    local data = {
        LocalPlayer = require("Data.LocalPlayer"),
        LocalRole = require("Data.LocalRole"),
    }
    local UNITY_EDITOR = true
    if UNITY_EDITOR then
        self.LocalPlayer = ReadOnly(data.LocalPlayer)
        self.LocalRole = ReadOnly(data.LocalRole)
    else
        self.LocalPlayer = data.LocalPlayer
        self.LocalRole = data.LocalRole
    end

    self.Mutable = Trace(data)
end

return DataManager