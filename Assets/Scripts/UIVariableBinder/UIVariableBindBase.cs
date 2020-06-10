using UnityEngine;

namespace TZUI
{
    [ExecuteInEditMode]
    public abstract class UIVariableBindBase : MonoBehaviour
    {
        [SerializeField]
        private UIMaster m_VariableTable;

        public UIMaster VariableTable
        {
            get
            {
                return m_VariableTable;
            }
        }

        protected void Awake()
        {
            RefreshVariableTable();
            BindVariables();
        }

        protected void OnDestroy()
        {
            UnbindVariables();
        }

        private void RefreshVariableTable()
        {
            if (m_VariableTable == null)
                m_VariableTable = this.GetComponentInParentHard<UIMaster>();
        }

        internal UIVariable FindVariable(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (m_VariableTable != null)
                return m_VariableTable.FindVariable(name);

            return null;
        }

        protected abstract void BindVariables();
        protected abstract void UnbindVariables();
        protected abstract void OnValueChanged();

#if UNITY_EDITOR
        public abstract bool IsVariableBinded(string name);

        private void OnValidate()
        {
            UnbindVariables();
            BindVariables();
        }
#endif
    }
}
