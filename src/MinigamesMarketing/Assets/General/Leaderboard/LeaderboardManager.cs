using UnityEngine;
using UnityEngine.UI;
using Root.General.API;

namespace Root.General.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private MarketingAPI marketingApi;
        [SerializeField] private Transform parent;
        [SerializeField] private Text textPrefab;
        [SerializeField] private int rowOffset;
        [SerializeField] private int nameWidth;
        private LeaderboardData leaderboard = new LeaderboardData();
        private bool renderedLeaderboard = false;

        void Awake()
        {
            this.marketingApi.GetLeaderboard(this.leaderboard);
        }

        void Update()
        {
            if (!this.renderedLeaderboard && this.leaderboard.Count() > 0)
            {
                this.RenderLeaderboard();
                this.renderedLeaderboard = true;
            }
        }

        private void RenderLeaderboard()
        {
            Vector3 pos = new Vector3(0, -rowOffset, 0);
            Vector3 nameOffset = new Vector3(this.nameWidth / 2, 0, 0);
            Vector3 pointsOffset = new Vector3(250, 0, 0);

            for (int i = 0; i < this.leaderboard.Count(); i++)
            {
                LeaderboardLine current = this.leaderboard.lines[i];
                Text number = Instantiate(textPrefab);
                number.transform.parent = this.parent;
                number.transform.localPosition = pos * i;
                Vector3 cumulativeOffset = new Vector3(number.GetComponent<RectTransform>().rect.width / 2, 0 , 0);
                number.text = i.ToString() + ".";

                Text name = Instantiate(textPrefab);
                name.transform.parent = this.parent;
                name.transform.localPosition = pos * i + nameOffset + cumulativeOffset;
                Rect nameRect = name.GetComponent<RectTransform>().rect;
                nameRect.Set(nameRect.x, nameRect.y, 800, nameRect.height);
                cumulativeOffset.x += nameRect.width / 2;
                name.fontSize = 54;
                name.text = current.player;

                Text points = Instantiate(textPrefab);
                points.transform.parent = this.parent;
                points.transform.localPosition = pos * i + pointsOffset + cumulativeOffset;
                Rect pointsRect = number.GetComponent<RectTransform>().rect;
                pointsRect.Set(pointsRect.x, pointsRect.y, 500, pointsRect.height);
                points.text = current.score.ToString();
            }
        }

    }
}
