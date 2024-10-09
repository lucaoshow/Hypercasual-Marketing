using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Root.General.Leaderboard;
using System;

namespace Root.General.API
{
    public class MarketingAPI : MonoBehaviour
    {
        [SerializeField] private GameInfo gameInfo;
        private readonly string API_URL = "http://localhost:5000";

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);    
        }

        public void GetLeaderboard(LeaderboardData resultContainer)
        {
            StartCoroutine(this.Get<LeaderboardData>(API_URL + "/scores?game_id=" + this.gameInfo.gameId.ToString(), resultContainer, "lines"));
        }

        public void SendPlayerFormData(string name, string email, string interest, int year, bool authorization)
        {
            string auth = authorization ? "true" : "false";
            this.StartCoroutine(this.Post(API_URL + "/players", $"{{\"name\": \"{name}\",\"email\": \"{email}\",\"interest_area\": \"{interest}\", \"graduation_year\": {year}, \"email_authorization\": {auth}}}", true));
        }

        public void SendPlayerScore(int score)
        {
            this.StartCoroutine(this.Post(API_URL + "/scores", $"{{\"player_id\": {this.gameInfo.playerId},\"game_id\": {this.gameInfo.gameId},\"score\": {score}}}", false));
        }

        private IEnumerator Get<T>(string url, T resultContainer, string jsonArrayKey) where T : IGetResultContainer<T>
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                try
                {
                    string json = webRequest.downloadHandler.text;
                    if (jsonArrayKey != string.Empty)
                    {
                        resultContainer.AddRange(JsonUtility.FromJson<T>($"{{\"{jsonArrayKey}\":" + json + "}"));
                    }
                    else
                    {
                        resultContainer.AddRange(JsonUtility.FromJson<T>(json));
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log("Failed to parse JSON:\n" + ex.ToString());
                }
            }
        }

        private IEnumerator Post(string url, string json, bool playerIdReturned)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, json, "application/json"))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                }
                else if (playerIdReturned)
                {
                    this.gameInfo.playerId = Int32.Parse(webRequest.downloadHandler.text[webRequest.downloadHandler.text.IndexOf("id\":") + "id\":".Length].ToString());
                }
            }
        }

    }
}
