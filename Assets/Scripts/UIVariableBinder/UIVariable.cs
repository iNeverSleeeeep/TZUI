using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TZUI
{
    [Serializable]
    public class UIVariable
    {
        [SerializeField] internal string Name;
        
        [SerializeField] private UIVariableType m_Type;

        [SerializeField] private bool m_BooleanValue;

        [SerializeField] private long m_IntegerValue;

        [SerializeField] private float m_FloatValue;

        [SerializeField] private string m_StringValue;
        
        public string GetString()
        {
            return m_StringValue;
        }

        public bool GetBool()
        {
            return m_BooleanValue;
        }

        public event Action OnValueChanged;

        public void ForceRefresh()
        {
            OnValueChanged?.Invoke();
        }

        public void SetString(string text)
        {
            Debug.Assert(m_Type == UIVariableType.String);
            m_StringValue = text;
            OnValueChanged?.Invoke();
        }

        public void SetBool(bool b)
        {
            Debug.Assert(m_Type == UIVariableType.Boolean);
            m_BooleanValue = b;
            OnValueChanged?.Invoke();
        }
    }

    public enum UIVariableType
    {
        Boolean = 0,
        
        Integer = 1,
        
        Float = 2,
        
        String = 3,
        
        Asset = 4,
        
        Color = 5,
    }
}
