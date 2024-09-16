using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class EnemySpawnerScript : MonoBehaviour
    {
        public UnityEngine.Vector2 minSpawnPosition = new UnityEngine.Vector2(-1.718f, 3.596f);
        public UnityEngine.Vector2 maxSpawnPosition = new UnityEngine.Vector2(1.69f, 0.8f);
        public int round;
        public List<GameObject> enemyPrefabs;
        public List<GameObject> enemiesInScene;

        public void Update(){
            if(enemiesInScene.Count == 0){
                round++;
                Spawn();
            }
        }
        public void Spawn(){
            if(round % 5 == 0){
                GameObject enemy  = Instantiate(enemyPrefabs[0], new UnityEngine.Vector3(0,3.38f, 0), Quaternion.identity);
                enemiesInScene.Add(enemy);
            }
            for (int i = 0; i < round / 2; i++)
            {
                float x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
                float y = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
                GameObject enemy = Instantiate(enemyPrefabs[1], new UnityEngine.Vector3(x, y, 0), Quaternion.identity);
                enemiesInScene.Add(enemy);
            }
            for (int i = 0; i < round; i++)
            {
                float x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
                float y = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
                GameObject enemy = Instantiate(enemyPrefabs[2], new UnityEngine.Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y), 0), Quaternion.identity);
                enemiesInScene.Add(enemy);
            }
        }
    }
}
