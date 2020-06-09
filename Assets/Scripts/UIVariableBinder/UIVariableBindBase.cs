using UnityEngine;

namespace TZUI
{
    public abstract class UIVariableBindBase : MonoBehaviour
    {
        [SerializeField]
        private IVariableTable m_VariableTable;

        internal IVariableTable VariableTable { get; private set; }

        protected void Awake()
        {
            RefreshVariableTable();
            UnbindVariables();
        }

        protected void OnDestroy()
        {
            UnbindVariables();
        }

        private void RefreshVariableTable()
        {
            if (m_VariableTable == null)
                m_VariableTable = this.GetComponentInParentHard<IVariableTable>();

            VariableTable = m_VariableTable;
        }

        internal UIVariable FindVariable(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (VariableTable != null)
                return VariableTable.FindVariable(name);

            return null;
        }

        protected abstract void BindVariables();
        protected abstract void UnbindVariables();
    }
}
