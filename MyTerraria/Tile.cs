using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    enum TileType {
        NONE, //Пусто
        GROUND //Почта
    }

    class Tile : Transformable, Drawable {

        //Размер тайла по ширине и высоте
        public const int TILE_SIZE = 16;

        TileType type = TileType.GROUND;
        RectangleShape reactShape;

        public Tile() {

            reactShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            switch (type) {
                case TileType.NONE:
                    break;
                case TileType.GROUND:
                    reactShape.Texture = Content.textTile0;
                    reactShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                default:
                    break;
            }
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;

            target.Draw(reactShape, states);
        }
    }
}
