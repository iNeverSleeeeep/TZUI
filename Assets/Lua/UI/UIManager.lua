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
    LogD("UI Open "..id)
    self.dic[id] = panel
end

function UIManager:Close(id)
    LogD("UI Close "..id)
    local panel = self.dic[id]
    self.dic[id] = nil
    for i = 1, #self.stack do
        if self.stack[i] == panel then
            table.remove(self.stack, i)
            break
        end
    end
    if panel then
        panel:Release()
    end
end

function UIManager:Release()
    local idlist = {}
    for i = 1, #self.stack do
        idlist[i] = self.stack[i].name
    end
    for i = #idlist, 1, -1 do
        self:Close(idlist[i])
    end
end

return UIManager