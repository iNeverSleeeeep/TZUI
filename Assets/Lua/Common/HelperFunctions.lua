local HelperFunctions = {}

function HelperFunctions.UnPack(param, count, i, ...)
	if i >= count then
		if i == count then
			return param[i], ...
		end
		return ...
	end
	return param[i], BindTool.UnPack(param, count, i + 1, ...)
end

function HelperFunctions.Bind(func, self, ...)
	local count = select('#', ...)
	local param = {...}

	if 0 == count then
		return function(...) return func(...) end
	elseif 1 == count then
		return function(...) return func(param[1], ...) end
	elseif 2 == count then
		return function(...) return func(param[1], param[2], ...) end
	end

	return function(...) return func(HelperFunctions.UnPack(param, count, 1, ...)) end
end

return HelperFunctions