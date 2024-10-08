using UnityEngine;

namespace Root.General
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "GameInfo")]
    public class GameInfo : ScriptableObject 
    {
        public int gameId;
        public int playerId;
    }
}
