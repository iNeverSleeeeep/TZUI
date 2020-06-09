using UnityEngine;
using UnityEngine.UI;

namespace TZUI
{
    public sealed class UIVariableBindText : UIVariableBindBase
    {
        [SerializeField]
        private string m_VariableName;

        private UIVariable m_Variable;

        private Text m_Text;

        private void OnValueChanged()
        {
            if (m_Text = null)
                m_Text = GetComponent<Text>();

            if (m_Text != null)
            {
                m_Text.enabled = true;
                m_Text.text = m_Variable.GetString();
            }
        }
        protected override void BindVariables()
        {
            if (string.IsNullOrEmpty(m_VariableName))
                return;
            m_Variable = FindVariable(m_VariableName);
            m_Variable.OnValueChanged += OnValueChanged;
            OnValueChanged();
        }

        /// <inheritdoc/>
        protected override void UnbindVariables()
        {
            if (m_Variable != null)
            {
                m_Variable.OnValueChanged -= OnValueChanged;
                m_Variable = null;
            }
        }
    }
}
