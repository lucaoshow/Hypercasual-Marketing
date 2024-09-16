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
    }
}
