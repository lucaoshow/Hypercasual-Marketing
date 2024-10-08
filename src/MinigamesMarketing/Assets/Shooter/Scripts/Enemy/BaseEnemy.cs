using System.Collections;
using Root.Shooter.Score;
using UnityEngine;

namespace Root
{
    public class BaseEnemy : MonoBehaviour
    {
        public float speed = 1.0f;
        public float health = 100.0f;
        public float fireRate = 1.0f;
        public float timeSinceLastShot = 0.0f;
        public float shotSpeed;
        public int scoreValue;
        public GameObject enemyShot;
        public GameObject gm;
        public GameObject explosion;
        public Animator anim;
        public string hitAnimationName;
        public string idleAnimationName;
        public bool canShoot = true;

        public virtual void Start()
        {
            anim = GetComponent<Animator>();
            gm = GameObject.Find("GM");
            timeSinceLastShot = Random.Range(0.0f, fireRate);
        }


        public virtual void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= fireRate)
            {
                Shoot(Quaternion.identity);
                timeSinceLastShot = 0.0f;
            }
        }

        public void Shoot(Quaternion rotation)
        {
            GameObject shot = Instantiate(enemyShot, transform.position, rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shot.transform.up * shotSpeed);
            StartCoroutine(DestroyShot(shot));
        }
        
        IEnumerator DestroyShot(GameObject shot)
        {
            yield return new WaitForSeconds(3);
            Destroy(shot);
        }
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "PlayerShot")
            {
                Destroy(collision.gameObject);
                StartCoroutine(TakeHit());
            }
        }

        public virtual IEnumerator TakeHit()
        {
            health -= 1;
            canShoot = false;
            anim.CrossFade(hitAnimationName, 0);
            for (int i = 0; i < 60; i++)
            {
                yield return null;
            }

            if (health <= 0)
            {
                ShooterScoreManager.Instance.IncrementScore();
                gm.GetComponent<EnemySpawnerScript>().RemoveEnemy(gameObject);
                Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                canShoot = true;
                anim.CrossFade(idleAnimationName, 0);
            }
        }
    }
}
