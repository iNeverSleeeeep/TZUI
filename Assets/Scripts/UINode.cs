using System;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace TZUI
{
    public class UINode : MonoBehaviour, IObjectTable, IVariableTable, IEventTable
    {
        [SerializeField]
        private UIBinder m_Binder = new UIBinder();

        public UISignal GetEventSignal(string name)
        {
            return m_Binder.GetEventSignal(name);
        }

        public UnityObject FindObject(string name)
        {
            return m_Binder.FindObject(name);
        }

        public UIVariable FindVariable(string name)
        {
            return m_Binder.FindVariable(name);
        }

        public void ListenEvent(string name, UIEventDelegate callback)
        {
            m_Binder.ListenEvent(name, callback);
        }

        public void ClearEvent(string name)
        {
            m_Binder.ClearEvent(name);
        }

        public void ClearAllEvent(string name)
        {
            m_Binder.ClearAllEvent(name);
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
