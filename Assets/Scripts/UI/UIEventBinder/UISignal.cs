using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZUI
{
    public class UISignal : PoolableObject<UISignal>
    {
        internal UIEventDelegate Delegates;
        protected override void Reset()
        {
            Delegates = null;
        }
        internal void Invoke(params object[] args)
        {
            Delegates?.Invoke(args);
        }
        internal void Invoke()
        {
            Delegates?.Invoke();
        }
    }
    public delegate void UIEventDelegate(params object[] args);
}
