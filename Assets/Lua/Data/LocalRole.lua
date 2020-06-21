local LocalRole = {}

function LocalRole:SetHP(value)
    self.hp = value
    GEventManager:DispatchEvent(EEvent.LocalRoleAttribute.HP)
end

return LocalRole