using System;
using System.Collections;
using System.Collections.Generic;
using DoozyUI;
using Thresh.Core.Variant;
using Thresh.Unity.Asset;
using Thresh.Unity.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Thresh.Unity.UI
{
    public delegate void UIDelegate(PanelBase panel);
    public class UIEngine :SingletonEngine<UIEngine>,IEngine
    {
        public bool InstantAction { get; private set; }
        
        public bool ShouldDisable { get; private set; }

        private Dictionary<string, PanelBase> _PanelDic;
        private List<PanelBase> TopList;

        public void AddTopList(PanelBase panel)
        {
            TopList.Add(panel);
        }

        public void RemoveTopList(PanelBase panel)
        {
            TopList.Remove(panel);
        }

        public override void Awake()
        {
            base.Awake();

            _PanelDic = new Dictionary<string, PanelBase>();
            TopList = new List<PanelBase>();
        }

        protected override IEnumerator Shutdown()
        {
            yield return new WaitForFixedUpdate();
        }

        protected override IEnumerator Startup()
        {
            ResourceRequest request = Resources.LoadAsync("prefab/DoozyUI");
            while (!request.isDone)
            {
                yield return null;
            }
            
            GameObject go = GameObject.Instantiate(request.asset,Vector3.zero,Quaternion.identity) as GameObject;
            _UIRoot = go.transform;
            _UIRoot.name = "UI_Root";
            
            _UIRoot.SetParent(transform);

            _UIContainer = _UIRoot.Find("UI Container");

            yield return new WaitForFixedUpdate();
        }
        
        private Transform _UIRoot { get; set; }
        
        private Transform _UIContainer { get; set; }

        public PANEL ShowPanel<PANEL>(string panel_path, UIDelegate callback = null) where PANEL : PanelBase
        {
            PANEL panel = GetPanel<PANEL>();
            if (panel == null)
            {
                panel = CreatePanel<PANEL>();
                BindPanel(panel,panel_path,callback);
            }
            else
            {
                panel.Show();
                if (callback != null)
                {
                    callback(panel);
                }

                for (int i = 0; i < TopList.Count; i++)
                {
                    TopList[i].PanelObject.transform.SetAsLastSibling();
                }
            }

            return panel;
        }

        public PANEL NewPanel<PANEL>(string panel_path) where PANEL : PanelBase
        {
            Type t = typeof(PANEL);
            string name = t.Name;

            string ui_path = name;

            PanelBase panel = null;

            panel = Activator.CreateInstance<PANEL>();
            panel.Name = name;
            
            BindPanelImmu(panel,panel_path);

            return panel as PANEL;
        }

        public void DestroyPanel(PanelBase panel_path)
        {
            panel_path.Destroy();
            Destroy(panel_path.PanelObject);
        }

        public bool IsShow<PANEL>() where PANEL : PanelBase
        {
            PANEL panel = GetPanel<PANEL>();
            if (panel == null)
            {
                return false;
            }
            else
            {
                return panel.IsShow;
            }
        }

        public bool HidePanel<PANEL>() where PANEL : PanelBase
        {
            PANEL panel = GetPanel<PANEL>();
            if (panel == null)
            {
                return false;
            }
            
            panel.Hide();

            return true;
        }

        public bool HidePanel(PanelBase panel)
        {
            if (panel == null)
            {
                return false;
            }
            panel.Hide();

            return true;
        }

        private void BindPanelImmu(PanelBase panel, string panel_path)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("ui/" + panel_path));
            go.transform.SetRectParent(_UIContainer.gameObject);
            RectTransform transform = go.transform as RectTransform;
            if (transform.anchorMin == Vector2.zero && transform.anchorMax == Vector2.one)
            {
                transform.offsetMin = Vector2.zero;
                transform.offsetMax = Vector2.zero;
            }
            else
            {
                go.transform.localPosition = Vector3.zero;
            }

            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            go.name = "ui/" + panel_path;

            UIElement uiElement = go.GetComponent<UIElement>();
            if (uiElement)
            {
                panel.UIElement = uiElement;
            }

            panel.PanelObject = go;
            CommonInitPanel(panel);
            panel.Craete();
            
            panel.Show();
        }
        
        

        private void BindPanel(PanelBase panel, string panel_path, UIDelegate callback = null)
        {
            UIDelegate cb = callback;
            AssetEngine.Instance.CreateObject("ui/"+panel_path, (go) =>
            {
                go.transform.SetRectParent(_UIContainer.gameObject);
                RectTransform transform = go.transform as RectTransform;
                if (transform.anchorMin == Vector2.zero && transform.anchorMax == Vector2.one)
                {
                    transform.offsetMin = Vector2.zero;
                    transform.offsetMax = Vector2.zero;
                }
                else
                {
                    go.transform.localPosition = Vector3.zero;
                }
                go.transform.localRotation = Quaternion.identity;
                go.transform.localScale = Vector3.zero;
                go.name = "ui/" + panel_path;

                UIElement uiElement = go.GetComponent<UIElement>();
                if (uiElement)
                {
                    panel.UIElement = uiElement;
                }

                panel.PanelObject = go;

                CommonInitPanel(panel);
                
                panel.Craete();
                
                panel.Show();
                if (cb != null) cb(panel);
                for (int i = 0; i < TopList.Count; i++)
                {
                    TopList[i].PanelObject.transform.SetAsLastSibling();    
                }
            });
        }

        void CommonInitPanel(PanelBase panel)
        {
            PanelBase tpanel = panel;
            Button close = UnityUtil.GetComponent<Button>(panel.PanelObject, "close");
            if (close != null)
            {
                close.onClick.AddListener(() =>
                {
                    tpanel.Hide();
                });
            }

            Button mask = UnityUtil.GetComponent<Button>(panel.PanelObject, "mask");
            if (mask != null)
            {
                mask.onClick.AddListener(() =>
                {
                    tpanel.Hide();
                });
            }
        }

        private PANEL CreatePanel<PANEL>() where PANEL : PanelBase
        {
            Type t = typeof(PANEL);
            string name = t.Name;

            string ui_path = name;

            PanelBase panel = null;
            if (_PanelDic.TryGetValue(name,out panel))
            {
                return panel as PANEL;
            }

            panel = Activator.CreateInstance<PANEL>();
            panel.Name = name;
            
            _PanelDic.Add(name,panel);

            return panel as PANEL;
        }

        public PANEL GetPanel<PANEL>() where PANEL : PanelBase
        {
            Type t = typeof(PANEL);
            string name = t.Name;

            PanelBase panel = null;
            if (!_PanelDic.TryGetValue(name,out panel))
            {
                return null;
            }

            return panel as PANEL;
        }

        public void OnRecvGameEvent(string gameEvent, VariantList param)
        {
            foreach (var panel in _PanelDic.Values)
            {
                if (!panel.IsShow)
                {
                    continue;
                }
                
                panel.OnEvent(gameEvent,param);
            }
        }
        
    }
}