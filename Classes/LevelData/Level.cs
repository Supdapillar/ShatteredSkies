using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ShatteredSkies.Classes
{
    public class Level
    {
        public Layer[] Layers { get; set; }
        public TileData[] TileDatas { get; set; }
        public Level(dynamic json)
        {
            Layers = json.SelectToken("layers").ToObject<Layer[]>();
            TileDatas = json.SelectToken("tilesets").ToObject<TileData[]>();
        }
    }
}
