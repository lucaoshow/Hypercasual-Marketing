using UnityEngine;

namespace Root.Frogger.ScenarioBeings
{
    
    public class BeingSpawner : MonoBehaviour
    {
        [SerializeField] private ScenarioBeing being;
        [SerializeField] private float secondsToSpawn;
        [SerializeField] private bool beingFacingLeft;
        private float timer;
        void Start()
        {
            this.timer = this.secondsToSpawn;
            
        }

        void Update()
        {
            if (this.timer >= this.secondsToSpawn)
            {
                this.timer = 0;
                ScenarioBeing instance = Instantiate(this.being, this.transform.position, Quaternion.identity);
                if (!this.beingFacingLeft)
                {
                    instance.Flip();
                }
            }

            this.timer += Time.deltaTime;
        }
    }
}
