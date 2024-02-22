namespace RogueLike.Interfaces.Objects
{
    public interface IStaticGameObject : IGameObject
    {
        public bool IsPassable { get; }
    }
}