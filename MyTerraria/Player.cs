using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyTerraria {
    class Player : Transformable, Drawable {
        public const float PLAYER_MOVE_SPEED = 4F;
        public const float PLAYER_MOVE_ACCELERATION = 0.2F;

        public Vector2f StartPosition;

        private RectangleShape rect;
        private RectangleShape rectDirection;
        private Vector2f velocity; // Ускорение игрока
        private Vector2f movement; // вектор перемещения игрока
        private World world;

        public int Direction
        {

            set
            {
                int dir = value >= 0 ? 1 : -1;
                Scale = new Vector2f(dir, 1);
            }
            get
            {
                int dir = Scale.X >= 0 ? 1 : -1;
                return dir;
            }
        }

        public Player(World _world) {
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);

            rectDirection = new RectangleShape(new Vector2f(50, 3));
            rectDirection.FillColor = Color.Red;
            rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);

            this.world = _world;
        }

        public void Update() {
            updatePhysics(); // обновление физики
            updateMovement(); // обновление перемещения

            Position += velocity;
        }

        private void updatePhysics() {
            bool isFall = true;

            velocity += new Vector2f(0, 0.15f);

            int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
            int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);
            Tile tile = world.GetTile(pX, pY);

            if(tile != null) {
                Vector2f nextPos = Position + velocity - rect.Origin;
                FloatRect playerRect = new FloatRect(nextPos, rect.Size);
                FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                isFall = !playerRect.Intersects(tileRect);
            }

            if (!isFall) {
                velocity.Y = 0;
            }
        }

        private void updateMovement() {
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            if(!isMoveLeft)
                isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            if(!isMoveRight)
                isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            bool isMove = isMoveLeft || isMoveRight;

            if (isMove) {
                if (isMoveLeft) {
                    movement.X -= PLAYER_MOVE_ACCELERATION;
                    this.Direction = -1;
                } else if (isMoveRight) {
                    movement.X += PLAYER_MOVE_ACCELERATION;
                    Direction = 1;
                }
            }
        }

        public void Spawn() {
            Position = this.StartPosition;
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;
            target.Draw(rect, states);
            target.Draw(this.rectDirection, states);
        }
    }
}
