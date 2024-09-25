using System.Collections;
using UnityEngine;
using Root.Shooter.Score;

namespace Root
{
    public class ShotgunEnemy : BaseEnemy
    {
        new public void Update()
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

                gm.GetComponent<EnemySpawnerScript>().RemoveEnemy(gameObject);
                GameObject explosionObj = Instantiate(explosion, new UnityEngine.Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), UnityEngine.Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Destroy(explosionObj);
                for (int i = 0; i < 3; i++)
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
