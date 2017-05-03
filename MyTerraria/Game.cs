using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class Game {
        private World world;
        private Player player;

        public Game() {
            //Создаем мир
            world = new World();
            world.GenerateWorld();

            //создаем игрока
            player = new Player(world);
            player.StartPosition = new Vector2f(300 , 150);
            player.Spawn();

            DebugRender.Enabled = true;
        }

        // Обновление логики игры
        public void Update() {
            player.Update();
        }

        // Прорисовка игры
        public void Draw() {
            Program.Window.Draw(world); // рисуем мир
            Program.Window.Draw(player); // рисуем игрока

            DebugRender.Draw(Program.Window); // рисуем объекты для визуальной отладки
        }
    }
}
