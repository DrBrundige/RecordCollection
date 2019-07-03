using Microsoft.EntityFrameworkCore;

namespace IntroToEntity.Models
{
	public class Context : DbContext
	{
		// base() calls the parent class' constructor passing the "options" parameter along
		public Context(DbContextOptions options) : base(options) { }

		public DbSet<Record> Records { get; set; }
		public DbSet<Band> Bands { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<Musician> Musicians { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Label> Labels { get; set; }
		public DbSet<Roll> Rolls { get; set; }
		public DbSet<MusicianToBand> MusicianToBand { get; set; }
		public DbSet<MusicianToRecord> MusicianToRecord { get; set; }
	}
}