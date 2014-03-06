using System;
using System.Collections.Generic;

namespace SandraFx
{
    public abstract class NancyModule
    {
        public IDictionary<string, Func<object, string>> Get
        {
            get { return _get; }
        }

        readonly IDictionary<string, Func<object, string>> _get = new Dictionary<string, Func<object, string>>();
    }
}