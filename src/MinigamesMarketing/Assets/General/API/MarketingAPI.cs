using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Root.General.Leaderboard;

namespace Root.General.API
{
    public class MarketingAPI : MonoBehaviour
    {
        private readonly string API_URL = "http://localhost:5000";
        private static MarketingAPI instance;
        public static MarketingAPI Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject("MarketingAPIContainer")).AddComponent<MarketingAPI>();
                }
                return instance;
            }
        }

        public void GetLeaderboard(LeaderboardData resultContainer)
        {
            StartCoroutine(this.Get<LeaderboardData>(API_URL + "/scores", resultContainer, "lines"));
        }

        public void SendPlayerFormData(string name, string email, string interest, int year, bool authorization)
        {
            string auth = authorization ? "true" : "false";
            this.StartCoroutine(this.Post(API_URL + "/players", $"{{\"name\": \"{name}\",\"email\": \"{email}\",\"interest_area\": \"{interest}\", \"graduation_year\": {year}, \"email_authorization\": {auth}}}"));
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
                catch 
                {
                    Debug.Log("Failed to parse JSON");
                }
            }
        }

        private IEnumerator Post(string url, string json)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, json, "application/json"))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                }
                else
                {
                    Debug.Log("Upload succeeded!");
                }
            }
        }

    }
}
