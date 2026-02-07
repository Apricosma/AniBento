using AniBento.Api.Models;
using AniBento.Api.Models.Enums;

namespace AniBento.Api.Data.DbSeedData
{
    public static class DbMediaSeed
    {
        public static List<Media> BuildMedias(IReadOnlyDictionary<string, Genre> genresByNorm)
        {
            Genre G(string name)
            {
                var key = name.Trim().ToUpperInvariant();
                if (!genresByNorm.TryGetValue(key, out var genre))
                    throw new InvalidOperationException($"Genre '{name}' not found.");
                return genre;
            }

            static string Img(string title)
            {
                var s = title.Trim().ToLowerInvariant();
                foreach (
                    var ch in new[]
                    {
                        ":",
                        ";",
                        "'",
                        "\"",
                        ".",
                        ",",
                        "!",
                        "?",
                        "—",
                        "-",
                        "(",
                        ")",
                        "[",
                        "]",
                    }
                )
                    s = s.Replace(ch, "");
                s = s.Replace(" ", "");
                return $"https://example.com/{s}.jpg";
            }

            static List<MediaGenre> Joins(Media media, IEnumerable<Genre> genres) =>
                genres.Select(g => new MediaGenre { Media = media, Genre = g }).ToList();

            Media Anime(
                string title,
                string desc,
                DateOnly release,
                string studio,
                int eps,
                params Genre[] gs
            )
            {
                var media = new Media
                {
                    MediaType = MediaType.Anime,
                    Title = title.Trim(),
                    TitleNormalized = title.Trim().ToUpperInvariant(),
                    Description = desc.Trim(),
                    DescriptionNormalized = desc.Trim().ToUpperInvariant(),
                    ReleaseDate = release,
                    MediaImageUrl = Img(title),
                    EnteredAt = DateTimeOffset.UtcNow,
                    AnimeDetails = new AnimeDetails { Studio = studio, EpisodeCount = eps },
                };

                media.MediaGenres = Joins(media, gs);
                return media;
            }

            Media Manga(
                string title,
                string desc,
                DateOnly release,
                string publisher,
                int chapters,
                int volumes,
                params Genre[] gs
            )
            {
                var media = new Media
                {
                    MediaType = MediaType.Manga,
                    Title = title.Trim(),
                    TitleNormalized = title.Trim().ToUpperInvariant(),
                    Description = desc.Trim(),
                    DescriptionNormalized = desc.Trim().ToUpperInvariant(),
                    ReleaseDate = release,
                    MediaImageUrl = Img(title),
                    EnteredAt = DateTimeOffset.UtcNow,
                    MangaDetails = new MangaDetails
                    {
                        Publisher = publisher,
                        ChapterCount = chapters,
                        VolumeCount = volumes,
                    },
                };

                media.MediaGenres = Joins(media, gs);
                return media;
            }

            Media Movie(
                string title,
                string desc,
                DateOnly release,
                string studio,
                string[] directors,
                params Genre[] gs
            )
            {
                var media = new Media
                {
                    MediaType = MediaType.Movie,
                    Title = title.Trim(),
                    TitleNormalized = title.Trim().ToUpperInvariant(),
                    Description = desc.Trim(),
                    DescriptionNormalized = desc.Trim().ToUpperInvariant(),
                    ReleaseDate = release,
                    MediaImageUrl = Img(title),
                    EnteredAt = DateTimeOffset.UtcNow,
                    MovieDetails = new MovieDetails { Studio = studio, Directors = directors },
                };

                media.MediaGenres = Joins(media, gs);
                return media;
            }

            var Action = G("Action");
            var Adventure = G("Adventure");
            var Comedy = G("Comedy");
            var Drama = G("Drama");
            var Fantasy = G("Fantasy");
            var Horror = G("Horror");
            var Mystery = G("Mystery");
            var Romance = G("Romance");
            var SciFi = G("Sci-Fi");
            var SliceOfLife = G("Slice of Life");
            var Sports = G("Sports");
            var Supernatural = G("Supernatural");
            var Thriller = G("Thriller");
            var Mecha = G("Mecha");
            var Music = G("Music");
            var Psychological = G("Psychological");
            var Historical = G("Historical");
            var Military = G("Military");
            var Parody = G("Parody");
            var School = G("School");
            var Shounen = G("Shounen");
            var Shoujo = G("Shoujo");
            var Seinen = G("Seinen");
            var Josei = G("Josei");
            var Isekai = G("Isekai");
            var Ecchi = G("Ecchi");
            var Harem = G("Harem");
            var ReverseHarem = G("Reverse Harem");
            var Superhero = G("Superhero");
            var Cyberpunk = G("Cyberpunk");
            var PostApocalyptic = G("Post-Apocalyptic");
            var Steampunk = G("Steampunk");
            var Crime = G("Crime");
            var Detective = G("Detective");
            var MartialArts = G("Martial Arts");
            var Vampire = G("Vampire");
            var Magic = G("Magic");
            var Game = G("Game");
            var Demons = G("Demons");
            var Space = G("Space");

            return new List<Media>
            {
                Manga(
                    "Naruto",
                    "A story about a young ninja.",
                    new(2002, 10, 3),
                    "Shueisha",
                    700,
                    72,
                    Action,
                    Adventure,
                    MartialArts,
                    Shounen
                ),
                Anime(
                    "One Piece",
                    "A story about pirates searching for treasure.",
                    new(1999, 10, 20),
                    "Toei Animation",
                    1100,
                    Action,
                    Adventure,
                    Comedy,
                    Shounen
                ),
                Anime(
                    "Attack on Titan",
                    "Humans fight against giant humanoid Titans.",
                    new(2013, 4, 7),
                    "Wit Studio",
                    90,
                    Action,
                    Drama,
                    Mystery,
                    Military
                ),
                Movie(
                    "Spirited Away",
                    "A girl enters a spirit world and must find her way back.",
                    new(2001, 7, 20),
                    "Studio Ghibli",
                    new[] { "Hayao Miyazaki" },
                    Fantasy,
                    Supernatural,
                    Adventure
                ),
                Anime(
                    "Fullmetal Alchemist: Brotherhood",
                    "Two brothers search for a way to restore their bodies after forbidden alchemy.",
                    new(2009, 4, 5),
                    "Bones",
                    64,
                    Action,
                    Adventure,
                    Fantasy,
                    Drama,
                    Shounen
                ),
                Anime(
                    "Death Note",
                    "A student discovers a notebook that can kill anyone whose name is written in it.",
                    new(2006, 10, 4),
                    "Madhouse",
                    37,
                    Mystery,
                    Thriller,
                    Psychological,
                    Crime
                ),
                Anime(
                    "Demon Slayer: Kimetsu no Yaiba",
                    "A boy joins demon slayers to cure his sister and avenge his family.",
                    new(2019, 4, 6),
                    "ufotable",
                    55,
                    Action,
                    Demons,
                    Supernatural,
                    Shounen
                ),
                Anime(
                    "Jujutsu Kaisen",
                    "A teen becomes host to a powerful curse and joins a school of sorcerers.",
                    new(2020, 10, 3),
                    "MAPPA",
                    48,
                    Action,
                    Supernatural,
                    Horror,
                    Shounen,
                    School
                ),
                Anime(
                    "My Hero Academia",
                    "In a world of superpowers, a powerless boy dreams of becoming a hero.",
                    new(2016, 4, 3),
                    "Bones",
                    150,
                    Action,
                    Superhero,
                    School,
                    Shounen
                ),
                Anime(
                    "Hunter x Hunter",
                    "A boy sets out to become a Hunter and find his father.",
                    new(2011, 10, 2),
                    "Madhouse",
                    148,
                    Action,
                    Adventure,
                    Fantasy,
                    Shounen
                ),
                Anime(
                    "Bleach",
                    "A teen gains Soul Reaper powers and protects the living from evil spirits.",
                    new(2004, 10, 5),
                    "Pierrot",
                    366,
                    Action,
                    Supernatural,
                    Adventure,
                    Shounen
                ),
                Anime(
                    "Dragon Ball Z",
                    "Earth’s mightiest fighters defend the planet from increasingly powerful threats.",
                    new(1989, 4, 26),
                    "Toei Animation",
                    291,
                    Action,
                    Adventure,
                    MartialArts,
                    Shounen
                ),
                Anime(
                    "Steins;Gate",
                    "A group of friends accidentally discovers a method of time travel with consequences.",
                    new(2011, 4, 6),
                    "White Fox",
                    24,
                    SciFi,
                    Thriller,
                    Psychological,
                    Mystery
                ),
                Anime(
                    "Cowboy Bebop",
                    "Bounty hunters drift through space chasing criminals and their pasts.",
                    new(1998, 4, 3),
                    "Sunrise",
                    26,
                    Action,
                    SciFi,
                    Space,
                    Crime
                ),
                Anime(
                    "Neon Genesis Evangelion",
                    "Teen pilots defend humanity in biomechanical mechs against mysterious beings.",
                    new(1995, 10, 4),
                    "Gainax",
                    26,
                    Mecha,
                    SciFi,
                    Psychological,
                    Drama
                ),
                Anime(
                    "Haikyuu!!",
                    "A determined volleyball player chases greatness with a ragtag team.",
                    new(2014, 4, 6),
                    "Production I.G",
                    85,
                    Sports,
                    Comedy,
                    School,
                    Shounen
                ),
                Movie(
                    "Your Name",
                    "Two strangers find themselves linked across distance and time.",
                    new(2016, 8, 26),
                    "CoMix Wave Films",
                    new[] { "Makoto Shinkai" },
                    Romance,
                    Drama,
                    Supernatural
                ),
                Movie(
                    "Princess Mononoke",
                    "A prince becomes entangled in a conflict between nature and industry.",
                    new(1997, 7, 12),
                    "Studio Ghibli",
                    new[] { "Hayao Miyazaki" },
                    Fantasy,
                    Adventure,
                    Drama
                ),
                Movie(
                    "Akira",
                    "A biker gang member gains dangerous powers in a dystopian future Tokyo.",
                    new(1988, 7, 16),
                    "Tokyo Movie Shinsha",
                    new[] { "Katsuhiro Otomo" },
                    SciFi,
                    Cyberpunk,
                    Action
                ),
                Movie(
                    "Ghost in the Shell",
                    "A cyborg officer investigates cybercrime and questions identity and consciousness.",
                    new(1995, 11, 18),
                    "Production I.G",
                    new[] { "Mamoru Oshii" },
                    SciFi,
                    Cyberpunk,
                    Crime
                ),
                Manga(
                    "Tokyo Ghoul",
                    "A student is pulled into a hidden world where ghouls survive by eating humans.",
                    new(2011, 9, 8),
                    "Shueisha",
                    143,
                    14,
                    Horror,
                    Supernatural,
                    Thriller,
                    Seinen
                ),
                Manga(
                    "Berserk",
                    "A mercenary struggles through brutal wars and supernatural horrors.",
                    new(1989, 8, 25),
                    "Hakusensha",
                    370,
                    41,
                    Action,
                    Horror,
                    Fantasy,
                    Seinen
                ),
                Manga(
                    "Vagabond",
                    "A wandering swordsman seeks meaning and mastery through duels and hardship.",
                    new(1998, 9, 3),
                    "Kodansha",
                    327,
                    37,
                    Action,
                    Historical,
                    Drama,
                    Seinen,
                    MartialArts
                ),
                Manga(
                    "Vinland Saga",
                    "A young warrior is consumed by revenge amid the Viking age.",
                    new(2005, 4, 13),
                    "Kodansha",
                    210,
                    27,
                    Historical,
                    Drama,
                    Action,
                    Seinen
                ),
                Manga(
                    "One Punch Man",
                    "A hero who can defeat anyone with one punch searches for a real challenge.",
                    new(2012, 6, 14),
                    "Shueisha",
                    200,
                    30,
                    Action,
                    Comedy,
                    Superhero,
                    Parody
                ),
                Manga(
                    "Chainsaw Man",
                    "A desperate teen merges with a devil and becomes a chaotic devil hunter.",
                    new(2018, 12, 3),
                    "Shueisha",
                    160,
                    16,
                    Action,
                    Horror,
                    Demons,
                    Shounen
                ),
                Manga(
                    "Spy x Family",
                    "A spy forms a fake family—unaware his wife is an assassin and his child can read minds.",
                    new(2019, 3, 25),
                    "Shueisha",
                    100,
                    12,
                    Comedy,
                    Action,
                    Romance,
                    Crime
                ),
                Manga(
                    "Slam Dunk",
                    "A delinquent joins a basketball team and discovers a passion for the sport.",
                    new(1990, 10, 1),
                    "Shueisha",
                    276,
                    31,
                    Sports,
                    Comedy,
                    School,
                    Shounen
                ),
                Manga(
                    "JoJo's Bizarre Adventure",
                    "Generations of the Joestar family battle supernatural foes with style and grit.",
                    new(1987, 1, 1),
                    "Shueisha",
                    900,
                    130,
                    Action,
                    Supernatural,
                    Adventure,
                    Shounen
                ),
                Manga(
                    "Fruits Basket",
                    "A girl befriends a family cursed to transform into animals of the zodiac.",
                    new(1998, 7, 18),
                    "Hakusensha",
                    136,
                    23,
                    Romance,
                    Drama,
                    Supernatural,
                    Shoujo
                ),
                Manga(
                    "Kaguya-sama: Love Is War",
                    "Two geniuses turn romance into psychological warfare.",
                    new(2015, 5, 19),
                    "Shueisha",
                    281,
                    28,
                    Romance,
                    Comedy,
                    Psychological,
                    School,
                    Seinen
                ),
                Anime(
                    "Re:ZERO -Starting Life in Another World-",
                    "A boy is transported to another world and relives tragic events through time loops.",
                    new(2016, 4, 4),
                    "White Fox",
                    50,
                    Isekai,
                    Fantasy,
                    Drama,
                    Psychological
                ),
                Anime(
                    "Sword Art Online",
                    "Players trapped in a VRMMO must clear the game to escape.",
                    new(2012, 7, 8),
                    "A-1 Pictures",
                    100,
                    Adventure,
                    Fantasy,
                    Game,
                    Romance
                ),
                Anime(
                    "Code Geass",
                    "A banished prince gains a power to command and launches a rebellion.",
                    new(2006, 10, 6),
                    "Sunrise",
                    50,
                    Mecha,
                    Military,
                    Drama,
                    Thriller
                ),
                Anime(
                    "Gintama",
                    "Odd jobs and absurd comedy collide with heartfelt action in an alien-occupied Edo.",
                    new(2006, 4, 4),
                    "Sunrise",
                    360,
                    Comedy,
                    Parody,
                    Action,
                    Shounen
                ),
                Anime(
                    "Black Clover",
                    "A boy without magic aims to become the Wizard King through grit and friendship.",
                    new(2017, 10, 3),
                    "Pierrot",
                    170,
                    Action,
                    Fantasy,
                    Magic,
                    Shounen
                ),
                Anime(
                    "Fairy Tail",
                    "Wizards in a guild take on jobs, rivals, and world-threatening foes.",
                    new(2009, 10, 12),
                    "A-1 Pictures",
                    328,
                    Action,
                    Adventure,
                    Fantasy,
                    Magic
                ),
                Manga(
                    "The Promised Neverland",
                    "Children uncover a terrifying secret and plot an escape from their idyllic home.",
                    new(2016, 8, 1),
                    "Shueisha",
                    181,
                    20,
                    Thriller,
                    Mystery,
                    Horror,
                    Psychological
                ),
                Anime(
                    "Mob Psycho 100",
                    "A powerful psychic tries to live a normal life while spirits and rivals escalate.",
                    new(2016, 7, 12),
                    "Bones",
                    37,
                    Comedy,
                    Supernatural,
                    Action,
                    School
                ),
                Anime(
                    "Dr. Stone",
                    "A genius teen rebuilds civilization with science after humanity turns to stone.",
                    new(2019, 7, 5),
                    "TMS Entertainment",
                    60,
                    Adventure,
                    SciFi,
                    Comedy,
                    Shounen
                ),
                Anime(
                    "Made in Abyss",
                    "A girl descends into a vast abyss where wonders and horrors grow deeper.",
                    new(2017, 7, 7),
                    "Kinema Citrus",
                    25,
                    Adventure,
                    Fantasy,
                    Mystery,
                    Horror
                ),
                Movie(
                    "A Silent Voice",
                    "A former bully seeks redemption with the girl he once tormented.",
                    new(2016, 9, 17),
                    "Kyoto Animation",
                    new[] { "Naoko Yamada" },
                    Drama,
                    Romance,
                    School
                ),
                Movie(
                    "Weathering With You",
                    "A runaway boy meets a girl who can change the weather in a rain-soaked city.",
                    new(2019, 7, 19),
                    "CoMix Wave Films",
                    new[] { "Makoto Shinkai" },
                    Romance,
                    Drama,
                    Fantasy
                ),
                Movie(
                    "Howl's Moving Castle",
                    "A young woman cursed with old age finds refuge in a wizard’s moving castle.",
                    new(2004, 11, 20),
                    "Studio Ghibli",
                    new[] { "Hayao Miyazaki" },
                    Fantasy,
                    Romance,
                    Adventure
                ),
                Movie(
                    "The Wind Rises",
                    "A passionate engineer pursues his dream of designing airplanes amid turbulent times.",
                    new(2013, 7, 20),
                    "Studio Ghibli",
                    new[] { "Hayao Miyazaki" },
                    Drama,
                    Historical,
                    Romance
                ),
                Movie(
                    "Perfect Blue",
                    "A pop idol’s life fractures as obsession and paranoia blur reality.",
                    new(1997, 2, 28),
                    "Madhouse",
                    new[] { "Satoshi Kon" },
                    Thriller,
                    Psychological,
                    Mystery
                ),
                Movie(
                    "Summer Wars",
                    "A math prodigy gets pulled into a digital crisis that threatens the real world.",
                    new(2009, 8, 1),
                    "Madhouse",
                    new[] { "Mamoru Hosoda" },
                    SciFi,
                    Comedy
                ),
                Movie(
                    "Paprika",
                    "A device that enters dreams is stolen, unleashing surreal chaos.",
                    new(2006, 11, 25),
                    "Madhouse",
                    new[] { "Satoshi Kon" },
                    SciFi,
                    Psychological,
                    Mystery
                ),
                Movie(
                    "Kiki's Delivery Service",
                    "A young witch starts a delivery service to find independence and confidence.",
                    new(1989, 7, 29),
                    "Studio Ghibli",
                    new[] { "Hayao Miyazaki" },
                    Fantasy,
                    SliceOfLife
                ),
                Movie(
                    "Nausicaä of the Valley of the Wind",
                    "A princess strives to understand a toxic world and prevent war from destroying it.",
                    new(1984, 3, 11),
                    "Topcraft",
                    new[] { "Hayao Miyazaki" },
                    Fantasy,
                    PostApocalyptic,
                    Adventure
                ),
                Anime(
                    "Naruto Shippuden",
                    "The continuation of Naruto's journey as he faces greater threats and deeper bonds.",
                    new(2007, 2, 15),
                    "Pierrot",
                    500,
                    Action,
                    Adventure,
                    MartialArts,
                    Shounen
                ),
                Anime(
                    "Dorohedoro",
                    "A man with a lizard head searches for the sorcerer who cursed him.",
                    new(2020, 1, 12),
                    "MAPPA",
                    12,
                    Action,
                    Fantasy,
                    Horror,
                    Comedy
                ),
                Anime(
                    "Kuroko's Basketball",
                    "A shadowy sixth man helps a rising team challenge Japan’s best.",
                    new(2012, 4, 7),
                    "Production I.G",
                    75,
                    Sports,
                    School,
                    Shounen
                ),
                Anime(
                    "Tokyo Revengers",
                    "A time-leaper tries to change the future by rewriting the past of a gang war.",
                    new(2021, 4, 11),
                    "Liden Films",
                    50,
                    Drama,
                    Crime,
                    Thriller,
                    School
                ),
                Anime(
                    "Blue Lock",
                    "Strikers compete in an extreme program designed to create Japan’s ultimate forward.",
                    new(2022, 10, 9),
                    "Eight Bit",
                    24,
                    Sports,
                    Psychological,
                    Drama
                ),
                Anime(
                    "Frieren: Beyond Journey's End",
                    "An elf mage reflects on time, loss, and the meaning of connections after a hero’s quest.",
                    new(2023, 9, 29),
                    "Madhouse",
                    28,
                    Fantasy,
                    Drama,
                    Adventure,
                    SliceOfLife
                ),
                Manga(
                    "Monster",
                    "A doctor’s life spirals after saving a boy who grows into a terrifying criminal.",
                    new(1994, 12, 5),
                    "Shogakukan",
                    162,
                    18,
                    Mystery,
                    Thriller,
                    Crime,
                    Psychological,
                    Seinen
                ),
                Manga(
                    "20th Century Boys",
                    "Old friends confront a doomsday cult tied to their childhood games.",
                    new(1999, 9, 27),
                    "Shogakukan",
                    249,
                    22,
                    Mystery,
                    Thriller,
                    Crime,
                    SciFi,
                    Seinen
                ),
                Manga(
                    "Claymore",
                    "A warrior with demonic power hunts monsters while battling her own nature.",
                    new(2001, 6, 6),
                    "Shueisha",
                    155,
                    27,
                    Action,
                    Horror,
                    Demons,
                    Fantasy
                ),
                Manga(
                    "Hellsing",
                    "A secret organization battles vampires and warping horrors behind the scenes.",
                    new(1997, 4, 30),
                    "Shonen Gahosha",
                    95,
                    10,
                    Horror,
                    Vampire,
                    Action,
                    Supernatural
                ),
            };
        }
    }
}
