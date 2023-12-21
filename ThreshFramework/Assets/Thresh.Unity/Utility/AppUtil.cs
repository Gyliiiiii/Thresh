using Thresh.Unity.Localization;

namespace Thresh.Unity.Utility
{
    public static class AppUtil
    {
        public static string GetLocalization(string text)
        {
            return L10NEngine.Instance.GetText(text);
        }
    }
}