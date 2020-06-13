using UnityEngine;
using UnityEditor;

namespace TZUI
{
    public class UIVariableBindBaseInspector : Editor
    {
        protected UIMaster m_VariableTable;
        static GUIContent VariableTableContent = new GUIContent("VariableTable");
        SerializedProperty m_VariableTableProperty;

        protected void OnEnable()
        {
            m_VariableTableProperty = serializedObject.FindProperty("m_VariableTable");
            foreach (var t in targets)
            {
                var binder = t as UIVariableBindBase;
                var table = binder.VariableTable;
                if (m_VariableTable == null)
                    m_VariableTable = table as UIMaster;
                else if (m_VariableTable != table as UIMaster)
                {
                    m_VariableTable = null;
                    break;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_VariableTableProperty, VariableTableContent);
        }

        protected void DrawVariable(SerializedProperty variableProperty, string label)
        {
            if (m_VariableTable == null)
                return;
            var variables = m_VariableTable.Variables;
            var index = variables.IndexOf(variableProperty.stringValue);
            index = EditorGUILayout.Popup(label, index, variables.ToArray());
            if (index >= 0)
                variableProperty.stringValue = variables[index];
        }
    }
}
