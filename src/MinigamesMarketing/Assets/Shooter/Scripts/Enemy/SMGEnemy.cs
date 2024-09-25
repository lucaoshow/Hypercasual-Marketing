using System.Collections;
using UnityEngine;
using Root.Shooter.Score;

namespace Root
{
    public class SMGEnemy : BaseEnemy
    {
        public UnityEngine.Vector2 minSpawnPosition = new UnityEngine.Vector2(-1.718f, 3.38f);
        public UnityEngine.Vector2 maxSpawnPosition = new UnityEngine.Vector2(1.69f, 3.38f);
        public bool goingRight = true;
        public bool isShooting = false;

        private EnemySpawnerScript enemySpawner;

        new private void Start()
        {
            base.Start();
            this.enemySpawner = gm.GetComponent<EnemySpawnerScript>();
        }
        new public void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= fireRate && !isShooting)
            {
                isShooting = true;
                StartCoroutine(Burst());
                timeSinceLastShot = 0.0f;
            }
            if (!this.enemySpawner.spawning)
            {
                if (goingRight)
                {
                    transform.position = new UnityEngine.Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
                    if (transform.position.x >= maxSpawnPosition.x)
                    {
                        goingRight = false;
                    }
                }else{
                    transform.position = new UnityEngine.Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
                    if (transform.position.x <= minSpawnPosition.x)
                    {
                        goingRight = true;
                    }
                }
            }
        }
        IEnumerator Burst()
        {
            for (int i = 0; i < 3; i++)
            {
                Shoot(UnityEngine.Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            isShooting = false;
        }

        new public IEnumerator TakeHit()
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
                
                this.enemySpawner.RemoveEnemy(gameObject);
                GameObject explosionObj = Instantiate(explosion, new UnityEngine.Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), UnityEngine.Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Destroy(explosionObj);
                for (int i = 0; i < 5; i++)
                {
                    ShooterScoreManager.Instance.IncrementScore();
                }
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
