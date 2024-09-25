using Root.General.Score;

namespace Root.Frogger.Score
{
    public class FroggerScoreManager : ScoreManager
    {
        private static FroggerScoreManager instance;
        public static FroggerScoreManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FroggerScoreManager();
                }
                return instance;
            }
        }
    }
}
