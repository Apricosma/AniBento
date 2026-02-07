namespace AniBento.Api.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string NameNormalized { get; set; }

        public ICollection<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
    }
}
