using System.Collections;

namespace Thresh.Unity.Stage
{
    public abstract class StageBase
    {
        internal string Name;

        public abstract IEnumerator OnLoad();

        public abstract IEnumerator OnUnload();
        public abstract void OnLoop();
    }
}