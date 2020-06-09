using UnityEngine;

namespace TZUI
{
    internal abstract class UIEventBindBase : MonoBehaviour
    {
        [SerializeField]
        private IEventTable m_EventTable;

        internal IEventTable EventTable { get; private set; }


        protected void Awake()
        {
            RefreshEventTable();
        }

        private void RefreshEventTable()
        {
            if (m_EventTable == null)
                m_EventTable = this.GetComponentInParentHard<IEventTable>();

            EventTable = m_EventTable;
        }

        internal UISignal FindEvent(string name)
        {
            if (EventTable != null)
                return EventTable.GetEventSignal(name);

            return null;
        }
    }
}
