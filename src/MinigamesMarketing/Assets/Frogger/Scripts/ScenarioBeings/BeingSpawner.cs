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
            if (!this.beingFacingLeft)
            {
                this.being.Flip();
            }
        }

        void Update()
        {
            if (this.timer >= this.secondsToSpawn)
            {
                this.timer = 0;
                Instantiate(this.being.gameObject, this.transform.position, Quaternion.identity);
            }

            this.timer += Time.deltaTime;
        }
    }
}
