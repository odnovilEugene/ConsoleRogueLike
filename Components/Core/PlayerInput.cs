

namespace RogueLike.Components.Core
{
    public class PlayerInput
    {
        public const ConsoleKey MoveUpKey = ConsoleKey.W;
        public const ConsoleKey MoveDownKey = ConsoleKey.S;
        public const ConsoleKey MoveLeftKey = ConsoleKey.A;
        public const ConsoleKey MoveRightKey = ConsoleKey.D;
        public const ConsoleKey BreakKey = ConsoleKey.Enter;
        public const ConsoleKey AcceptKey = ConsoleKey.Y;
        public const ConsoleKey RejectKey = ConsoleKey.N;

        public static ConsoleKey ReadInput()
        {
            ConsoleKey key = Console.ReadKey().Key;
            return key;
        }

        public static Vector2 ReadMoveInput() {
            ConsoleKey key = Console.ReadKey().Key;
            Vector2 direction = InputToDirection(key);
            return direction;
        }

        private static Vector2 InputToDirection(ConsoleKey key)
        {
            return key switch
            {
                MoveLeftKey => (-1,0),
                MoveUpKey => (0,-1),
                MoveRightKey => (1, 0),
                MoveDownKey => (0, 1),
                _ => (0,0)
            };
        }

        public static ConsoleKey DirectionToInput(Vector2 direction)
        {
            return direction switch
            {
                (0,-1) => MoveUpKey,
                (-1,0) => MoveLeftKey,
                (0, 1) => MoveDownKey,
                (1, 0) => MoveRightKey,
                _ => BreakKey
            };
        }

        public static bool AcceptableInput(Vector2 direction)
        {
            return direction switch
            {
                (-1,0) => true,
                (0,-1) => true,
                (1, 0) => true,
                (0, 1) => true,
                _ => false
            };
        }
    }
}