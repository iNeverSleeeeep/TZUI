using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;

namespace TZUI
{
    public class UIMaster : MonoBehaviour, IObjectTable, IVariableTable, IEventTable
    {
        [SerializeField]
        private UIBinder m_Binder = new UIBinder();

        public UISignal GetEventSignal(string name)
        {
            return m_Binder.GetEventSignal(name);
        }

        public UIVariable FindVariable(string name)
        {
            return m_Binder.FindVariable(name);
        }

#if UNITY_EDITOR
        public List<string> Events
        {
            get
            {
                return m_Binder.Events;
            }
        }
        public List<string> Variables
        {
            get
            {
                return m_Binder.Variables;
            }
        }

        private void OnValidate()
        {
            m_Binder.OnValidate();
        }
#endif
    }
}

