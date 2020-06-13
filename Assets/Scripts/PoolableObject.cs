using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZUI
{
    public abstract class PoolableObject<T> where T : PoolableObject<T>, new()
    {
        protected abstract void Reset();
        private static Stack<T> m_Objects = null;

        internal static T Get()
        {
            if (m_Objects == null || m_Objects.Count == 0)
                return new T();
            else
                return m_Objects.Pop();
        }

        internal static void Release(T obj)
        {
            obj.Reset();
            if (m_Objects == null)
                m_Objects = new Stack<T>();
            m_Objects.Push(obj);
        }
    }
}
