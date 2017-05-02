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

        //Соседи
        Tile upTile = null; //верхний сосед
        Tile downTile = null;//нижний сосед
        Tile leftTile = null;// левый сосед 
        Tile rightTile = null;//правый сосед

        public Tile(TileType _type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile) {
            this.type = _type;

            if (upTile != null) {
                this.upTile = upTile;
                this.upTile.downTile = this; //для верхнего соседа плитка будет нижним соседом
            }
            if (downTile != null) {
                this.downTile = downTile;
                this.downTile.upTile = this; //для нижнего соседа плитка будет верхним соседом
            }
            if (leftTile != null) {
                this.leftTile = leftTile;
                this.leftTile.rightTile = this; //для левого соседа плитка будет правым соседом
            }
            if (rightTile != null) {
                this.rightTile = rightTile;
                this.rightTile.leftTile = this; //для правого соседа плитка будет левым соседом
            }

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
            reactShape.TextureRect = GetTextureRect(1, 1);

            UpdateView();

        }

        // Обновляем внешний вид плитки в зависимости от соседей
        public void UpdateView() {
            
        }

        public IntRect GetTextureRect(int i, int j) {
            int x = i * TILE_SIZE + i * 2;
            int y = j * TILE_SIZE + j * 2;
            return new IntRect(x, y, TILE_SIZE, TILE_SIZE);
        }

        //рисуем плитку
        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;

            target.Draw(reactShape, states);
        }
    }
}
