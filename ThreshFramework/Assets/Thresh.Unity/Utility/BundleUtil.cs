namespace Thresh.Unity.Utility
{
    public class BundleUtil
    {
        public static string GetAssetName(string asset_path)
        {
            int index = asset_path.LastIndexOf('/');
            if (index < 0)
            {
                return asset_path;
            }

            return asset_path.Remove(0, index+1);
        }
    }
}