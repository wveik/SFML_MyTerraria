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
        GROUND ,//Почва
        GRASS // трава
    }

    class Tile : Transformable, Drawable {

        //Размер тайла по ширине и высоте
        public const int TILE_SIZE = 16;

        TileType type = TileType.GROUND;
        RectangleShape reactShape;

        public Tile(TileType _type) {
            this.type = _type;

            reactShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            switch (type) {
                case TileType.NONE:
                    /* NOP */
                    break;
                case TileType.GROUND:
                    reactShape.Texture = Content.textTile0; // ПОЧВА
                    break;
                case TileType.GRASS:
                    reactShape.Texture = Content.textTile1; // ЗЕМЕЛЬНЫЙ БЛОК С ТРАВОЙ 
                    break;
            }
            reactShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;

            target.Draw(reactShape, states);
        }
    }
}
