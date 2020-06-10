using UnityEngine;
using UnityEditor;

namespace TZUI
{
    [CustomEditor(typeof(UIVariableBindText))]
    [CanEditMultipleObjects]
    public class UIVariableBindTextInspector : UIVariableBindBaseInspector
    {
        SerializedProperty m_VariableName;

        private new void OnEnable()
        {
            base.OnEnable();
            m_VariableName = serializedObject.FindProperty("m_VariableName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            DrawVariable(m_VariableName, "监听变量：");

            serializedObject.ApplyModifiedProperties();
        }
    }
}