using System;
using System.Collections;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using Thresh.Unity.Asset.Loader;
using Thresh.Unity.Global;
using Thresh.Unity.Utility;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Thresh.Unity.Asset
{
    public class AssetEngine : SingletonEngine<AssetEngine>, IEngine
    {
        public const string PAK_CONFIG = "config";
        public const string PAK_LUA = "lua";
        public const string PAK_BUNDLE = "bundle";

        private int _CurLoadedPaks = 0;
        private int _MaxLoadedPaks = 3;

        public IAssetLoader Loader { get; private set; }

        public override void Awake()
        {
            base.Awake();
        }

        public void DownloadFile()
        {
        }

        private void InstallPak(string pak)
        {
            string config_path = "";
            Debug.Log(pak);

            if (Application.platform == RuntimePlatform.Android)
            {
                config_path = AssetPath.StreamingPath + pak;
            }
            else
            {
                config_path = "file://" + AssetPath.StreamingPath + pak;
            }

            ObservableWWW.GetAndGetBytes(config_path).Subscribe(
                bytes =>
                {
                    MemoryStream _AssetStream = new MemoryStream(bytes);
                    FileUtil.UnZip(_AssetStream, AssetPath.PersistentDataPath + pak, "", true);

                    _CurLoadedPaks++;
                }
            );
        }

        protected override IEnumerator Startup()
        {
            ZipConstants.DefaultCodePage = Encoding.UTF8.CodePage;
            InstallPak(PAK_CONFIG);
            InstallPak(PAK_LUA);
            InstallPak(PAK_BUNDLE);

            while (_CurLoadedPaks != _MaxLoadedPaks)
            {
                yield return null;
            }

            GameObject loader = new GameObject("Loader");
            loader.transform.parent = transform;
            Loader = loader.AddComponent<ResourceLoader>();
            yield return new WaitForFixedUpdate();

            GameObject pool = new GameObject("Pool");
            pool.transform.parent = transform;
            pool.AddComponent<FastPoolManager>();
            yield return new WaitForFixedUpdate();
        }

        protected override IEnumerator Shutdown()
        {
            yield return new WaitForFixedUpdate();
        }

        public void LoadAsset<T>(string asset_path, Action<T> load_finish) where T : Object
        {
            Loader.LoadAsset<T>(asset_path, (asset) =>
            {
                load_finish(asset);
            });
        }

        public void LoadScene(string scene_path, Action<bool, float> load_finish)
        {
            Loader.LoadScene(scene_path, (is_done, progress) =>
            {
                load_finish(is_done, progress);
            });
        }

        public void CreateObject(string asset_path, Action<GameObject> load_finish)
        {
            Loader.LoadAsset<Object>(asset_path, (asset) =>
            {
                GameObject init = Resources.Load<GameObject>(asset_path);
                GameObject child = GameObject.Instantiate(init);

                load_finish(child);
            });
        }
        
        
    }
}