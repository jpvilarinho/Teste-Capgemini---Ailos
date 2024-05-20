using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class FootballDataService
{
    public static async Task<int> GetTotalScoredGoalsAsync(string team, int year)
    {
        int totalGoals = 0;
        HttpClient client = new HttpClient();

        int page = 1;
        while (true)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(responseBody);
            JsonElement root = json.RootElement;
            JsonElement data = root.GetProperty("data");
            if (data.GetArrayLength() == 0) break;

            foreach (var match in data.EnumerateArray())
            {
                totalGoals += match.GetProperty("team1goals").GetInt32();
            }
            page++;
        }

        page = 1;
        while (true)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(responseBody);
            JsonElement root = json.RootElement;
            JsonElement data = root.GetProperty("data");
            if (data.GetArrayLength() == 0) break;

            foreach (var match in data.EnumerateArray())
            {
                totalGoals += match.GetProperty("team2goals").GetInt32();
            }
            page++;
        }

        return totalGoals;
    }
}
