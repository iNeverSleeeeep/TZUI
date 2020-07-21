using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZUI
{
#if UNITY_EDITOR
    public enum Asset
    {
        未指定 = 0,
        金币 = 1,
        铜板 = 2,
    }
#endif
    [ExecuteInEditMode]
    public class AssetListWidget : UIWidget
    {
        public int[] m_AssetList;
    }
}