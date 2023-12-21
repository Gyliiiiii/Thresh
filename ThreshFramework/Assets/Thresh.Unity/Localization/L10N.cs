using System.Collections.Generic;
using Thresh.Core.Config.XML;
using Thresh.Unity.Global;
using Thresh.Unity.Utility;
using UnityEngine;

namespace Thresh.Unity.Localization
{
    class L10N
    {
        private const string _NULL_LOCALIZATION = "unknow_localization";
        private Dictionary<string, string> _KeyValueTexts = null;
        public SystemLanguage Language { get; private set; }

        public L10N(SystemLanguage language)
        {
            Language = language;
            _KeyValueTexts = new Dictionary<string, string>();
        }

        public void LoadTexts()
        {
            string text = FileUtil.ReadString(AssetPath.LocalizationPath + Language + ".language");
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            XMLParse.XMLDocument xml = new XMLParse.XMLDocument(text);
            foreach (var xml_node in xml.RootNode.SubNodes)
            {
                string key = xml_node.GetValue("name");
                string value = xml_node.GetValue("text");

                if (_KeyValueTexts.ContainsKey(key))
                {
                    continue;
                }
                
                _KeyValueTexts.Add(key,value);
            }
        }

        public string GetText(string key)
        {
            string value = "";

            if (_KeyValueTexts.TryGetValue(key,out value))
            {
                return value;
            }

            return string.IsNullOrEmpty(key) ? _NULL_LOCALIZATION : key;
        }
    }
}