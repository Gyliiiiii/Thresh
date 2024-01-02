using System.Collections;
using System.Collections.Generic;
using Thresh.Unity.Asset;
using Thresh.Unity.Stage;
using Thresh.Unity.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStage : StageBase
{
    public override IEnumerator OnLoad()
    {
        bool load_finish = false;
        AssetEngine.Instance.LoadScene("main", (is_done, progress) =>
        {
            load_finish = is_done;
        });

        while (!load_finish)
        {
            yield return null;
        }

        UIEngine.Instance.ShowPanel<MainPanel>("main_panel");
    }

    public override IEnumerator OnUnload()
    {
        AsyncOperation oper = SceneManager.UnloadSceneAsync("main");
        while (oper != null && !oper.isDone)
        {
            yield return null;
        }

        UIEngine.Instance.HidePanel<MainPanel>();
    }

    public override void OnLoop()
    {
    }
}
