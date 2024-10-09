using UnityEngine;
using UnityEngine.UI;
using Root.General.API;
using Root.Frogger.Score;
using Root.Shooter.Score;
using System.Linq;

namespace Root.General.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private GameInfo gameInfo;
        [SerializeField] private Transform parent;
        [SerializeField] private Text textPrefab;
        [SerializeField] private int rowOffset;
        [SerializeField] private int nameWidth;
        private MarketingAPI marketingApi;
        private LeaderboardData leaderboard = new LeaderboardData();
        private bool renderedLeaderboard = false;

        void Awake()
        {
            this.marketingApi = GameObject.FindObjectOfType<MarketingAPI>();
            this.marketingApi.GetLeaderboard(this.leaderboard);
        }

        void Update()
        {
            if (!this.renderedLeaderboard && this.leaderboard.Count() > 0)
            {
                this.leaderboard.lines.Add(new LeaderboardLine(this.gameInfo.playerName, this.gameInfo.playerId == 2 ? FroggerScoreManager.Instance.HighestScore : ShooterScoreManager.Instance.HighestScore));
                this.leaderboard.lines.OrderBy(line => line.score);
                this.RenderLeaderboard();
                this.renderedLeaderboard = true;
            }
        }

        private void RenderLeaderboard()
        {
            Vector3 pos = new Vector3(0, -rowOffset, 0);
            Vector3 nameOffset = new Vector3(this.nameWidth / 2, 0, 0);
            Vector3 pointsOffset = new Vector3(120, 0, 0);

            for (int i = 0; i < this.leaderboard.Count(); i++)
            {
                LeaderboardLine current = this.leaderboard.lines[i];
                Text number = Instantiate(textPrefab);
                number.text = i.ToString() + ".";
                number.transform.SetParent(this.parent, false);
                number.transform.localPosition = pos * i;
                Vector3 cumulativeOffset = new Vector3(number.GetComponent<RectTransform>().rect.width / 2, 0 , 0);

                Text name = Instantiate(textPrefab);
                name.text = current.player;
                name.transform.SetParent(this.parent, false);
                name.transform.localPosition = pos * i + nameOffset + cumulativeOffset;
                cumulativeOffset.x += name.GetComponent<RectTransform>().rect.width / 2;

                Text points = Instantiate(textPrefab);
                points.text = current.score.ToString();
                points.transform.SetParent(this.parent, false);
                points.transform.localPosition = pos * i + pointsOffset + cumulativeOffset;
            }
        }

    }
}
