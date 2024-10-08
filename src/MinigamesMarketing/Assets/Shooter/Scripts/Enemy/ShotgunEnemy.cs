using System.Collections;
using UnityEngine;
using Root.Shooter.Score;

namespace Root
{
    public class ShotgunEnemy : BaseEnemy
    {
        public override void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= fireRate)
            {
                Shoot(Quaternion.identity);
                Shoot(Quaternion.Euler(0, 0, 20));
                Shoot(Quaternion.Euler(0, 0, -20));
                timeSinceLastShot = 0.0f;
            }
        }

        public override IEnumerator TakeHit()
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

                ShooterScoreManager.Instance.IncrementScore(3);
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
