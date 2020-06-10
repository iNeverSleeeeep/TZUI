using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;

namespace TZUI
{
    [CustomPropertyDrawer(typeof(UIBinder))]
    public class UIBinderDrawer : PropertyDrawer
    {
        private bool m_ObjectTableUnfolded = false;
        private bool m_VariableTableUnfolded = false;
        private bool m_EventTableUnfolded = false;

        private SerializedProperty m_ObjectBinds;
        private SerializedProperty m_Events;
        private SerializedProperty m_VariableBinds;

        private ReorderableList m_ObjectList;
        private ReorderableList m_EventsList;
        private ReorderableList m_VariableList;

        SerializedProperty property;

        private bool m_Inited = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            this.property = property;
            if (m_Inited == false)
            {
                InitUIBinds();
                m_Inited = true;
            }
            DrawUIBinds(position);
        }
        private void InitUIBinds()
        {
            AddCallbackDelegate onAddCallback = list =>
            {
                list.serializedProperty.arraySize++;
                var element = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);
                if (element.propertyType == SerializedPropertyType.String)
                    element.stringValue = null;
                else if (element.propertyType == SerializedPropertyType.ObjectReference)
                    element.objectReferenceValue = null;
            };

            RemoveCallbackDelegate onRemoveCallback = list =>
            {
                list.serializedProperty.DeleteArrayElementAtIndex(list.index);
            };

            m_ObjectBinds = property.FindPropertyRelative("m_ObjectBinds");
            m_ObjectList = new ReorderableList(property.serializedObject, m_ObjectBinds);
            m_ObjectList.headerHeight = 0;
            m_ObjectList.draggable = false;
            m_ObjectList.onAddCallback = onAddCallback;
            m_ObjectList.onRemoveCallback = onRemoveCallback;
            m_ObjectList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = m_ObjectBinds.GetArrayElementAtIndex(index);
                element.objectReferenceValue = EditorGUI.ObjectField(rect, element.objectReferenceValue, typeof(MonoBehaviour), allowSceneObjects: true);
            };

            m_Events = property.FindPropertyRelative("m_Events");
            m_EventsList = new ReorderableList(property.serializedObject, m_Events);
            m_EventsList.headerHeight = 0;
            m_EventsList.draggable = false;
            m_EventsList.onAddCallback = onAddCallback;
            m_EventsList.onRemoveCallback = onRemoveCallback;
            m_EventsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = m_Events.GetArrayElementAtIndex(index);
                element.stringValue = EditorGUI.TextField(rect, element.stringValue);
            };
            m_EventsList.onSelectCallback = list =>
            {
                var element = m_Events.GetArrayElementAtIndex(list.index);
                var table = property.serializedObject.targetObject as UIMaster;
                var binders = table.GetComponentsInChildren<UIEventBindBase>(true);
                var eventName = element.stringValue;
                if (string.IsNullOrEmpty(eventName))
                    return;
                foreach (var binder in binders)
                {
                    if (binder.EventTable == table && binder.IsListenEvent(eventName))
                    {
                        Debug.Log(string.Format("EventTable:{2}, 监听事件:{0}, 节点名:{1}", eventName, binder.ToString(), table.ToString()), binder);
                    }
                }
            };

            m_VariableBinds = property.FindPropertyRelative("m_VariableBinds");
            m_VariableList = new ReorderableList(property.serializedObject, m_VariableBinds);
            m_VariableList.headerHeight = 0;
            m_VariableList.elementHeight *= 2;
            m_VariableList.draggable = false;
            m_VariableList.onAddCallback = onAddCallback;
            m_VariableList.onRemoveCallback = onRemoveCallback;
            m_VariableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = m_VariableBinds.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element);
            };
            m_VariableList.onSelectCallback = list =>
            {
                var element = m_VariableBinds.GetArrayElementAtIndex(list.index);
                var table = property.serializedObject.targetObject as UIMaster;
                var binders = table.GetComponentsInChildren<UIVariableBindBase>(true);
                var variableName = element.FindPropertyRelative("Name").stringValue;
                if (string.IsNullOrEmpty(variableName))
                    return;
                foreach (var binder in binders)
                {
                    if (binder.VariableTable == table && binder.IsVariableBinded(variableName))
                    {
                        Debug.Log(string.Format("VariableTable:{2}, 监听变量:{0}, 节点名:{1}", variableName, binder.ToString(), table.ToString()), binder);
                    }
                }
            };
        }

        private void DrawUIBinds(Rect position)
        {
            var left = position; left.width /= 2;
            var middle = position; middle.width /= 4; middle.x += position.width / 2;
            var right = position; right.width /= 4; right.x += position.width / 4 * 3;
            EditorGUI.LabelField(left, "UI绑定");
            if (GUI.Button(middle, "全部折叠"))
            {
                m_ObjectTableUnfolded = false;
                m_VariableTableUnfolded = false;
                m_EventTableUnfolded = false;
            }
            if (GUI.Button(right, "全部展开"))
            {
                m_ObjectTableUnfolded = true;
                m_VariableTableUnfolded = true;
                m_EventTableUnfolded = true;
            }

            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUI.indentLevel++;

            DrawObjectTable();
            DrawVariableTable();
            DrawEventTable();

            EditorGUI.indentLevel--;


            EditorGUILayout.EndVertical();
        }

        private void DrawObjectTable()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            m_ObjectTableUnfolded = EditorGUILayout.Foldout(m_ObjectTableUnfolded, string.Format("Object Table ({0})", m_ObjectBinds.arraySize));

            EditorGUILayout.EndHorizontal();
            if (m_ObjectTableUnfolded)
                m_ObjectList.DoLayoutList();
            EditorGUILayout.EndVertical();
        }

        private void DrawVariableTable()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            m_VariableTableUnfolded = EditorGUILayout.Foldout(m_VariableTableUnfolded, string.Format("Variable Table ({0})", m_VariableBinds.arraySize));

            EditorGUILayout.EndHorizontal();
            if (m_VariableTableUnfolded)
                m_VariableList.DoLayoutList();
            EditorGUILayout.EndVertical();
        }

        private void DrawEventTable()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            m_EventTableUnfolded = EditorGUILayout.Foldout(m_EventTableUnfolded, string.Format("Event Table ({0})", m_Events.arraySize));

            EditorGUILayout.EndHorizontal();
            if (m_EventTableUnfolded)
                m_EventsList.DoLayoutList();
            EditorGUILayout.EndVertical();
        }
    }
}