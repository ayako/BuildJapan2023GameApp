namespace Asteroids.GameComponents
{
    // Player クラスの定義:
    // プロパティは以下を設定
    //      connectionId, name, float x, float y, float angle, float speed, float acceleration
    //      int size = 50, uint Score = 0, bool isDead = false
    // connectionId, name, x, yを引数とするコンストラクターを定義
    // Update(x, y, angle, speed, acceleration)メソッドを定義
    // Update(x, y)メソッドを定義
    // すべての関数とプロパティは Public に設定
    public class Player
    {
    }

    // Bullet クラスの定義:
    // プロパティは以下を設定
    //     connectionId, float x, float y, float angle
    //     float speed = 10, int size = 30
    // ConnectionId, x, y, angle を引数とするコンストラクターを定義
    // Update(x, y)メソッドを定義
    // すべての関数とプロパティは Public に設定
    public class Bullet
    {
        public string connectionId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float angle { get; set; }
        public float speed { get; set; } = 10.0f;
        public int size { get; set; } = 30;

        public Bullet(string ConnectionId, float x, float y, float angle)
        {
            this.connectionId = ConnectionId;
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public void Update(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }


    // Asteroid クラスの定義: 
    // プロパティは以下を設定
    //     float x, float y, float dx, float dy, int size
    //     x, y, dx, dy, sizeを引数とするコンストラクターを定義
    // Update(float x, float y)メソッドを定義
    // すべての関数とプロパティは Public に設定
    public class Asteroid
    {
        public float x { get; set; }
        public float y { get; set; }
        public float dx { get; set; }
        public float dy { get; set; }
        public int size { get; set; }

        public Asteroid(float x, float y, float dx, float dy, int size)
        {
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
            this.size = size;
        }

        public void Update(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

