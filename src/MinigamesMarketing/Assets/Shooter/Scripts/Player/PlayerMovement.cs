using System.Collections;
using UnityEngine;

namespace Root.Shooter.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private readonly float LERP_FACTOR = 0.01f;
        public float timeSinceLastShot;
        public float fireRate;
        public float shotSpeed;
        private bool touching = false;
        private Vector3 origin;
        private Vector3 target;
        public GameObject shotPrefab;
        public GameObject healthBar;
        public GameObject explosion;
        public GameObject hitSound;
        public int health = 3;
        

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if ( Input.touchCount <= 0 )
            {
                this.touching = false;
            }
            else if ( this.touching )
            {
                Vector3 currentPos = Camera.main.ScreenToWorldPoint( Input.GetTouch( 0 ).position );
                this.target += currentPos - this.origin;
                this.transform.position = Vector3.Lerp( this.transform.position, this.target, LERP_FACTOR);
                this.origin = currentPos;
            }
            else
            {
                this.touching = true;
                this.origin = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
                this.target = this.transform.position;
            }
            if(fireRate < timeSinceLastShot && Input.touchCount > 0)
            {
                timeSinceLastShot = 0;
                Shoot();
            }
        }
        public void Shoot()
        {
            GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce(shot.transform.up * shotSpeed);
            StartCoroutine(DestroyShot(shot));
        }
        IEnumerator DestroyShot(GameObject shot)
        {
            yield return new WaitForSeconds(2);
            Destroy(shot);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "EnemyProjectile")
            {
                health--;
                Instantiate(hitSound, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                switch (health){
                    case 6:
                        healthBar.GetComponent<Animator>().CrossFade("2hp", 0);
                        break;
                    case 3:
                        healthBar.GetComponent<Animator>().CrossFade("1hp", 0);
                        break;
                    case 0:
                        healthBar.GetComponent<Animator>().CrossFade("0hp", 0);
                        GameObject explodeObj = Instantiate(explosion, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                        break;
                }
                
            }
        }
    }
}
