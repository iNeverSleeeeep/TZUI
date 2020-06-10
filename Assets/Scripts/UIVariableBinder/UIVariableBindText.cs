using UnityEngine;
using UnityEngine.UI;

namespace TZUI
{
    [RequireComponent(typeof(Text))]
    public sealed class UIVariableBindText : UIVariableBindBase
    {
        [SerializeField]
        private string m_VariableName;

        private UIVariable m_Variable;

        private Text m_Text;

        protected override void OnValueChanged()
        {
            if (m_Text == null)
                m_Text = GetComponent<Text>();

            if (m_Text != null)
            {
                string text = null;
                if (m_Variable != null)
                    text = m_Variable.GetString();
                if (string.IsNullOrEmpty(text) == false)
                {
                    m_Text.canvasRenderer.cull = false;
                    m_Text.text = text;
                }
                else
                {
                    m_Text.canvasRenderer.cull = true;
                }
            }
        }
        protected override void BindVariables()
        {
            if (string.IsNullOrEmpty(m_VariableName))
                return;
            m_Variable = FindVariable(m_VariableName);
            if (m_Variable != null)
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

#if UNITY_EDITOR
        public override bool IsVariableBinded(string name)
        {
            return m_VariableName == name;
        }
#endif
    }
}
