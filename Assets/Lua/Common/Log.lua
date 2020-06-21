local Log = {}

function Log.LogD(...)
    CS.UnityEngine.Debug.Log(...)
end

function Log.LogE(...)
    CS.UnityEngine.Debug.LogError(...)
end

return Log