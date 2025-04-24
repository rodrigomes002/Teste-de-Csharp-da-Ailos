using Newtonsoft.Json;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;

        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public async static Task<int> getTotalScoredGoals(string team, int year)
    {
        int goalsAsTeam1 = await GetTotalGoalsForTeamPosition(team, year, "team1");

        int goalsAsTeam2 = await GetTotalGoalsForTeamPosition(team, year, "team2");

        return goalsAsTeam1 + goalsAsTeam2;
    }

    private static async Task<int> GetTotalGoalsForTeamPosition(string team, int year, string position)
    {
        var initialResponse = await GetMatchResponseAsync(team, year, position, 1);
        var allMatchData = initialResponse.Data;
        int totalPages = initialResponse.Total_Pages;

        for (int page = 2; page <= totalPages; page++)
        {
            var response = await GetMatchResponseAsync(team, year, position, page);
            allMatchData.AddRange(response.Data);
        }

        return position == "team1"
        ? allMatchData.Sum(match => int.Parse(match.Team1Goals))
        : allMatchData.Sum(match => int.Parse(match.Team2Goals));
    }

    public static async Task<MatchResponse> GetMatchResponseAsync(string team, int year, string teamQueryString, int page)
    {
        var client = new HttpClient();

        var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamQueryString}={team}&page={page}";

        var result = await client.GetStringAsync(url);

        var matchResponse = JsonConvert.DeserializeObject<MatchResponse>(result);

        return matchResponse;
    }
}


public class Match
{
    public string Competition { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Round { get; set; } = string.Empty;
    public string Team1 { get; set; } = string.Empty;
    public string Team2 { get; set; } = string.Empty;
    public string Team1Goals { get; set; } = string.Empty;
    public string Team2Goals { get; set; } = string.Empty;
}

public class MatchResponse
{
    public int Page { get; set; }
    public int Per_Page { get; set; }
    public int Total { get; set; }
    public int Total_Pages { get; set; }
    public List<Match> Data { get; set; } = new List<Match>();
}
