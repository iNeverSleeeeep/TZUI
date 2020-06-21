local EventManager = BaseClass(nil, "EventManager")

function EventManager:__init()
    self.events = {} 
end

function EventManager:ListenEvent(eventName, owner, func)
    self.events[eventName] = self.events[eventName] or setmetatable({}, {__mode = "k"})
    self.events[eventName][owner] = func
end

function EventManager:StopListenEvent(eventName, owner)
    local eventHandlers = self.events[eventName]
    if eventHandlers then
        eventHandlers[owner] = nil
    end
end

function EventManager:DispatchEvent(eventName)
    local eventHandlers = self.events[eventName]
    if eventHandlers then
        for owner, func in pairs(eventHandlers) do
            func(owner)
        end
    end
end

return EventManager