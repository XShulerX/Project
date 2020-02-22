using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        public static int Width { get; set; }

        public static int Height { get; set; }

        //static Game()
        //{

        //}

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = 17 };
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static List<VisualObject> __GameObjects;

        public static void Load()
        {
            __GameObjects = new List<VisualObject>();
            for (var i = 0; i < 15; i++)
            {
                var visObj = new VisualObject(
                                    new Point(600, i * 20),
                                    new Point(15 - i, 20 - i),
                                    new Size(20, 20));
                __GameObjects.Add(visObj);
            }


            for (var i = 15; i < 30; i++)
            {
                var star = new Star(
                   new Point(600, i * 20),
                    new Point(-i, 0),
                    20);
                __GameObjects.Add(star);
            }

            var r = new Random();
            for (var i = 1; i <= 5; i++)
            {
                var sputnik = new Sputnik(
                    new Point(r.Next(Width-100), 20*i),
                    new Point(r.Next(5),1),
                    20, r.Next(30));
                __GameObjects.Add(sputnik);
            }

        }

        public static void Draw()
        {
            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            //g.DrawRectangle(Pens.White, new Rectangle(50, 50, 200, 200));
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 50, 70, 120));

            foreach (var visual_object in __GameObjects)
                visual_object.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object.Update();
        }
    }
}
