using System;
using System.Collections.Generic;

namespace Root.General.API
{
    [Serializable]
    public class Leaderboard : IGetResultContainer<Leaderboard>
    {
        public List<LeaderboardLine> lines = new List<LeaderboardLine>();

        public void AddRange(Leaderboard leaderboard)
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
        public string player;
        public int score;
    }
}
