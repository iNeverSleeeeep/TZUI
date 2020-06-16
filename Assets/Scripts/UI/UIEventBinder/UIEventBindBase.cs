using UnityEngine;

namespace TZUI
{
    [ExecuteInEditMode]
    public abstract class UIEventBindBase : MonoBehaviour
    {
        [SerializeField]
        private UINode m_EventTable;

        public UINode EventTable
        {
            get
            {
                return m_EventTable;
            }
        }

        protected void Awake()
        {
            RefreshEventTable();
        }

        private void RefreshEventTable()
        {
            if (m_EventTable == null)
                m_EventTable = this.GetComponentInParentHard<UINode>();
        }

        internal UISignal FindEvent(string name)
        {
            if (EventTable != null)
                return EventTable.GetEventSignal(name);

            return null;
        }

#if UNITY_EDITOR
        public abstract bool IsListenEvent(string eventName);

        private void OnValidate()
        {
            m_EventTable = null;
            RefreshEventTable();
        }
#endif
    }
}
