local UIManager = BaseClass(nil, "UIManager")

function UIManager:__init()
    self.stack = {}
    self.dic = {}
end

function UIManager:Open(id)
    if self.dic[id] then
        return
    end
    local panel = require("UI."..id..".Generated."..id).New():Load()
    self.stack[#self.stack+1] = panel
    self.dic[id] = panel
end

function UIManager:Close(id)
    local panel = self.dic[id]
    self.dic[id] = nil
    for i = 1, #self.stack do
        if self.stack[i] == panel then
            table.remove(self.stack, i)
            break
        end
    end
    panel:Release()
end

return UIManager