using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InspectorDisplayNameAttribute))]
public class InspectorDisplayNameDrawer : PropertyDrawer
{
    private GUIContent _label = null;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_label == null)
        {
            string name = (attribute as InspectorDisplayNameAttribute).displayName;
            _label = new GUIContent(name);
        }

        EditorGUI.PropertyField(position, property, _label);
    }
}
