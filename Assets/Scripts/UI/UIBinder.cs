using System;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace TZUI
{
    [Serializable]
    public class UIBinder : IObjectTable, IVariableTable, IEventTable
    {
        #region Object Table
        [SerializeField] private UnityObject[] m_ObjectBinds = null;
        private Dictionary<string, UnityObject> m_ObjectTable;
        private Dictionary<string, UnityObject> ObjectTable
        {
            get
            {
                if (m_ObjectTable == null)
                {
                    m_ObjectTable = new Dictionary<string, UnityObject>(StringComparer.Ordinal);
                    foreach (var obj in m_ObjectBinds)
                        m_ObjectTable.Add(obj.name, obj);
#if !UNITY_EDITOR
                    m_ObjectBinds = null;
#endif
                }

                return m_ObjectTable;
            }
        }
        public UnityObject FindObject(string name)
        {
            if (ObjectTable.TryGetValue(name, out UnityObject obj))
                return obj;
            return null;
        }

        #endregion

        #region Event Table
        [SerializeField] private string[] m_Events = null;
#if UNITY_EDITOR
        public List<string> Events
        {
            get { return new List<string>(m_Events); }
        }
#endif
        private Dictionary<string, UISignal> m_EventTable;
        private Dictionary<string, UISignal> EventTable
        {
            get
            {
                if (m_EventTable == null)
                {
                    m_EventTable = new Dictionary<string, UISignal>(StringComparer.Ordinal);
                    foreach (var e in m_Events)
                        m_EventTable.Add(e, UISignal.Get());
#if !UNITY_EDITOR
                    m_Events = null;
#endif
                }

                return m_EventTable;
            }
        }

        public UISignal GetEventSignal(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (this.EventTable.TryGetValue(name, out UISignal signal))
                return signal;

            return null;
        }

        public void ListenEvent(string name, UIEventDelegate callback)
        {
            var signal = GetEventSignal(name);
            if (signal != null)
                signal.Delegates += callback;
        }

        public void ClearEvent(string name)
        {
            var signal = GetEventSignal(name);
            if (signal != null)
                signal.Delegates = null;
        }

        public void ClearAllEvents(string name)
        {
            foreach (var signal in EventTable)
                signal.Value.Delegates = null;
        }
        #endregion

        #region Variable Table
        [SerializeField] private UIVariable[] m_VariableBinds = null;
#if UNITY_EDITOR
        public List<string> Variables
        {
            get
            {
                var variables = new List<string>();
                foreach (var v in m_VariableBinds)
                    variables.Add(v.Name);
                return variables;
            }
        }
#endif
        private Dictionary<string, UIVariable> m_VariableTable = null;
        private Dictionary<string, UIVariable> VariableTable
        {
            get
            {
                if (m_VariableTable == null)
                {
                    m_VariableTable = new Dictionary<string, UIVariable>(StringComparer.Ordinal);
                    if (m_VariableBinds != null)
                    {
                        foreach (var v in m_VariableBinds)
                            m_VariableTable.Add(v.Name, v);
                    }
#if !UNITY_EDITOR
                    m_VariableBinds = null;
#endif
                }

                return m_VariableTable;
            }
        }

        public UIVariable FindVariable(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (VariableTable.TryGetValue(name, out UIVariable variable))
                return variable;

            return null;
        }
        #endregion

#if UNITY_EDITOR
        public void OnValidate()
        {
            foreach (var v in VariableTable.Values)
                v.ForceRefresh();
        }
#endif
    }

    public interface IObjectTable
    {
        UnityObject FindObject(string name);
    }

    public interface IVariableTable
    {
        UIVariable FindVariable(string name);
    }

    public interface IEventTable
    {
        UISignal GetEventSignal(string name);
        void ListenEvent(string name, UIEventDelegate callback);
        void ClearEvent(string name);
        void ClearAllEvents(string name);
    }
}

