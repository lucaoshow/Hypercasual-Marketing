using Root.Frogger.Score;
using Root.General.Utils.Scenes;
using Root.Shooter.Score;
using UnityEngine;
using UnityEngine.UI;

namespace Root.General.Retry
{
    public class RetryManager : MonoBehaviour
    {
        [SerializeField] private GameInfo gameInfo;
        [SerializeField] private Text highscore;

        private void Start()
        {
            this.highscore.text = "Highscore:\n" + (this.gameInfo.gameId == 2 ? FroggerScoreManager.Instance.HighestScore.ToString() : ShooterScoreManager.Instance.HighestScore.ToString());    
        }

        public void OnRetryButtonPressed() 
        {
            if (this.gameInfo.gameId == 2)
            {
                FroggerScoreManager.Instance.ResetScore();
            }
            else
            {
                ShooterScoreManager.Instance.ResetScore();
            }
            
            SceneHelper.UnloadScene("RetryScene");
            SceneHelper.LoadScene(this.gameInfo.gameId);
        }

        public void OnLeaderboardButtonPressed() 
        {
            SceneHelper.LoadScene("LeaderboardScene");
        }   

        public void OnQuitButtonPressed() 
        {
            SceneHelper.LoadScene("StartScene");
        }
    }
}
