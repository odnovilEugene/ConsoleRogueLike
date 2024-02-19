
namespace RogueLike.Components.ObjectProps
{
    public struct Health
    {
        public int Hp { get; set; }
        public int MaxHp { get; set; }

        public Health(int hp, int maxhp)
        {
            Hp = hp;
            MaxHp = maxhp;
        }

        public static Health operator -(Health a, int b)
        {
            a.Hp -= b;
            return a;
        }

        public static Health operator +(Health a, int b)
        {
            int newHp = a.Hp += b;
            a.Hp = a.MaxHp <= newHp ? a.MaxHp : newHp;
            return a;
        }

        public override readonly string ToString()
        {
            return $"{Hp} / {MaxHp}";
        }
    }
}