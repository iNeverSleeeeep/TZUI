using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace TZUI
{
    [CustomEditor(typeof(SimpleWidget))]
    class SimpleWidgetInspector : UIWidgetInspector
    {
        SerializedProperty m_WidgetIndex;

        protected new void OnEnable()
        {
            base.OnEnable();
            m_WidgetIndex = serializedObject.FindProperty("m_WidgetIndex");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var widgetNames = Enum.GetNames(typeof(SimpleWidgetName));
            var currentIndex = m_WidgetIndex.intValue;
            var newIndex = EditorGUILayout.Popup(currentIndex, widgetNames);
            if (newIndex != currentIndex)
            {
                m_WidgetIndex.intValue = newIndex;
                serializedObject.ApplyModifiedProperties();
            }
            base.OnInspectorGUI();
        }
    }
}
