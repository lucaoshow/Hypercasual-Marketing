namespace Root.General.Score
{
    public abstract class ScoreManager
    {
        public int Score { get; private set; } = 0;
        public int HighestScore { get; private set; } = 0;

        public void IncrementScore()
        {
            this.Score++;
            if (this.Score >= this.HighestScore) { this.HighestScore = this.Score; }
        }

        public void ResetScore()
        {
            this.Score = 0;
        }
    }
}
