using microbytkonamic.navidad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.proxy
{
    [Serializable]
    public class FelicitacionDto
    {
        public string nick;
        public JsonDateTime fecha;

        public string texto;
    }
}
