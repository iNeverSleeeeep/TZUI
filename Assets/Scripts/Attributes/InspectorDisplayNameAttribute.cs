using UnityEngine;

public class InspectorDisplayNameAttribute : PropertyAttribute
{
    public string displayName;

    public InspectorDisplayNameAttribute(string displayName)
    {
        this.displayName = displayName;
    }
}