using Thresh.Unity.Utility;

namespace Thresh.Unity
{
    public delegate void GameClientEvent();
    
    public class GameClient : SingletonBehaviour<GameClient>
    {
        public GameClientEvent Enter;
        public GameClientEvent Exit;
        public GameClientEvent Esc;
        public GameClientEvent Pause;
        public GameClientEvent Continue;
        public GameClientEvent OnNet;
        public GameClientEvent OffNet;
        public GameClientEvent ChangeLanguage;

        public override void Awake()
        {
            base.Awake();
            #if UNITY_EDITOR
                
#else
            #endif
        }
    }
}