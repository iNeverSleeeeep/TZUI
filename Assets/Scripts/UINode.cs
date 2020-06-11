using System.Collections.Generic;
using UnityEngine;

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

        public Object FindObject(string name)
        {
            return m_Binder.FindObject(name);
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
