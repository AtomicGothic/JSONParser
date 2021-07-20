using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JReader : Newtonsoft.Json.JsonTextReader
    {
        public JReader(TextReader r) : base(r)
        {
        }

        public override bool Read()
        {
            bool b = base.Read();
            if (base.CurrentState == State.Property && ((string)base.Value).Contains(' '))
            {
                base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace(" ", "_"));
            }
            return b;
        }
    }
}
