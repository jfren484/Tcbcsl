using System;
using System.Collections.Generic;
using System.Linq;

namespace Tcbcsl.Presentation.Helpers
{
    public static class ChangeTracker
    {
        public static ChangeSets<TLeft, TRight> GetChangeSets<TLeft, TRight, TKey>(ICollection<TLeft> leftItems, ICollection<TRight> rightItems,
                                                                                   Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey)
        {
            var commonItems = (from a1 in leftItems
                               join b1 in rightItems
                               on leftKey(a1) equals rightKey(b1)
                               select new CommonPair<TLeft, TRight>(a1, b1))
                              .ToList();

            var onlyInA = leftItems.Except(commonItems.Select(t => t.LeftItem)).ToList();
            var onlyInB = rightItems.Except(commonItems.Select(t => t.RightItem)).ToList();

            return new ChangeSets<TLeft, TRight>
            {
                CommonItems = commonItems,
                LeftOnly = onlyInA,
                RightOnly = onlyInB
            };
        }

        public class ChangeSets<TLeft, TRight>
        {
            public List<TLeft> LeftOnly { get; set; }
            public List<TRight> RightOnly { get; set; }
            public List<CommonPair<TLeft, TRight>> CommonItems { get; set; }
        }

        public class CommonPair<TLeft, TRight>
        {
            public TLeft LeftItem { get; }
            public TRight RightItem { get; }

            public CommonPair(TLeft leftItem, TRight rightItem)
            {
                LeftItem = leftItem;
                RightItem = rightItem;
            }
        }
    }
}
