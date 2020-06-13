using UnityEngine;
using UnityEditor;

namespace TZUI
{
    [CustomEditor(typeof(UIEventBindClick))]
    [CanEditMultipleObjects]
    public class UIEventBindClickInspector : UIEventBindBaseInspector
    {
        SerializedProperty m_EventName;

        private new void OnEnable()
        {
            base.OnEnable();
            m_EventName = serializedObject.FindProperty("m_EventName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            DrawEvent(m_EventName, "点击事件：");

            serializedObject.ApplyModifiedProperties();
        }
    }
}