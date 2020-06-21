local CommandManager = BaseClass(nil, "CommandManager")

function CommandManager:__init()
    local commands = require("Debug.Commands")
    for i = 1, #commands do
        self:AddCommand(table.unpack(commands[i]))
    end
end

function CommandManager:AddCommand(...)
    CS.CommandTerminal.Terminal.Shell:AddCommand(...)
end

return CommandManager