using System;
using System.Collections;
using System.Collections.Generic;
using Thresh.Unity.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Thresh.Unity.Asset.Loader
{
    public class ResourceLoader : MonoBehaviour,IAssetLoader
    {
        private Dictionary<string, AssetCache> _RefCaches = null;

        private Dictionary<string, List<IAssetLoadRequest>> _LoadRequests = null;

        private HashSet<string> _LoadingCaches = null;

        private const float LOAD_FINISH_FLAG = 0.9f;

        private void Awake()
        {
            _RefCaches = new Dictionary<string, AssetCache>();
            _LoadingCaches = new HashSet<string>();
            _LoadRequests = new Dictionary<string, List<IAssetLoadRequest>>();
        }

        void AddRequest(IAssetLoadRequest request)
        {
            List<IAssetLoadRequest> requests = null;
            if (!_LoadRequests.TryGetValue(request.AssetPath,out requests))
            {
                requests = new List<IAssetLoadRequest>();
                _LoadRequests.Add(request.AssetPath,requests);
            }
            
            requests.Add(request);
        }

        public bool FindCache(string asset_path)
        {
            return _RefCaches.ContainsKey(asset_path);
        }

        public bool IsLoading(string asset_path)
        {
            return _LoadingCaches.Contains(asset_path);
        }

        public void LoadAsset<T>(string asset_path, Action<T> load_finish) where T : Object
        {
            if (FindCache(asset_path))
            {
                load_finish((T)_RefCaches[asset_path].Asset);
            }

            AssetLoadRequest<T> request = new AssetLoadRequest<T>(asset_path, load_finish);
            AddRequest(request);

            if (!FindCache(asset_path) && !IsLoading(asset_path))
            {
                _LoadingCaches.Add(asset_path);
                StartCoroutine(OnLoadAsset<T>(asset_path));
            }
        }

        IEnumerator OnLoadAsset<T>(string asset_path) where T : UnityEngine.Object
        {
            ResourceRequest load_request = Resources.LoadAsync(asset_path);
            yield return load_request;

            List<IAssetLoadRequest> requests = null;
            if (!_LoadRequests.TryGetValue(asset_path,out requests))
            {
                yield break;
            }

            for (int i = 0; i < requests.Count; i++)
            {
                AssetLoadRequest<T> request = (AssetLoadRequest<T>)requests[i];
                if (request != null)
                {
                    request.OnFinished((T)load_request.asset);
                }
            }
            
            requests.Clear();
            _LoadRequests.Remove(asset_path);

            AssetCache cache = new AssetCache(load_request.asset);
            _RefCaches.Add(asset_path,cache);
        }

        public void LoadScene(string scene_path, Action<bool, float> load_finish)
        {
            SceneLoadRequest request = new SceneLoadRequest(scene_path, load_finish);
            AddRequest(request);

            if (IsLoading(scene_path))
            {
                return;
            }
            
            StartCoroutine(OnLoadScene(scene_path));
        }

        IEnumerator OnLoadScene(string scene_path)
        {
            string scene_name = BundleUtil.GetAssetName(scene_path);

            AsyncOperation scene = SceneManager.LoadSceneAsync(scene_name);

            List<IAssetLoadRequest> requests = null;
            if (!_LoadRequests.TryGetValue(scene_path,out requests))
            {
                yield break;
            }

            while (!scene.isDone)
            {
                for (int i = 0; i < requests.Count; i++)
                {
                    SceneLoadRequest request = (SceneLoadRequest)requests[i];
                    if (request != null)
                    {
                        request.OnFinished(false, scene.progress);
                    }
                }

                yield return null;
            }

            for (int i = 0; i < requests.Count; i++)
            {
                SceneLoadRequest request = (SceneLoadRequest)requests[i];
                if (request != null)
                {
                    request.OnFinished(true, 1.0f);
                }
            }
            
            requests.Clear();
            _LoadRequests.Remove(scene_path);
        }
    }
}