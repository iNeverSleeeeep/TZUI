using UnityEngine;
using UnityEditor;
using System;

namespace TZUI
{
    [CustomPropertyDrawer(typeof(UIVariable))]
    public class UIVariableDrawer : PropertyDrawer
    {
        static GUIContent DefaultText = new GUIContent("默认值:");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var name = property.FindPropertyRelative("Name");
            var type = property.FindPropertyRelative("m_Type");
            var left = position; left.width /= 2; left.height /= 2;
            var right = position; right.width /= 2; right.x += right.width; right.height /= 2;
            var bot = position; bot.height /= 2; bot.y += bot.height;
            name.stringValue = EditorGUI.TextField(left, name.stringValue);
            type.enumValueIndex = EditorGUI.Popup(right, type.enumValueIndex, type.enumDisplayNames);


            if (Enum.TryParse(type.enumNames[type.enumValueIndex], out UIVariableType enumType))
            {
                switch (enumType)
                {
                    case UIVariableType.Boolean:
                        EditorGUI.PropertyField(bot, property.FindPropertyRelative("m_BooleanValue"), DefaultText);
                        break;
                    case UIVariableType.Float:
                        EditorGUI.PropertyField(bot, property.FindPropertyRelative("m_FloatValue"), DefaultText);
                        break;
                    case UIVariableType.Integer:
                        EditorGUI.PropertyField(bot, property.FindPropertyRelative("m_IntegerValue"), DefaultText);
                        break;
                    case UIVariableType.String:
                        EditorGUI.PropertyField(bot, property.FindPropertyRelative("m_StringValue"), DefaultText);
                        break;
                }
            }
        }
    }
}