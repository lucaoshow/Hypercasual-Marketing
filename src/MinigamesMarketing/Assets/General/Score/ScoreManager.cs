namespace Root.General.Score
{
    public abstract class ScoreManager
    {
        public int Score { get; private set; } = 0;
        public int HighestScore { get; private set; } = 0;
        public bool ShouldSendScore;

        public void IncrementScore()
        {
            this.Score++;
            if (this.Score >= this.HighestScore) 
            { 
                this.HighestScore = this.Score;
                this.ShouldSendScore = true;
            }
        }

        public void IncrementScore(int increment)
        {
            this.Score += increment;
            if (this.Score >= this.HighestScore)
            { 
                this.HighestScore = this.Score;
                this.ShouldSendScore = true;
            }
        }

        public void ResetScore()
        {
            this.Score = 0;
            this.ShouldSendScore = false;
        }
    }
}
