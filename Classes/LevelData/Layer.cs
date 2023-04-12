using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ShatteredSkies.Classes
{
    [Serializable]
    public class Layer
    {
        public int[] data { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int opacity { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public double? speed { get; set; }
    }
}
