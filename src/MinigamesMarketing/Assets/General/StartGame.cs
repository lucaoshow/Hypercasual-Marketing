using Root.General.Utils.Scenes;
using UnityEngine;

namespace Root.General
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private GameInfo gameInfo; 
        public void OnStartGameButtonPressed(int gameId)
        {
            this.gameInfo.gameId = gameId;
            SceneHelper.LoadScene(gameId);
        }
    }
}
