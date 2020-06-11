local HelperFunctions = {}

function HelperFunctions.Bind(func, self, ...)
    return function()
        func(self, ...)
    end
end

return HelperFunctions