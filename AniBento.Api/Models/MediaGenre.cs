namespace AniBento.Api.Models
{
    public class MediaGenre
    {
        public int MediaId { get; set; }
        public Media Media { get; set; } = default!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
