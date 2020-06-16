using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TZUI
{
    public enum UIViewLoadMode
    {
        [InspectorName("总是加载")] Always,
        [InspectorName("用时加载")] LoadOnUse,
    }

    [DisallowMultipleComponent]
    public class UIView : UINode
    {
        [SerializeField, Tooltip("用时加载方式该View会被拆分为独立prefab"), InspectorDisplayName("加载方式")]
        private UIViewLoadMode m_LoadMode = UIViewLoadMode.Always;

        public UIViewLoadMode LoadMode
        {
            get
            {
                return m_LoadMode;
            }
        }
    }
}
