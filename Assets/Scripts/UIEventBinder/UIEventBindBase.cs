using UnityEngine;

namespace TZUI
{
    [ExecuteInEditMode]
    public abstract class UIEventBindBase : MonoBehaviour
    {
        [SerializeField]
        private UIMaster m_EventTable;

        public UIMaster EventTable
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
                m_EventTable = this.GetComponentInParentHard<UIMaster>();
        }

        internal UISignal FindEvent(string name)
        {
            if (EventTable != null)
                return EventTable.GetEventSignal(name);

            return null;
        }

#if UNITY_EDITOR
        public abstract bool IsListenEvent(string eventName);
#endif
    }
}
