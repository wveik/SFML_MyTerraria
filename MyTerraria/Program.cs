using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria {
    class Program {
        private static RenderWindow win;

        public static RenderWindow Window { get { return win; } }
        public static Game Game { private set; get; }

        static void Main(string[] args) {
            win = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Моя Terraria!");
            win.SetVerticalSyncEnabled(true);

            win.Closed += Win_Closed;

            //Загрузка контента
            Content.Load();

            Game = new Game();

            while (win.IsOpen) {
                win.DispatchEvents();

                Game.Update();

                win.Clear(Color.Black);

                Game.Draw();

                win.Display();
            }
        }

        private static void Win_Closed(object sender, EventArgs e) {
            if (win != null)
                win.Close();
        }
    }
}
