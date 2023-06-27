using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Asteroids.GameComponents
{
    // GameState: ゲーム内のステータスを定義
    public enum GameState
    {
        Running,
        Winning,
        Lost,
        End,
        WaitingforStart
    }

    public partial class Game
    {
        public int AsteroidsCount { get; set; } = 5;

        private int _width;
        private int _height;
        private Random _random = new Random();

        public Dictionary<string, Player> Players { get; set; } = new Dictionary<string, Player>();

        public List<Asteroid> Asteroids { get; set; } = new List<Asteroid>();

        public List<Bullet> Bullets { get; set; } = new List<Bullet>();

        public List<Asteroid> Explosions { get; set; } = new List<Asteroid>();

        public GameState State { get; set; } = GameState.WaitingforStart;

        // コンストラクター定義:
        // プレイヤーを含めたゲームのステータスを初期化
        public Game(int screenWidth, int screenHeight)
        {
            _width = screenWidth;
            _height = screenHeight;
            Asteroids = GenerateAsteroids(AsteroidsCount);
        }

        // GenerateAsteroids メソッド:
        // 指定された数の小惑星(List)をランダムな位置、速度、サイズで生成
        // コンストラクター:
        //     float x, float y, float dx, float dy, int size
        public List<Asteroid> GenerateAsteroids(int count)
        {
            var asteroids = new List<Asteroid>();

            for (int i = 0; i < count; i++)
            {
                float x = _random.Next(0, _width);
                float y = _random.Next(0, _height);
                float dx = _random.Next(-5, 5);
                float dy = _random.Next(-5, 5);
                int size = _random.Next(20, 50);

                asteroids.Add(new Asteroid(x, y, dx, dy, size));
            }

            return asteroids;
        }

        // MoveAsteroids メソッド:
        // dx, dy に基づいて小惑星を移動させる
        // 移動できる範囲はスクリーンのサイズに限定
        public void MoveAsteroids()
        {
        }

        // MoveSpaceShip メソッド:
        // 各プレイヤーの 角度 (angle) と 速度 (speed) に基づいて全てのプレイヤーを移動させる
        // 移動できる範囲はスクリーンのサイズに限定
        public void MoveSpaceShip()
        {
        }

        // MoveBullets メソッド:
        // 各弾の 角度 (angle) と 速度 (speed) に基づいて弾を移動させる
        public void MoveBullets()
        {
            lock (Bullets)
            {
                if (Bullets is null)
                {
                    return;
                }
                for (int i = 0; i < Bullets.Count; i++)
                {
                    float x = Bullets[i].x + MathF.Sin(Bullets[i].angle) * Bullets[i].speed;
                    float y = Bullets[i].y - MathF.Cos(Bullets[i].angle) * Bullets[i].speed;

                    if (x > _width || x < 0 || y > _height || y < 0)
                    {
                        Bullets.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        Bullets[i].Update(x, y);
                    }
                }
            }
        }

        // BulletAsteroidsCollisionDetection メソッド:
        public void BulletAsteroidsCollisionDetection()
        {
            foreach (var bullet in Bullets)
            {
                for (int j = 0; j < Asteroids.Count; j++)
                {
                    var dx = bullet.x - Asteroids[j].x;
                    var dy = bullet.y - Asteroids[j].y;
                    var distance = MathF.Sqrt(dx * dx + dy * dy);
                    if (distance < Asteroids[j].size / 2)
                    {
                        Explosions.Add(Asteroids[j]);
                        Bullets.Remove(bullet);
                        Asteroids.RemoveAt(j);
                        foreach (var player in Players.Values)
                        {
                            if (player.connectionId == bullet.connectionId)
                            {
                                player.Score += 10;
                            }
                        }
                        break;
                    }
                }
            }
        }

        // ShipAsteroidsCollisionDetection メソッド:
        // プレイヤーと小惑星の位置とサイズから、プレイヤーが小惑星に衝突したのかを判定
        // もしプレイヤーと小惑星が衝突していたら、プレイヤーの isDead プロパティを true に設定
        public void ShipAsteroidsCollisionDetection()
        {
            foreach (var player in this.Players.Values)
            {
                for (int j = 0; j < Asteroids.Count; j++)
                {
                    var dx = player.x - Asteroids[j].x;
                    var dy = player.y - Asteroids[j].y;
                    var distance = MathF.Sqrt(dx * dx + dy * dy);
                    if (distance < Asteroids[j].size)
                    {
                        //State = GameState.Lost;
                        player.isDead = true;
                        RespawnPlayer(player);
                    }
                }
            }
        }

        public void Move()
        {
            this.Explosions = new List<Asteroid>();

            MoveSpaceShip();
            MoveAsteroids();
            MoveBullets();

            SpawnAsteroids();
        }
    }
}
