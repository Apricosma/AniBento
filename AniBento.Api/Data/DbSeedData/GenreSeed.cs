using AniBento.Api.Models;

namespace AniBento.Api.Data.DbSeedData
{
    public static class GenreSeed
    {
        private static Genre G(int id, string name) =>
            new Genre()
            {
                Id = id,
                Name = name,
                NameNormalized = name.ToUpperInvariant(),
            };

        public static readonly Genre[] CanonicalGenres =
        [
            G(1, "Action"),
            G(2, "Adventure"),
            G(3, "Comedy"),
            G(4, "Drama"),
            G(5, "Fantasy"),
            G(6, "Horror"),
            G(7, "Mystery"),
            G(8, "Romance"),
            G(9, "Sci-Fi"),
            G(10, "Slice of Life"),
            G(11, "Sports"),
            G(12, "Supernatural"),
            G(13, "Thriller"),
            G(14, "Mecha"),
            G(15, "Music"),
            G(16, "Psychological"),
            G(17, "Historical"),
            G(18, "Military"),
            G(19, "Parody"),
            G(20, "School"),
            G(21, "Shounen"),
            G(22, "Shoujo"),
            G(23, "Seinen"),
            G(24, "Josei"),
            G(25, "Isekai"),
            G(26, "Ecchi"),
            G(27, "Harem"),
            G(28, "Reverse Harem"),
            G(29, "Superhero"),
            G(30, "Cyberpunk"),
            G(31, "Post-Apocalyptic"),
            G(32, "Steampunk"),
            G(33, "Crime"),
            G(34, "Detective"),
            G(35, "Martial Arts"),
            G(36, "Vampire"),
            G(37, "Magic"),
            G(38, "Game"),
            G(39, "Demons"),
            G(40, "Space"),
        ];
    }
}
