local DataManager = BaseClass(nil, "DataManager")

function DataManager:__init()
    self.LocalPlayer = require("Data.LocalPlayer")
    self.LocalRole = require("Data.LocalRole")
end

return DataManager