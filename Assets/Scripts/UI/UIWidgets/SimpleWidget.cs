using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZUI
{
#if UNITY_EDITOR
    public enum SimpleWidgetName
    {
        未指定_Invalid = 0,
        关闭按钮组件_CloseButtonWidget = 1,
        资产组件_AssetWidget = 2,
    }
#endif

    [ExecuteInEditMode]
    public class SimpleWidget : UIWidget
    {
        public int m_WidgetIndex;
    }
}
