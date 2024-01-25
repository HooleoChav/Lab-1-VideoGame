namespace VideoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "videogames.csv";
            List<VideoGame> videoGames = new List<VideoGame>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip the header line with all the column names n stuff (why this take me 2 hours to figure out).
                while (!reader.EndOfStream)
                {
                    string[] columns = reader.ReadLine().Split(',');
                    VideoGame videoGame = new VideoGame
                    {
                        Name = columns[0],
                        Platform = columns[1],
                        Year = int.Parse(columns[2]),
                        Genre = columns[3],
                        Publisher = columns[4],
                        NA_Sales = float.Parse(columns[5]),
                        EU_Sales = float.Parse(columns[6]),
                        JP_Sales = float.Parse(columns[7]),
                        Other_Sales = float.Parse(columns[8]),
                        Global_Sales = float.Parse(columns[9])
                    };
                    videoGames.Add(videoGame);
                }
            }

            // This is where the method is called to get the User's input for the publisher
            PublisherData(videoGames);

            //This is where the method is called to get the User's input for the genre
            GenreData(videoGames);
        }

        // Calculating stuff below
        static void calculatingPercent(List<VideoGame> games, string selectedValue)
        {
            int totalGames = games.Count;
            int selectedValueGames = games.Count(game => game.Genre == selectedValue || game.Publisher == selectedValue);

            double valuePercentage = (double)selectedValueGames / totalGames * 100;

            Console.WriteLine($"\nOut of {totalGames} games in our CSV file, {selectedValueGames} are related to '{selectedValue}', which is {valuePercentage:F2}%");
        }

        // Method for publisher
        static void PublisherData(List<VideoGame> games)
        {
            Console.Write("\nEnter a publisher to analyse: ");
            string userInputPublisher = Console.ReadLine();

            // Filtering for getting the publisher information.
            List<VideoGame> userPublisherGames = games
                .Where(game => game.Publisher == userInputPublisher)
                .OrderBy(game => game.Name)
                .ToList();

            // The games all sorted (Alphabetically/Ascending)
            Console.WriteLine($"\nGames of Publisher '{userInputPublisher}' (Sorted Alphabetically):");
            foreach (var game in userPublisherGames)
            {
                Console.WriteLine(game);
            }

            // Calculate and display the percentage for the user-inputted publisher
            calculatingPercent(games, userInputPublisher);
        }

        // Method for genre
        static void GenreData(List<VideoGame> games)
        {
            Console.Write("\nNow Choose a genre to analyse: ");
            string userInputGenre = Console.ReadLine();

            // Filtering 
            List<VideoGame> userGenreGames = games
                .Where(game => game.Genre == userInputGenre)
                .OrderBy(game => game.Name)
                .ToList();

            // Display each item in the filtered and sorted genre list
            Console.WriteLine($"\nGames of Genre '{userInputGenre}' (Sorted Alphabetically):");
            foreach (var game in userGenreGames)
            {
                Console.WriteLine(game);
            }
            calculatingPercent(games, userInputGenre);
        }

        class VideoGame : IComparable<VideoGame>
        {
            public string Name { get; set; }
            public string Platform { get; set; }
            public int Year { get; set; }
            public string Genre { get; set; }
            public string Publisher { get; set; }
            public float NA_Sales { get; set; }
            public float EU_Sales { get; set; }
            public float JP_Sales { get; set; }
            public float Other_Sales { get; set; }
            public float Global_Sales { get; set; }

            public int CompareTo(VideoGame other)
            {
                return string.Compare(Name, other.Name, StringComparison.Ordinal);
            }

            public override string ToString()
            {
                return $"Name: {Name}, Platform: {Platform}, Year: {Year}, Genre: {Genre}, Publisher: {Publisher}, " +
                    $"NA_Sales: {NA_Sales}, EU_Sales: {EU_Sales}, JP_Sales: {JP_Sales}, Other_Sales: {Other_Sales}, " +
                    $"Global_Sales: {Global_Sales}";
            }
        }
    }
}