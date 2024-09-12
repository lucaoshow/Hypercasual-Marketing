using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Root.General.API
{
    public class MarketingAPI : MonoBehaviour
    {
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

        private readonly string API_URL = "http://localhost:5000/players";
        public void SendData(string json)
        {
            this.StartCoroutine(this.Post(json));
        }

        private IEnumerator Post(string json)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(API_URL, json, "application/json"))
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
