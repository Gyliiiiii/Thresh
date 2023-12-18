using System;
using System.Collections.Generic;
using System.Threading;
using Thresh.Core.Data;

namespace Thresh.Core.Variant
{
    public class VariantPool
    {
        private static Queue<VariantList> _VariantLists = new Queue<VariantList>();
        
        private static Dictionary<VariantType,Queue<IVariant>> _VariantElements = new Dictionary<VariantType, Queue<IVariant>>(new VariantTypeCompare());
        
        internal static VariantList NewList()
        {
            if (_VariantLists.Count == 0)
            {
                return new VariantList();
            }

            return _VariantLists.Dequeue();
        }
        
        internal static VARIANT NewVariant<VARIANT>(VariantType variant_type) where VARIANT : IVariant
        {
            Queue<IVariant> found = null;
            if (_VariantElements.TryGetValue(variant_type, out found) && found.Count > 0)
            {
                VARIANT ret = (VARIANT)found.Dequeue();

                return ret;
            }

            IVariant variant = Activator.CreateInstance<VARIANT>();
            return (VARIANT)variant;
        }

        private static Thread tmp11;
        private static string tmp22;
        public static void Recycle<VARIANT>(VARIANT variant) where VARIANT : IVariant
        {
            Queue<IVariant> found = null;
            if (!_VariantElements.TryGetValue(variant.Type, out found))
            {
                found = new Queue<IVariant>();

                _VariantElements.Add(variant.Type, found);
            }
            variant.Clear();

            found.Enqueue(variant);
        }
        
        internal static void Recycle(VariantList variant_list)
        {
            for (int i = 0; i < variant_list.Count; i++)
            {
                IVariant variant = variant_list[i];
                Recycle(variant);
            }
            
            variant_list.Clear();
            
            _VariantLists.Enqueue(variant_list);
        }

    }
}