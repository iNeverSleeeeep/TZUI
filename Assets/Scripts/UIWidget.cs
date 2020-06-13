using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZUI
{
    public abstract class UIWidget : UINode
    {
        public abstract string WidgetName { get; }
    }
}
