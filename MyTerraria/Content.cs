using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class Content {
        public const string CONTENT_DIR = "..\\Content\\";

        public static Texture textTile0; // Ground
        public static Texture textTile1; // Grass

        public static void Load() {
            textTile0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
            textTile1 = new Texture(CONTENT_DIR + "Textures\\Tiles_1.png");
        }
    }
}
