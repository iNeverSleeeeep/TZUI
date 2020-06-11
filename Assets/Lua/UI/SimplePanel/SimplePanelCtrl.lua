local SimplePanelCtrl = BaseClass()

function SimplePanelCtrl:Hello()
    CS.UnityEngine.Debug.Log("Hsdflo")
    self.index = self.index or 0
    self.index = self.index + 1
    CS.UnityEngine.Debug.Log("Hsdflsdfdo" ..self.index)
end

return SimplePanelCtrl