using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.navidad
{
    [Serializable]
    public struct JsonDateTime
    {
        private DateTime? _date;

        public long ticks;

        private DateTime Date => _date ?? ((_date = new DateTime(ticks, DateTimeKind.Utc))).Value;

        public static implicit operator DateTime(JsonDateTime jdt) => jdt.Date.ToLocalTime();
        public static implicit operator JsonDateTime(DateTime dt)
        {
            var date = dt.ToUniversalTime();

            return new JsonDateTime { ticks = date.Ticks, _date = date };
        }

        public override string ToString() => _date.ToString();
        public string ToString(string format, IFormatProvider formatProvider = null) => Date.ToString(format, formatProvider);
    }
}
