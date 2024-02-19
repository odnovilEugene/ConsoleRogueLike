using RogueLike.Settings;

namespace RogueLike.Components.Core
{

    // Сделать метод InBounds
    // Писать операторы через =>
    public struct Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public readonly bool InBound => X > 0 && X < Map.Width - 1 && Y > 0 && Y < Map.Height - 1;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector2 pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
        
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }

        public static bool operator ==(Vector2 a, Vector2 b) => (a.Y == b.Y) && (a.X == b.X);

        public static bool operator !=(Vector2 a, Vector2 b) => (a.Y != b.Y) || (a.X != b.X);

        public static bool operator ==(Vector2 a, (int, int) coords) => (coords.Item1 == a.X) && (coords.Item2 == a.Y);

        public static bool operator !=(Vector2 a, (int, int) coords) => (coords.Item1 != a.X) || (coords.Item2 != a.Y);

        public static implicit operator Vector2((int, int) v)
        {
            return new Vector2(v.Item1, v.Item2);
        }

        public override readonly bool Equals(object obj)
        {
            return obj is Vector2 pos &&
                   X == pos.X &&
                   Y == pos.Y;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Vector2 GetRandom(Range x, Range y)
        {
            int randX = Random.Shared.Next(x.Start.Value, x.End.Value);
            int randY = Random.Shared.Next(y.Start.Value, y.End.Value);
            return new Vector2(randX, randY);
        }

        public override readonly string ToString() => $"({X}, {Y})";

        internal readonly void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}