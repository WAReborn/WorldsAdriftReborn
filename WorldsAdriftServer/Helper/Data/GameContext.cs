using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Helper.Data
{
    public class GameContext : DbContext
    {
        public DbSet<CharacterDataDTO> Characters { get; set; }

        public string DbPath { get; }

        public GameContext()
        {
            DbPath = Path.Join(Directory.GetCurrentDirectory(), "WAR.db");
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) 
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

    }
}
