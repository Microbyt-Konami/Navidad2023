using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.proxy
{
    [Serializable]
    public class GetFelicitacionResult
    {
        [SerializeField] public FelicitacionDto felicitacionDto;
        [SerializeField] public IntegerIntervals intervals;
    }
}
