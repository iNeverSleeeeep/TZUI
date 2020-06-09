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
    class UIBindClick : UIEventBindBase, IPointerClickHandler
    {
        [SerializeField]
        private string m_EventName;

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
    }
}
