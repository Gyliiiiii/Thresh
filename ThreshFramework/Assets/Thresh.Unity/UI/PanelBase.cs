using DoozyUI;
using Thresh.Core.Variant;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Thresh.Unity.UI
{
    public abstract class PanelBase
    {
        /// <summary>
        /// panel被创建
        /// </summary>
        protected abstract void OnCreate();

        /// <summary>
        /// panel被销毁
        /// </summary>
        protected abstract void OnDestroy();

        /// <summary>
        /// panel被显示
        /// </summary>
        protected abstract void OnShow();

        /// <summary>
        /// panel被隐藏
        /// </summary>
        protected abstract void OnHide();

        public virtual void OnEvent(string event_id, VariantList param)
        {
        }

        public string Name { get; set; }

        public GameObject PanelObject { get; set; }

        public UIElement UIElement { get; set; }

        public bool IsShow { get; private set; }

        private void RegisterButtonClickMusic()
        {
            Button[] btnList = PanelObject.GetComponentsInChildren<Button>();

            foreach (var btn in btnList)
            {
                if (btn != null)
                {
                    btn.onClick.AddListener(() =>
                    {
                    });
                }
            }

            Toggle[] togList = PanelObject.GetComponentsInChildren<Toggle>();
            foreach (var tog in togList)
            {
                if (tog != null)
                {
                    tog.onValueChanged.AddListener((isOn) =>
                    {
                    });
                }
            }
        }

        internal void Craete()
        {
            RegisterButtonClickMusic();
            
            OnCreate();
        }

        internal void Show()
        {
            if (IsShow) return;
            if (PanelObject == null) return;

            IsShow = true;

            if (UIElement != null)
            {
                UIElement.Show(false);
            }
            else
            {
                PanelObject.SetActive(true);
            }
            
            PanelObject.transform.SetAsLastSibling();
            OnShow();
        }

        internal void Hide() 
        {
            if (!IsShow) return;
            if (PanelObject == null) return;
            
            IsShow = false;
            if (UIElement != null)
            {
                UIElement.Hide(UIEngine.Instance.InstantAction,UIEngine.Instance.ShouldDisable);
            }
            else
            {
                PanelObject.SetActive(false);
            }

            OnHide();
        }

        private bool destroy = false;

        internal void Destroy()
        {
            if (destroy) return;
            destroy = true;
            OnDestroy();
        }
    }
}