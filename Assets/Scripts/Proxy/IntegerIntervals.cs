using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.proxy
{
    [Serializable]
    public class IntegerInterval
    {
        public int start;
        public int end;
    }

    [Serializable]
    public class IntegerIntervals
    {
        public IntegerInterval[] intervals;
    }
}
