using System;
using System.Collections.Generic;
using Root.General.API;

namespace Root.General.Leaderboard
{
    [Serializable]
    public class LeaderboardData : IGetResultContainer<LeaderboardData>
    {
        public List<LeaderboardLine> lines = new List<LeaderboardLine>();

        public void AddRange(LeaderboardData leaderboard)
        {
            if (leaderboard.lines.Count > 0)
            {
                this.lines.AddRange(leaderboard.lines);
            }
        }

        public int Count()
        {
            return this.lines.Count;
        }
    }

    [Serializable]
    public struct LeaderboardLine 
    {
        public LeaderboardLine(string playerName, int points)
        {
            player = playerName;
            score = points;
        }
        
        public string player;
        public int score;
    }
}
