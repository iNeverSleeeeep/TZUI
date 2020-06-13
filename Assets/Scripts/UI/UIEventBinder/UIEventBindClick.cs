using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TZUI
{
    public class UIEventBindClick : UIEventBindBase, IPointerClickHandler
    {
        [SerializeField]
        private string m_EventName = null;

        private Selectable m_Selectable;

        private UISignal m_ClickSignal;
        private UISignal ClickSignal
        {
            get
            {
                if (m_ClickSignal == null)
                    m_ClickSignal = FindEvent(m_EventName);
                return m_ClickSignal;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_Selectable != null && m_Selectable.interactable == false)
                return;

            ClickSignal?.Invoke();
        }

        private new void Awake()
        {
            base.Awake();
            m_Selectable = GetComponent<Selectable>();
        }

#if UNITY_EDITOR
        public override bool IsListenEvent(string eventName)
        {
            return m_EventName == eventName;
        }
#endif
    }
}
