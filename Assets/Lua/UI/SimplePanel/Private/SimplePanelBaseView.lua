local SimplePanelBaseView = require("UI.SimplePanel.Generated.SimplePanelBaseView")

-- 刷新全部显示
function SimplePanelBaseView:RefreshAll()

end

function SimplePanelBaseView:OnButtonClick()
    CS.UnityEngine.Debug.Log(123)
end

return SimplePanelBaseView