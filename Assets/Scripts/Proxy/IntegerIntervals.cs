using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.proxy
{
    public class IntegerInterval
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class IntegerIntervals
    {
        public IntegerInterval[] Intervals { get; set; }
    }
}
