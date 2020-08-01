using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class TicTacToeDb : DbContext
    {
        public TicTacToeDb(DbContextOptions<TicTacToeDb> opt) : base(opt)
        {

        }

        public DbSet<Game> Games { get; set; }
    }
}