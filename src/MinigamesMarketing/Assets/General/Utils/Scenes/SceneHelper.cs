using UnityEngine.SceneManagement;

namespace Root.General.Utils.Scenes
{
    public class SceneHelper
    {
        public static void LoadScene(string sceneName, bool additive = false)
        {
            SceneManager.LoadScene(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        public static void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}