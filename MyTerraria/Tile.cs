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
        GROUND,//Почва
        GRASS // трава
    }

    class Tile : Transformable, Drawable {

        //Размер тайла по ширине и высоте
        public const int TILE_SIZE = 16;

        private TileType type = TileType.GROUND;
        private RectangleShape reactShape;

        //Соседи
        private Tile upTile = null; //верхний сосед
        private Tile downTile = null;//нижний сосед
        private Tile leftTile = null;// левый сосед 
        private Tile rightTile = null;//правый сосед

        public Tile UpTile
        {
            set
            {
                upTile = value;
                UpdateView();
            }
            get
            {
                return upTile;
            }
        }

        public Tile DownTile
        {
            get
            {
                return downTile;
            }

            set
            {
                downTile = value;
                UpdateView();
            }
        }

        public Tile LeftTile
        {
            get
            {
                return leftTile;
            }

            set
            {
                leftTile = value;
                UpdateView();
            }
        }

        public Tile RightTile
        {
            get
            {
                return rightTile;
            }

            set
            {
                rightTile = value;
                UpdateView();
            }
        }

        public Tile(TileType _type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile) {
            this.type = _type;

            reactShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            if (upTile != null) {
                UpTile = upTile;
                UpTile.DownTile = this; //для верхнего соседа плитка будет нижним соседом
            }
            if (downTile != null) {
                DownTile = downTile;
                DownTile.UpTile = this; //для нижнего соседа плитка будет верхним соседом
            }
            if (leftTile != null) {
                LeftTile = leftTile;
                LeftTile.RightTile = this; //для левого соседа плитка будет правым соседом
            }
            if (rightTile != null) {
                RightTile = rightTile;
                RightTile.LeftTile = this; //для правого соседа плитка будет левым соседом
            }

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
            UpdateView();
        }

        // Обновляем внешний вид плитки в зависимости от соседей
        public void UpdateView() {
            if (UpTile != null && DownTile != null && LeftTile != null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(1 + i, 1);
            } else if (UpTile == null && DownTile == null && LeftTile == null && RightTile == null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(9 + i, 3);
            }
            //-------------

            else if (UpTile == null && DownTile != null && LeftTile != null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(1 + i, 0);
            } else if (UpTile != null && DownTile == null && LeftTile != null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(1 + i, 2);
            } else if (UpTile != null && DownTile != null && LeftTile == null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(0, i);
            } else if (UpTile != null && DownTile != null && LeftTile != null && RightTile == null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(4, i);
            }
            //--------------------
            else if (UpTile == null && DownTile != null && LeftTile == null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(i * 2, 3);
            } else if (UpTile == null && DownTile != null && LeftTile != null && RightTile == null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(1 + i * 2, 3);
            } else if (UpTile != null && DownTile == null && LeftTile == null && RightTile != null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(0 + i * 2, 4);
            } else if (UpTile != null && DownTile == null && LeftTile != null && RightTile == null) {
                int i = Program.Rand.Next(0, 3);
                reactShape.TextureRect = GetTextureRect(1 + i * 2, 4);
            }
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
