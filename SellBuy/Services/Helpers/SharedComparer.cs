using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;

namespace SellBuy.Services.Helpers
{
    public static class SharedComparer
    {
        public static ComparisonResult DiffObjects<A, B>(A first, B second, IEnumerable<string> ignore)
        {
            var compareLogic = new CompareLogic();
            compareLogic.Config.MaxDifferences = 99999;
            compareLogic.Config.IgnoreObjectTypes = true;

            if (ignore != null)
            {
                foreach (var item in ignore)
                {
                    compareLogic.Config.MembersToIgnore.Add(item);
                }
            }

            return compareLogic.Compare(first, second);
        }
    }
}
