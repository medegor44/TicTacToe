namespace TicTacToe
{
    public class GameEngine
    {
        private int[,] _map;
        public int CurrentTurn { get; set; }

        public GameEngine()
        {
            _map = new int[3, 3];
            for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
                _map[i, j] = None;
        }

        public GameEngine(int[,] map, int currentTurn)
        {
            _map = map;
            CurrentTurn = currentTurn;
        }

        public void MakeTurn(int i, int j)
        {
            if (CurrentTurn == Xs)
                _map[i, j] = 1;
            else
                _map[i, j] = 0;

            CurrentTurn = 1 - CurrentTurn;
        }

        public int GameOver()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                var vWinner = VerticalGameOver(i);
                var hWinner = HorizontalGameOver(i);

                if (vWinner != None)
                    return vWinner;
                if (hWinner != None)
                    return hWinner;
            }

            var dWinner = DiagonalGameOver();

            if (dWinner != None)
                return dWinner;
            return None;
        }

        public int VerticalGameOver(int column)
        {
            int[] count = new int[2];
            for (int i = 0; i < _map.GetLength(0); i++)
                if (_map[i, column] != -1)
                    count[_map[i, column]]++;

            if (count[0] == 3)
                return Os;
            if (count[1] == 3)
                return Xs;
            return None;
        }

        public int HorizontalGameOver(int row)
        {
            int[] count = new int[2];

            for (int j = 0; j < _map.GetLength(1); j++)
                if (_map[row, j] != -1)
                    count[_map[row, j]]++;

            if (count[0] == 3)
                return Os;
            if (count[1] == 3)
                return Xs;
            return None;
        }

        public int DiagonalGameOver()
        {
            int[] countMainDiagonal = new int[2];
            int n = _map.GetLength(0);

            for (int i = 0; i < n; i++)
                if (_map[i, i] != -1)
                    countMainDiagonal[_map[i, i]]++;

            int[] countAuxDiagonal = new int[2];
            for (int i = 0; i < n; i++)
                if (_map[i, n - i - 1] != -1)
                    countAuxDiagonal[_map[i, n - i - 1]]++;

            if (countMainDiagonal[Os] == 3 || countAuxDiagonal[Os] == 3)
                return Os;
            if (countMainDiagonal[Xs] == 3 || countAuxDiagonal[Xs] == 3)
                return Xs;
            return None;
        }

        public const int Os = 0;
        public const int Xs = 1;
        public const int None = -1;
        public int[,] Map => _map;
    }
}

