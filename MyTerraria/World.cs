using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class World : Transformable, Drawable {
        //Кол-во чанков по ширине и высоте
        public const int WORLD_SIZE = 5;

        //Чанки
        private Chunk[][] chunks;

        public World() {
            chunks = new Chunk[WORLD_SIZE][];

            for (int i = 0; i < WORLD_SIZE; i++) {
                chunks[i] = new Chunk[WORLD_SIZE];
            }
        }

        //Генерируем новый мир
        public void GenerateWorld() {
            for (int x = 0; x < 50; x++) {
                for (int y = 17; y <= 17; y++) {
                    SetTile(TileType.GRASS, x, y);
                }
            }

            for (int x = 0; x < 50; x++) {
                for (int y = 18; y <= 32; y++) {
                    SetTile(TileType.GROUND, x, y);
                }
            }
        }

        //Установить плитку
        public void SetTile(TileType type, int x, int y) {
            var chunk = GetChunk(x, y);
            var tilePos = GetTilePosFromChunk(x, y);

            //Находим соседей
            Tile upTile = GetTile(x , y - 1); //верхний сосед
            Tile downTile = GetTile(x, y + 1);//нижний сосед
            Tile leftTile = GetTile(x - 1, y);// левый сосед 
            Tile rightTile = GetTile(x + 1, y);//правый сосед

            chunk.SetTile(type, tilePos.X, tilePos.Y, upTile, downTile, leftTile, rightTile);
        }

        //Установить плитку
        public Tile GetTile(int x, int y) {
            var chunk = GetChunk(x, y);
            if(chunk == null) {
                return null;
            }
            var tilePos = GetTilePosFromChunk(x, y);

            return chunk.GetTile(tilePos.X, tilePos.Y);
        }

        // Получить чанк
        public Chunk GetChunk(int x, int y) {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;

            if (chunks[X][Y] == null) {
                chunks[X][Y] = new Chunk(new Vector2i(X, Y));
            }

            return chunks[X][Y];
        }

        //Получить позицию плитку внутри чанка
        public Vector2i GetTilePosFromChunk(int x, int y) {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;

            return new Vector2i(x - X * Chunk.CHUNK_SIZE, y - Y * Chunk.CHUNK_SIZE);
        }

        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states) {
            for (int x = 0; x < WORLD_SIZE; x++) {
                for (int y = 0; y < WORLD_SIZE; y++) {
                    if (chunks[x][y] == null) continue;

                    target.Draw(chunks[x][y]);
                }
            }
        }
    }
}
