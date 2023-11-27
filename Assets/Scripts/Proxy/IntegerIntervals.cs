using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.proxy
{
    public class IntegerInterval
    {
        public int? Start { get; set; }
        public int? End { get; set; }

        public int CompareInInterval(int? num)
        {
            if (!Start.HasValue)
                return 0;

            if (!num.HasValue)
                return -1;

            if (Start.Value < num.Value)
                return -1;

            if (!End.HasValue || !num.HasValue)
                return 0;

            if (End.Value > num.Value)
                return 1;

            return 0;
        }
    }

    public class IntegerIntervals
    {
        public List<IntegerInterval> Intervals { get; } = new List<IntegerInterval>();

        public void Add(int num)
        {
            int idx = FindIn(num);

            if (idx == 0)
                return;

            idx = -idx;
            if (idx >= Intervals.Count)
                Intervals.Insert(idx, new IntegerInterval { Start = num, End = num });
            else
            {
                var interval = Intervals[idx];

                if (interval.Start.GetValueOrDefault() - 1 == num)
                    interval.Start = num;
                else if (interval.End.GetValueOrDefault() + 1 == num)
                    interval.End = num;
                else
                    Intervals.Insert(idx, new IntegerInterval { Start = num, End = num });
            }
        }

        public int FindIn(int num)
        {
            int l = 0;
            int r = Intervals.Count - 1;
            int m = 0, cmp = -1;

            while (l <= r)
            {
                m = (l + r) / 2;
                cmp = Intervals[m].CompareInInterval(num);

                if (cmp == 0)
                    return m;
                if (cmp < 0)
                    l = m + 1;
                else
                    r = m - 1;
            }

            return -(cmp <= 0 ? m : m - 1);
        }
    }
}
