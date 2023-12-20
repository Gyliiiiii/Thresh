using System.Collections.Generic;
using UnityEngine;

namespace Thresh.Unity.Utility
{
    public class MathUtil
    {
        public static float Distance(Vector3 l, Vector3 r)
        {
            return (l - r).magnitude;
        }
        
        public static void ParabolaToSegment(Vector3 s, Vector3 e, float angle, int segCount,
            ref List<Vector3> segments)
        {
            float dist = MathUtil.Distance(e, s);
            float disttan = dist * Mathf.Tan(Mathf.Deg2Rad * angle);

            float t = Mathf.Sqrt(2 * disttan / -Physics.gravity.y);
            float v0 = disttan / t;

            float step = t / segCount;
            
            segments.Clear();
            segments.Add(s);

            for (int i = 0; i < segCount; i++)
            {
                float tt = step * (i + 1);
                Vector3 vv = Vector3.Lerp(s, e, (float)(i + 1) / (float)segCount);
                float h = v0 * tt + 0.5f * Physics.gravity.y * tt * tt;
                vv.y += h;
                segments.Add(vv);
            }
        }
    }
}