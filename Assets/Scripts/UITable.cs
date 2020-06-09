using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;

namespace TZUI
{
    public class UITable : MonoBehaviour, IObjectTable, IVariableTable, IEventTable
    {
        #region Object Table
        [SerializeField] private UnityObject[] m_ObjectBinds;
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
                m_Binds = null;
#endif
                }

                return m_ObjectTable;
            }
        }

        #endregion

        #region Event Table

        [SerializeField]
        private string[] m_Events;
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
                    m_EventTable = null;
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
        #endregion

        #region Variable Table
        [SerializeField]
        private UIVariable[] m_VariableBinds;
        private Dictionary<string, UIVariable> m_VariableTable;
        private Dictionary<string, UIVariable> VariableTable
        {
            get
            {
                if (m_VariableTable == null)
                {
                    m_VariableTable = new Dictionary<string, UIVariable>(StringComparer.Ordinal);
                    foreach (var v in this.m_VariableBinds)
                        m_VariableTable.Add(v.Name, v);
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
    }

    internal interface IObjectTable
    {

    }

    internal interface IVariableTable
    {
        UIVariable FindVariable(string name);
    }

    internal interface IEventTable
    {
        UISignal GetEventSignal(string name);
    }
}

