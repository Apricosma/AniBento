using AniBento.Api.Models;
using AniBento.Api.Models.Enums;

namespace AniBento.Api.Data.DbSeedData;

public static class DbMediaSeed
{
    public static readonly Media[] Medias =
    [
        new Media
        {
            Title = "Naruto",
            Description = "A story about a young ninja.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2002, 10, 3),
            MediaImageUrl = "https://example.com/naruto.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 700,
                VolumeCount = 72,
                Genres = ["Action", "Adventure", "Shounen"],
            },
        },
        new Media
        {
            Title = "One Piece",
            Description = "A story about pirates searching for treasure.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(1999, 10, 20),
            MediaImageUrl = "https://example.com/onepiece.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Toei Animation",
                EpisodeCount = 1100,
                Genres = ["Action", "Adventure", "Shounen"],
            },
        },
        new Media
        {
            Title = "Attack on Titan",
            Description = "Humans fight against giant humanoid Titans.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2013, 4, 7),
            MediaImageUrl = "https://example.com/aot.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Wit Studio",
                EpisodeCount = 90,
                Genres = ["Action", "Drama", "Dark Fantasy"],
            },
        },
        new Media
        {
            Title = "Spirited Away",
            Description = "A girl enters a spirit world and must find her way back.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2001, 7, 20),
            MediaImageUrl = "https://example.com/spiritedaway.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Fantasy", "Adventure"],
            },
        },
        new Media
        {
            Title = "Fullmetal Alchemist: Brotherhood",
            Description =
                "Two brothers search for a way to restore their bodies after forbidden alchemy.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2009, 4, 5),
            MediaImageUrl = "https://example.com/fmab.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Bones",
                EpisodeCount = 64,
                Genres = ["Action", "Adventure", "Drama"],
            },
        },
        new Media
        {
            Title = "Death Note",
            Description =
                "A student discovers a notebook that can kill anyone whose name is written in it.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2006, 10, 4),
            MediaImageUrl = "https://example.com/deathnote.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Madhouse",
                EpisodeCount = 37,
                Genres = ["Thriller", "Mystery", "Supernatural"],
            },
        },
        new Media
        {
            Title = "Demon Slayer: Kimetsu no Yaiba",
            Description = "A boy joins demon slayers to cure his sister and avenge his family.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2019, 4, 6),
            MediaImageUrl = "https://example.com/demonslayer.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "ufotable",
                EpisodeCount = 55,
                Genres = ["Action", "Adventure", "Supernatural"],
            },
        },
        new Media
        {
            Title = "Jujutsu Kaisen",
            Description =
                "A teen becomes host to a powerful curse and joins a school of sorcerers.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2020, 10, 3),
            MediaImageUrl = "https://example.com/jujutsukaisen.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "MAPPA",
                EpisodeCount = 48,
                Genres = ["Action", "Dark Fantasy", "Shounen"],
            },
        },
        new Media
        {
            Title = "My Hero Academia",
            Description = "In a world of superpowers, a powerless boy dreams of becoming a hero.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2016, 4, 3),
            MediaImageUrl = "https://example.com/mha.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Bones",
                EpisodeCount = 150,
                Genres = ["Action", "Superhero", "Shounen"],
            },
        },
        new Media
        {
            Title = "Hunter x Hunter",
            Description = "A boy sets out to become a Hunter and find his father.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2011, 10, 2),
            MediaImageUrl = "https://example.com/hxh.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Madhouse",
                EpisodeCount = 148,
                Genres = ["Action", "Adventure", "Shounen"],
            },
        },
        new Media
        {
            Title = "Bleach",
            Description =
                "A teen gains Soul Reaper powers and protects the living from evil spirits.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2004, 10, 5),
            MediaImageUrl = "https://example.com/bleach.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Pierrot",
                EpisodeCount = 366,
                Genres = ["Action", "Supernatural", "Shounen"],
            },
        },
        new Media
        {
            Title = "Dragon Ball Z",
            Description =
                "Earth’s mightiest fighters defend the planet from increasingly powerful threats.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(1989, 4, 26),
            MediaImageUrl = "https://example.com/dbz.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Toei Animation",
                EpisodeCount = 291,
                Genres = ["Action", "Martial Arts", "Shounen"],
            },
        },
        new Media
        {
            Title = "Steins;Gate",
            Description =
                "A group of friends accidentally discovers a method of time travel with consequences.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2011, 4, 6),
            MediaImageUrl = "https://example.com/steinsgate.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "White Fox",
                EpisodeCount = 24,
                Genres = ["Sci-Fi", "Thriller", "Drama"],
            },
        },
        new Media
        {
            Title = "Cowboy Bebop",
            Description = "Bounty hunters drift through space chasing criminals and their pasts.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(1998, 4, 3),
            MediaImageUrl = "https://example.com/cowboybebop.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Sunrise",
                EpisodeCount = 26,
                Genres = ["Sci-Fi", "Action", "Neo-noir"],
            },
        },
        new Media
        {
            Title = "Neon Genesis Evangelion",
            Description =
                "Teen pilots defend humanity in biomechanical mechs against mysterious beings.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(1995, 10, 4),
            MediaImageUrl = "https://example.com/eva.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Gainax",
                EpisodeCount = 26,
                Genres = ["Mecha", "Psychological", "Sci-Fi"],
            },
        },
        new Media
        {
            Title = "Haikyuu!!",
            Description = "A determined volleyball player chases greatness with a ragtag team.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2014, 4, 6),
            MediaImageUrl = "https://example.com/haikyuu.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Production I.G",
                EpisodeCount = 85,
                Genres = ["Sports", "Comedy", "Shounen"],
            },
        },
        new Media
        {
            Title = "Your Name",
            Description = "Two strangers find themselves linked across distance and time.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2016, 8, 26),
            MediaImageUrl = "https://example.com/yourname.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "CoMix Wave Films",
                Directors = ["Makoto Shinkai"],
                Genres = ["Romance", "Drama", "Fantasy"],
            },
        },
        new Media
        {
            Title = "Princess Mononoke",
            Description = "A prince becomes entangled in a conflict between nature and industry.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1997, 7, 12),
            MediaImageUrl = "https://example.com/mononoke.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Fantasy", "Adventure", "Drama"],
            },
        },
        new Media
        {
            Title = "Akira",
            Description = "A biker gang member gains dangerous powers in a dystopian future Tokyo.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1988, 7, 16),
            MediaImageUrl = "https://example.com/akira.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Tokyo Movie Shinsha",
                Directors = ["Katsuhiro Otomo"],
                Genres = ["Sci-Fi", "Action", "Cyberpunk"],
            },
        },
        new Media
        {
            Title = "Ghost in the Shell",
            Description =
                "A cyborg officer investigates cybercrime and questions identity and consciousness.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1995, 11, 18),
            MediaImageUrl = "https://example.com/gits.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Production I.G",
                Directors = ["Mamoru Oshii"],
                Genres = ["Sci-Fi", "Cyberpunk", "Thriller"],
            },
        },
        new Media
        {
            Title = "Tokyo Ghoul",
            Description =
                "A student is pulled into a hidden world where ghouls survive by eating humans.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2011, 9, 8),
            MediaImageUrl = "https://example.com/tokyoghoul.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 143,
                VolumeCount = 14,
                Genres = ["Horror", "Dark Fantasy", "Seinen"],
            },
        },
        new Media
        {
            Title = "Berserk",
            Description = "A mercenary struggles through brutal wars and supernatural horrors.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1989, 8, 25),
            MediaImageUrl = "https://example.com/berserk.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Hakusensha",
                ChapterCount = 370,
                VolumeCount = 41,
                Genres = ["Dark Fantasy", "Action", "Seinen"],
            },
        },
        new Media
        {
            Title = "Vagabond",
            Description =
                "A wandering swordsman seeks meaning and mastery through duels and hardship.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1998, 9, 3),
            MediaImageUrl = "https://example.com/vagabond.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Kodansha",
                ChapterCount = 327,
                VolumeCount = 37,
                Genres = ["Historical", "Action", "Seinen"],
            },
        },
        new Media
        {
            Title = "Vinland Saga",
            Description = "A young warrior is consumed by revenge amid the Viking age.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2005, 4, 13),
            MediaImageUrl = "https://example.com/vinlandsaga.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Kodansha",
                ChapterCount = 210,
                VolumeCount = 27,
                Genres = ["Historical", "Action", "Drama"],
            },
        },
        new Media
        {
            Title = "One Punch Man",
            Description =
                "A hero who can defeat anyone with one punch searches for a real challenge.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2012, 6, 14),
            MediaImageUrl = "https://example.com/opm.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 200,
                VolumeCount = 30,
                Genres = ["Action", "Comedy", "Superhero"],
            },
        },
        new Media
        {
            Title = "Chainsaw Man",
            Description =
                "A desperate teen merges with a devil and becomes a chaotic devil hunter.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2018, 12, 3),
            MediaImageUrl = "https://example.com/chainsawman.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 160,
                VolumeCount = 16,
                Genres = ["Action", "Horror", "Dark Fantasy"],
            },
        },
        new Media
        {
            Title = "Spy x Family",
            Description =
                "A spy forms a fake family—unaware his wife is an assassin and his child can read minds.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2019, 3, 25),
            MediaImageUrl = "https://example.com/spyfamily.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 100,
                VolumeCount = 12,
                Genres = ["Comedy", "Action", "Slice of Life"],
            },
        },
        new Media
        {
            Title = "Slam Dunk",
            Description =
                "A delinquent joins a basketball team and discovers a passion for the sport.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1990, 10, 1),
            MediaImageUrl = "https://example.com/slamdunk.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 276,
                VolumeCount = 31,
                Genres = ["Sports", "Comedy", "Shounen"],
            },
        },
        new Media
        {
            Title = "JoJo's Bizarre Adventure",
            Description =
                "Generations of the Joestar family battle supernatural foes with style and grit.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1987, 1, 1),
            MediaImageUrl = "https://example.com/jojo.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 900,
                VolumeCount = 130,
                Genres = ["Action", "Adventure", "Supernatural"],
            },
        },
        new Media
        {
            Title = "Fruits Basket",
            Description =
                "A girl befriends a family cursed to transform into animals of the zodiac.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1998, 7, 18),
            MediaImageUrl = "https://example.com/fruitsbasket.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Hakusensha",
                ChapterCount = 136,
                VolumeCount = 23,
                Genres = ["Romance", "Drama", "Slice of Life"],
            },
        },
        new Media
        {
            Title = "Kaguya-sama: Love Is War",
            Description = "Two geniuses turn romance into psychological warfare.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2015, 5, 19),
            MediaImageUrl = "https://example.com/kaguya.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 281,
                VolumeCount = 28,
                Genres = ["Comedy", "Romance", "Seinen"],
            },
        },
        new Media
        {
            Title = "Re:ZERO -Starting Life in Another World-",
            Description =
                "A boy is transported to another world and relives tragic events through time loops.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2016, 4, 4),
            MediaImageUrl = "https://example.com/rezero.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "White Fox",
                EpisodeCount = 50,
                Genres = ["Isekai", "Drama", "Fantasy"],
            },
        },
        new Media
        {
            Title = "Sword Art Online",
            Description = "Players trapped in a VRMMO must clear the game to escape.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2012, 7, 8),
            MediaImageUrl = "https://example.com/sao.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "A-1 Pictures",
                EpisodeCount = 100,
                Genres = ["Action", "Adventure", "Isekai"],
            },
        },
        new Media
        {
            Title = "Code Geass",
            Description = "A banished prince gains a power to command and launches a rebellion.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2006, 10, 6),
            MediaImageUrl = "https://example.com/codegeass.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Sunrise",
                EpisodeCount = 50,
                Genres = ["Mecha", "Thriller", "Drama"],
            },
        },
        new Media
        {
            Title = "Gintama",
            Description =
                "Odd jobs and absurd comedy collide with heartfelt action in an alien-occupied Edo.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2006, 4, 4),
            MediaImageUrl = "https://example.com/gintama.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Sunrise",
                EpisodeCount = 360,
                Genres = ["Comedy", "Action", "Parody"],
            },
        },
        new Media
        {
            Title = "Black Clover",
            Description =
                "A boy without magic aims to become the Wizard King through grit and friendship.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2017, 10, 3),
            MediaImageUrl = "https://example.com/blackclover.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Pierrot",
                EpisodeCount = 170,
                Genres = ["Action", "Fantasy", "Shounen"],
            },
        },
        new Media
        {
            Title = "Fairy Tail",
            Description = "Wizards in a guild take on jobs, rivals, and world-threatening foes.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2009, 10, 12),
            MediaImageUrl = "https://example.com/fairytail.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "A-1 Pictures",
                EpisodeCount = 328,
                Genres = ["Action", "Adventure", "Fantasy"],
            },
        },
        new Media
        {
            Title = "The Promised Neverland",
            Description =
                "Children uncover a terrifying secret and plot an escape from their idyllic home.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2016, 8, 1),
            MediaImageUrl = "https://example.com/promisedneverland.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 181,
                VolumeCount = 20,
                Genres = ["Thriller", "Mystery", "Dark Fantasy"],
            },
        },
        new Media
        {
            Title = "Mob Psycho 100",
            Description =
                "A powerful psychic tries to live a normal life while spirits and rivals escalate.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2016, 7, 12),
            MediaImageUrl = "https://example.com/mobpsycho.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Bones",
                EpisodeCount = 37,
                Genres = ["Action", "Comedy", "Supernatural"],
            },
        },
        new Media
        {
            Title = "Dr. Stone",
            Description =
                "A genius teen rebuilds civilization with science after humanity turns to stone.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2019, 7, 5),
            MediaImageUrl = "https://example.com/drstone.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "TMS Entertainment",
                EpisodeCount = 60,
                Genres = ["Sci-Fi", "Adventure", "Shounen"],
            },
        },
        new Media
        {
            Title = "Made in Abyss",
            Description =
                "A girl descends into a vast abyss where wonders and horrors grow deeper.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2017, 7, 7),
            MediaImageUrl = "https://example.com/madeinabyss.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Kinema Citrus",
                EpisodeCount = 25,
                Genres = ["Adventure", "Dark Fantasy", "Mystery"],
            },
        },
        new Media
        {
            Title = "A Silent Voice",
            Description = "A former bully seeks redemption with the girl he once tormented.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2016, 9, 17),
            MediaImageUrl = "https://example.com/silentvoice.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Kyoto Animation",
                Directors = ["Naoko Yamada"],
                Genres = ["Drama", "Slice of Life"],
            },
        },
        new Media
        {
            Title = "Weathering With You",
            Description =
                "A runaway boy meets a girl who can change the weather in a rain-soaked city.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2019, 7, 19),
            MediaImageUrl = "https://example.com/weathering.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "CoMix Wave Films",
                Directors = ["Makoto Shinkai"],
                Genres = ["Romance", "Drama", "Fantasy"],
            },
        },
        new Media
        {
            Title = "Howl's Moving Castle",
            Description =
                "A young woman cursed with old age finds refuge in a wizard’s moving castle.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2004, 11, 20),
            MediaImageUrl = "https://example.com/howl.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Fantasy", "Romance", "Adventure"],
            },
        },
        new Media
        {
            Title = "The Wind Rises",
            Description =
                "A passionate engineer pursues his dream of designing airplanes amid turbulent times.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2013, 7, 20),
            MediaImageUrl = "https://example.com/windrises.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Drama", "Historical"],
            },
        },
        new Media
        {
            Title = "Perfect Blue",
            Description = "A pop idol’s life fractures as obsession and paranoia blur reality.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1997, 2, 28),
            MediaImageUrl = "https://example.com/perfectblue.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Madhouse",
                Directors = ["Satoshi Kon"],
                Genres = ["Thriller", "Psychological", "Mystery"],
            },
        },
        new Media
        {
            Title = "Summer Wars",
            Description =
                "A math prodigy gets pulled into a digital crisis that threatens the real world.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2009, 8, 1),
            MediaImageUrl = "https://example.com/summerwars.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Madhouse",
                Directors = ["Mamoru Hosoda"],
                Genres = ["Sci-Fi", "Adventure", "Comedy"],
            },
        },
        new Media
        {
            Title = "Paprika",
            Description = "A device that enters dreams is stolen, unleashing surreal chaos.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2006, 11, 25),
            MediaImageUrl = "https://example.com/paprika.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Madhouse",
                Directors = ["Satoshi Kon"],
                Genres = ["Sci-Fi", "Thriller", "Fantasy"],
            },
        },
        new Media
        {
            Title = "Kiki's Delivery Service",
            Description =
                "A young witch starts a delivery service to find independence and confidence.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1989, 7, 29),
            MediaImageUrl = "https://example.com/kiki.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Fantasy", "Slice of Life", "Adventure"],
            },
        },
        new Media
        {
            Title = "Nausicaä of the Valley of the Wind",
            Description =
                "A princess strives to understand a toxic world and prevent war from destroying it.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(1984, 3, 11),
            MediaImageUrl = "https://example.com/nausicaa.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Topcraft",
                Directors = ["Hayao Miyazaki"],
                Genres = ["Sci-Fi", "Adventure", "Fantasy"],
            },
        },
        // ---- additional items to bring the seed set to ~50 ----

        new Media
        {
            Title = "Naruto Shippuden",
            Description =
                "The continuation of Naruto's journey as he faces greater threats and deeper bonds.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2007, 2, 15),
            MediaImageUrl = "https://example.com/narutoshippuden.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Pierrot",
                EpisodeCount = 500,
                Genres = ["Action", "Adventure", "Shounen"],
            },
        },
        new Media
        {
            Title = "Dorohedoro",
            Description = "A man with a lizard head searches for the sorcerer who cursed him.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2020, 1, 12),
            MediaImageUrl = "https://example.com/dorohedoro.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "MAPPA",
                EpisodeCount = 12,
                Genres = ["Action", "Dark Fantasy", "Comedy"],
            },
        },
        new Media
        {
            Title = "Kuroko's Basketball",
            Description = "A shadowy sixth man helps a rising team challenge Japan’s best.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2012, 4, 7),
            MediaImageUrl = "https://example.com/kuroko.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Production I.G",
                EpisodeCount = 75,
                Genres = ["Sports", "Shounen", "Comedy"],
            },
        },
        new Media
        {
            Title = "Tokyo Revengers",
            Description =
                "A time-leaper tries to change the future by rewriting the past of a gang war.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2021, 4, 11),
            MediaImageUrl = "https://example.com/tokyorevengers.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Liden Films",
                EpisodeCount = 50,
                Genres = ["Action", "Drama", "Time Travel"],
            },
        },
        new Media
        {
            Title = "Blue Lock",
            Description =
                "Strikers compete in an extreme program designed to create Japan’s ultimate forward.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2022, 10, 9),
            MediaImageUrl = "https://example.com/bluelock.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Eight Bit",
                EpisodeCount = 24,
                Genres = ["Sports", "Drama", "Shounen"],
            },
        },
        new Media
        {
            Title = "Frieren: Beyond Journey's End",
            Description =
                "An elf mage reflects on time, loss, and the meaning of connections after a hero’s quest.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2023, 9, 29),
            MediaImageUrl = "https://example.com/frieren.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Madhouse",
                EpisodeCount = 28,
                Genres = ["Fantasy", "Adventure", "Drama"],
            },
        },
        new Media
        {
            Title = "Monster",
            Description =
                "A doctor’s life spirals after saving a boy who grows into a terrifying criminal.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1994, 12, 5),
            MediaImageUrl = "https://example.com/monster.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shogakukan",
                ChapterCount = 162,
                VolumeCount = 18,
                Genres = ["Thriller", "Mystery", "Seinen"],
            },
        },
        new Media
        {
            Title = "20th Century Boys",
            Description = "Old friends confront a doomsday cult tied to their childhood games.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1999, 9, 27),
            MediaImageUrl = "https://example.com/20thcb.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shogakukan",
                ChapterCount = 249,
                VolumeCount = 22,
                Genres = ["Thriller", "Mystery", "Drama"],
            },
        },
        new Media
        {
            Title = "Claymore",
            Description =
                "A warrior with demonic power hunts monsters while battling her own nature.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2001, 6, 6),
            MediaImageUrl = "https://example.com/claymore.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shueisha",
                ChapterCount = 155,
                VolumeCount = 27,
                Genres = ["Action", "Dark Fantasy", "Seinen"],
            },
        },
        new Media
        {
            Title = "Hellsing",
            Description =
                "A secret organization battles vampires and warping horrors behind the scenes.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(1997, 4, 30),
            MediaImageUrl = "https://example.com/hellsing.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shonen Gahosha",
                ChapterCount = 95,
                VolumeCount = 10,
                Genres = ["Action", "Horror", "Seinen"],
            },
        },
        new Media
        {
            Title = "Pluto",
            Description =
                "A detective investigates murders of humans and robots in a world of advanced AI.",
            MediaType = MediaType.Manga,
            ReleaseDate = new DateOnly(2003, 9, 9),
            MediaImageUrl = "https://example.com/pluto.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MangaDetails = new MangaDetails
            {
                Publisher = "Shogakukan",
                ChapterCount = 65,
                VolumeCount = 8,
                Genres = ["Sci-Fi", "Mystery", "Seinen"],
            },
        },
        new Media
        {
            Title = "K-On!",
            Description =
                "High school girls revive a light music club and learn friendship through music.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2009, 4, 3),
            MediaImageUrl = "https://example.com/kon.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Kyoto Animation",
                EpisodeCount = 39,
                Genres = ["Slice of Life", "Comedy", "Music"],
            },
        },
        new Media
        {
            Title = "Violet Evergarden",
            Description =
                "A former soldier writes letters to understand emotions and heal from war.",
            MediaType = MediaType.Anime,
            ReleaseDate = new DateOnly(2018, 1, 11),
            MediaImageUrl = "https://example.com/violet.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            AnimeDetails = new AnimeDetails
            {
                Studio = "Kyoto Animation",
                EpisodeCount = 13,
                Genres = ["Drama", "Slice of Life"],
            },
        },
        new Media
        {
            Title = "The Girl Who Leapt Through Time",
            Description =
                "A teen gains the ability to time-leap and learns the cost of changing outcomes.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2006, 7, 15),
            MediaImageUrl = "https://example.com/timelapt.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Madhouse",
                Directors = ["Mamoru Hosoda"],
                Genres = ["Sci-Fi", "Romance", "Drama"],
            },
        },
        new Media
        {
            Title = "Wolf Children",
            Description = "A mother raises two half-wolf children while protecting their secret.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2012, 7, 21),
            MediaImageUrl = "https://example.com/wolfchildren.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Chizu",
                Directors = ["Mamoru Hosoda"],
                Genres = ["Drama", "Fantasy", "Slice of Life"],
            },
        },
        new Media
        {
            Title = "The Tale of the Princess Kaguya",
            Description =
                "A bamboo cutter finds a tiny girl who grows into a radiant princess with a hidden sorrow.",
            MediaType = MediaType.Movie,
            ReleaseDate = new DateOnly(2013, 11, 23),
            MediaImageUrl = "https://example.com/kaguya_movie.jpg",
            EnteredAt = DateTimeOffset.UtcNow,
            MovieDetails = new MovieDetails
            {
                Studio = "Studio Ghibli",
                Directors = ["Isao Takahata"],
                Genres = ["Drama", "Fantasy", "Historical"],
            },
        },
    ];
}
