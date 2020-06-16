using UnityEngine;
using UnityEditor;

namespace TZUI
{
    [CustomEditor(typeof(UIEventBindBase))]
    [CanEditMultipleObjects]
    public class UIEventBindBaseInspector : Editor
    {
        protected UINode m_EventTable;
        static GUIContent EventTableContent = new GUIContent("EventTable");
        SerializedProperty m_EventTableProperty;

        protected void OnEnable()
        {
            m_EventTableProperty = serializedObject.FindProperty("m_EventTable");
            foreach (var t in targets)
            {
                var binder = t as UIEventBindBase;
                var table = binder.EventTable;
                if (m_EventTable == null)
                    m_EventTable = table as UINode;
                else if (m_EventTable != table as UINode)
                {
                    m_EventTable = null;
                    break;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_EventTableProperty, EventTableContent);
        }

        protected void DrawEvent(SerializedProperty eventProperty, string label)
        {
            if (m_EventTable == null)
                return;
            var events = m_EventTable.Events;
            var index = events.IndexOf(eventProperty.stringValue);
            index = EditorGUILayout.Popup(label, index, events.ToArray());
            if (index >= 0)
                eventProperty.stringValue = events[index];
        }
    }
}