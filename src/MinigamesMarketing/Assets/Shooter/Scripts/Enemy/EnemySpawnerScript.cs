using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Root.Shooter.Score;

namespace Root
{
    public class EnemySpawnerScript : MonoBehaviour
    {
        [SerializeField] Text pointsText;
        public Vector2 minSpawnPosition = new Vector2(-1.718f, 3.596f);
        public Vector2 maxSpawnPosition = new Vector2(1.69f, 0.8f);
        public int round;
        public List<GameObject> enemyPrefabs;
        public List<GameObject> enemiesInScene;

        private Vector2 actualMin = new Vector2(-7f, 6f);
        private Vector2 actualMax = new Vector2(6f, 8.75f);
        private List<Vector3> targetPositions = new List<Vector3>();
        public bool spawning;
        public bool canUpdateScore = true;

        public void Update(){
            if(enemiesInScene.Count == 0){
                round++;
                Spawn();
            }

            if (spawning)
            {
                int count = 0;
                for (int i = 0; i < targetPositions.Count; i++)
                {
                    if ((enemiesInScene[i].transform.position - targetPositions[i]).magnitude <= 0.08f) { count++; }
                    enemiesInScene[i].transform.position = Vector3.Lerp(enemiesInScene[i].transform.position, targetPositions[i], 0.01f);
                }

                spawning = !(count == targetPositions.Count);
            }
        }

        public void Spawn() { 
            targetPositions.Clear();
            enemiesInScene.Clear();

            for (int i = 0; i < round / 2; i++)
            {
                this.targetPositions.Add(new Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y), 0));
                float x = Random.Range(actualMin.x, actualMax.x);
                float y = Random.Range(actualMin.y, actualMax.y);
                GameObject enemy = Instantiate(enemyPrefabs[1], new Vector3(x, y, 0), Quaternion.identity);
                enemiesInScene.Add(enemy);
            }
            
            for (int i = 0; i < round; i++)
            {
                this.targetPositions.Add(new Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y), 0));
                float x = Random.Range(actualMin.x, actualMax.x);
                float y = Random.Range(actualMin.y, actualMax.y);
                GameObject enemy = Instantiate(enemyPrefabs[2], new Vector3(x, y, 0), Quaternion.identity);
                enemiesInScene.Add(enemy);
            }

            if (round % 5 == 0)
            {
                Vector3 pos = new Vector3(0, 3.38f, 0);
                this.targetPositions.Add(pos);
                GameObject enemy = Instantiate(enemyPrefabs[0], pos, Quaternion.identity);
                enemiesInScene.Add(enemy);
            }

            spawning = true;
        }

        public void RemoveEnemy(GameObject enemy)
        {
            if (this.canUpdateScore) { this.pointsText.text = "Score: " + ShooterScoreManager.Instance.Score.ToString(); }
            int index = this.enemiesInScene.IndexOf(enemy);
            this.targetPositions.RemoveAt(index);
            this.enemiesInScene.RemoveAt(index);
        }
    }
}
