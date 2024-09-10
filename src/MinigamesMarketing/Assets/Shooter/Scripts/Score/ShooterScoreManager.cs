using Root.General.Score;

namespace Root.Shooter.Score
{
    public class ShooterScoreManager : ScoreManager
    {
        private static ShooterScoreManager instance;
        public static ShooterScoreManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShooterScoreManager();
                }
                return instance;
            }
        }
    }
}
