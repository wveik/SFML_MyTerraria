using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class Chunk : Transformable, Drawable {
        //Кол-во тайлов в одном чанке по ширине и высоте
        public const int CHUNK_SIZE = 25;
        private Tile[][] tiles; // массив плиток
        private Vector2i chunkPos; // позиция чанка в массиве мира

        public Chunk(Vector2i _chunkPos) {
            //выставляем позицию чанка
            this.chunkPos = _chunkPos;
            Position = new Vector2f(chunkPos.X * CHUNK_SIZE * Tile.TILE_SIZE, chunkPos.Y * CHUNK_SIZE * Tile.TILE_SIZE);

            //создаём двумерный массив тайлов
            tiles = new Tile[CHUNK_SIZE][];

            for (int i = 0; i < CHUNK_SIZE; i++) {
                tiles[i] = new Tile[CHUNK_SIZE];
            }
        }

        // установить плитку в чанке
        public void SetTile(TileType type, int x, int y, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile) {
            tiles[x][y] = new Tile(type, upTile, downTile, leftTile, rightTile);
            tiles[x][y].Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;
        }

        //Получить плитку из чанка
        public Tile GetTile(int x, int y) {
            //Если позиция плитки выходит за границу чанка
            if (x < 0 || y < 0 || x >= CHUNK_SIZE || y >= CHUNK_SIZE) {
                return null;
            }

            return tiles[x][y];
        }

        // рисуем чанк и его содержимое
        public void Draw(RenderTarget target, RenderStates states) {
            
            //Рисуем тайлы
            for (int x = 0; x < CHUNK_SIZE; x++) {
                for (int y = 0; y < CHUNK_SIZE; y++) {
                    if (tiles[x][y] == null) continue;

                    target.Draw(tiles[x][y]);
                }
            }
        }
    }
}
