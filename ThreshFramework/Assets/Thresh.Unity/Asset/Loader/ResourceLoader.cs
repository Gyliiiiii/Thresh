using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Thresh.Unity.Asset.Loader
{
    public class ResourceLoader : MonoBehaviour,IAssetLoader
    {
        private Dictionary<string, AssetCache> _RefCaches = null;

        public bool FindCache(string asset_path)
        {
            throw new NotImplementedException();
        }

        public bool IsLoading(string asset_path)
        {
            throw new NotImplementedException();
        }

        public void LoadAsset<T>(string asset_path, Action<T> load_finish) where T : Object
        {
            throw new NotImplementedException();
        }

        public void LoadScene(string scene_path, Action<bool, float> load_finish)
        {
            throw new NotImplementedException();
        }
    }
}