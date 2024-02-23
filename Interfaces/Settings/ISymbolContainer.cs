
namespace RogueLike.Interfaces.Settings
{
    public interface ISymbolContainer
    {
        public char GetPlayerChar { get; }
        public char GetZombieChar { get; }
        public char GetShooterChar { get; }
        public char GetProjectileChar { get; }
        public char GetEmptyChar { get; }
        public char GetWallChar { get; }
        public char GetExitChar { get; }
        public char GetFirstAidKitChar { get; }
    }
}