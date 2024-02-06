

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

        public static (int, int) ReadMoveInput() {
            ConsoleKey key = Console.ReadKey().Key;
            (int, int) direction = InputToDirection(key);
            return direction;
        }

        private static (int, int) InputToDirection(ConsoleKey key)
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

        public static ConsoleKey DirectionToInput((int, int) direction)
        {
            return direction switch
            {
                (-1,0) => MoveUpKey,
                (0,-1) => MoveLeftKey,
                (1, 0) => MoveDownKey,
                (0, 1) => MoveRightKey,
                _ => BreakKey
            };
        }

        public static bool AcceptableInput((int, int) direction)
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