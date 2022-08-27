using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers;

public class TeamsLeaderboard : TeamsAnalyzer
{
    public List<TeamAndScore> TeamsAndScores { get; }

    public TeamsLeaderboard()
    {
        TeamsAndScores = new List<TeamAndScore>();
    }
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        foreach (var v in CalculatedTeamsData)
        {
            float teamScore = 0;
            var teamData = (IDictionary<string, object>)v;
            foreach (var columnField in ColumnsFields)
            {
                string nameId = columnField.Id;
                var score = float.Parse(teamData["RawValue" + nameId].ToString()!);
                teamScore += score * columnField.WeightPercent;
            }
            TeamAndScore teamAndScore = new TeamAndScore()
            {
                TeamNumber = int.Parse(teamData["TeamNumber"].ToString()!),
                Nickname = teamData["Nickname"].ToString(),
                Score = teamScore
            };
            TeamsAndScores.Add(teamAndScore);
        }
        TeamsAndScores.Sort((a, b) => b.CompareTo(a));
    }
}

public class TeamAndScore : IComparable<TeamAndScore>
{
    public int TeamNumber { get; set; }
    public string Nickname { get; set; }
    public float Score { get; set; }

    public int CompareTo(TeamAndScore other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Score.CompareTo(other.Score);
    }
}
