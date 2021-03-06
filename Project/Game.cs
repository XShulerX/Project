﻿using Project.VisualObjects;
using Project.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    static class Game
    {
        /// <summary>Таймаут отрисовки одной сцены</summary>
        private const int __FrameTimeout = 10;

        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        private static Timer __Timer;

        public static event EventHandler AllAsteroidsDestroyed;

        public static event Action<string> Log;

        public static int Width { get; set; }

        public static int Height { get; set; }

        //static Game()
        //{

        //}

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;
            if (Width > 1000 || Width < 0 || Height < 0 || Height > 1000)
                throw new ArgumentOutOfRangeException();
            __Context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = __FrameTimeout };
            timer.Tick += OnTimerTick;
            timer.Start();
            __Timer = timer;
            _Score = 0;

            form.KeyDown += OnFormKeyDown;

            Log?.Invoke("Выполнена инициализация");
        }

        private static void OnFormKeyDown(object Sender, KeyEventArgs E)
        {
            switch (E.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullets.Add(new Bullet(__Ship.Position.Y));
                    break;

                case Keys.Up:
                    __Ship.MoveUp();
                    break;

                case Keys.Down:
                    __Ship.MoveDown();
                    break;
            }
            Log?.Invoke($"Нажата кнопка {E.KeyCode}");
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static SpaceShip __Ship;
        private static HealthPack __HealthPack;
        private static VisualObject[] __GameObjects;
        private static List<Bullet> __Bullets = new List<Bullet>();
        private static List<Asteroid> __Asteroids = new List<Asteroid>();
        private static int countAsteroids = 1;
        private static int _Score;

        public static void Load()
        {
            Log?.Invoke("Загрузка данных сцены...");

            var game_objects = new List<VisualObject>();
            var rnd = new Random();
            GenerateAsteroids(ref game_objects, ref countAsteroids);

            const int stars_count = 150;
            const int star_size = 5;
            const int star_max_speed = 20;
            for (var i = 0; i < stars_count; i++)
                game_objects.Add(new Star(
                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, star_max_speed), 0),
                    star_size));
            Log?.Invoke($"Создано звёзд {stars_count}");

            const int sputniks_count = 3;
            for (var i = 0; i < sputniks_count; i++)
                game_objects.Add(
                    new Sputnik(new Point(rnd.Next(Width - 100), 20 * i),
                    new Point(rnd.Next(5), 1),
                    20, rnd.Next(30)));

            const int healthpack_size = 20;
            game_objects.Add(new HealthPack(
                    new Point(Width, rnd.Next(0, Height)),
                    new Point(-5),
                    new Size(healthpack_size, healthpack_size)));
            __GameObjects = game_objects.ToArray();

            __Ship = new SpaceShip(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            __Ship.ShipDestroyed += OnShipDestroyed;
            AllAsteroidsDestroyed += OnAllAsteroidsDestroyed;
            Log?.Invoke("Загрузка данных сцены выполнена успешно");
        }

        /// <summary>
        /// Генерация астероидов
        /// </summary>
        /// <param name="game_objects">Лист игровых объектов</param>
        /// <param name="count">Колличество астероидов</param>
        private static void GenerateAsteroids(ref List<VisualObject> game_objects, ref int count)
        {
            Random rnd = new Random();
            int asteroids_count = count++;
            const int asteroid_size = 25;
            const int asteroid_max_speed = 20;
            for (var i = 0; i < asteroids_count; i++)
            {
                var asteroid = new Asteroid(
                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, asteroid_max_speed), 0),
                    asteroid_size);
                game_objects.Add(asteroid);
                __Asteroids.Add(asteroid);
            }
            Log?.Invoke($"Астероидов создано {asteroids_count}");
        }

        /// <summary>
        /// Обработчик события при уничтожении всех астероидов
        /// </summary>
        private static void OnAllAsteroidsDestroyed(object Sender, EventArgs E)
        {
            List<VisualObject> game_objects = new List<VisualObject>(__GameObjects);
            GenerateAsteroids(ref game_objects, ref countAsteroids);
            __GameObjects = game_objects.ToArray();
        }

        private static void OnShipDestroyed(object Sender, EventArgs E)
        {
            __Timer.Stop();
            __Buffer.Graphics.Clear(Color.DarkBlue);
            __Buffer.Graphics.DrawString("Game over!!!", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.Red, 200, 100);
            __Buffer.Render();
        }

        /// <summary>Метод визуализации сцены</summary>
        public static void Draw()
        {
            if (__Ship.Energy <= 0) return;
            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var visual_object in __GameObjects)
                visual_object?.Draw(g);

            foreach (var bullet in __Bullets) bullet.Draw(g);

            __Ship.Draw(g);
            g.DrawString($"Energy: {__Ship.Energy}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.White, 10, 10);
            g.DrawString($"Score: {_Score}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.White, 10, 30);
            __Buffer.Render();
        }

        /// <summary>Обновление состояния объектов сцены</summary>
        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object?.Update();

            var bullets_to_remove = new List<Bullet>();
            foreach (var bullet in __Bullets)
            {
                bullet.Update();
                if (bullet.Position.X > Width)
                    bullets_to_remove.Add(bullet);
            }
            for (var i = 0; i < __GameObjects.Length; i++)
            {
                var obj = __GameObjects[i];
                if (obj is ICollision collision_object) // Применить "сопоставление с образцом"!
                {
                    collision_object = (ICollision)obj;
                    __Ship.CheckCollision(collision_object);
                    if (__Ship.CheckCollision(collision_object) && collision_object is HealthPack healthPack)
                    {
                        __GameObjects[i] = new HealthPack(
                            new Point(Width, new Random().Next(0, Height)),
                            new Point(-5),
                            new Size(20, 20));
                    }
                    foreach (var bullet in __Bullets.ToArray())
                        if (bullet.CheckCollision(collision_object))
                        {
                            bullets_to_remove.Add(bullet);
                            for (var j = 0; j < __Asteroids.Count; j++)
                            {
                                if (__Asteroids[j].Equals(__GameObjects[i]))
                                {
                                    __GameObjects[i] = null;
                                    __Asteroids.RemoveAt(j);
                                    break;
                                }
                            }

                            _Score += 10;
                        }
                }
            }
            foreach (var bullet in bullets_to_remove)
                __Bullets.Remove(bullet);
            if (__Asteroids.Count == 0)
                AllAsteroidsDestroyed?.Invoke(__Ship, EventArgs.Empty);
        }


    }

}
