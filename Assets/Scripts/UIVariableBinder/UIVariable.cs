using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TZUI
{
    public class UIVariable
    {
        internal string Name;
        
        [SerializeField] private UIVariableType m_Type;

        [SerializeField] private bool m_BooleanValue;

        [SerializeField] private long m_IntegerValue;

        [SerializeField] private float m_FloatValue;

        [SerializeField] private string m_StringValue;
        
        public string GetString()
        {
            return m_StringValue;
        }

        public event Action OnValueChanged;
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
