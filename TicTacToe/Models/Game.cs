namespace TicTacToe.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Map { get; set; }
        public int Turn { get; set; }
        public int GameOver { get; set; }
    }
}