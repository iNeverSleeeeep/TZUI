using System;
using UnityEngine;

namespace TZUI
{
    public static class ComponentExtensions
    {
        public static Component GetOrAddComponent(this Component obj, Type type)
        {
            Component component = obj.GetComponent(type);
            if (component == null)
            {
                component = obj.gameObject.AddComponent(type);
            }
            return component;
        }
        public static T GetOrAddComponent<T>(this Component obj) where T : Component
        {
            T t = obj.GetComponent<T>();
            if (t == null)
            {
                t = obj.gameObject.AddComponent<T>();
            }
            return t;
        }
        public static bool HasComponent(this Component obj, Type type)
        {
            return obj.GetComponent(type) != null;
        }
        public static bool HasComponent<T>(this Component obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }
        public static T GetComponentInParentHard<T>(this Component obj)
        {
            Transform transform = obj.transform;
            while (transform != null)
            {
                T component = transform.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
                transform = transform.parent;
            }
            return (T)((object)null);
        }
    }
}

