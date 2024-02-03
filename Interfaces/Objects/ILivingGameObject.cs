namespace RogueLike.Interfaces.Objects
{
    public interface ILivingGameObject
    {
       public int MaxHp { get; }
       public int Hp { get; }
       public int Attack { get; }
       public bool IsDead { get; }
       void TakeDamage(int amount);
       string GetInfo();
    }
}