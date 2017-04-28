using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class Content {
        public const string CONTENT_DIR = "..\\Content\\Textures\\";

        public static Texture textTile0;

        public static void Load() {
            textTile0 = new Texture(CONTENT_DIR + "Tiles_0.png");

        }
    }
}
