using System.Collections;
using System.Collections.Generic;
using Thresh.Unity;
using Thresh.Unity.Stage;
using UnityEngine;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Start()
    {
        GameClient.Instance.Enter += OnEnter;
    }

    private void OnEnter()
    {
       DontDestroyOnLoad(GameObject.Find("UI_Root"));
       StageEngine.Instance.Open<MainStage>();
    }
}
