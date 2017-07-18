using System;
using System.Collections.Generic;
using System.Linq;

namespace boss.client.win
{
    public static class RestClientExtension
    {
        public static IDictionary<string, object> Parameters(params object[] parameters)
        {
            if (parameters.Length % 2 != 0) throw new ArgumentException("The length of parameters is invalid.");
            return Enumerable.Range(0, parameters.Length / 2).ToDictionary(x => (string)parameters[x * 2], x => parameters[x * 2 + 1]);
        }
    }
}