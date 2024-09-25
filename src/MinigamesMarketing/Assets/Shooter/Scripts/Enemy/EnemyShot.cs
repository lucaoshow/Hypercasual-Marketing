using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class EnemyShot : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "PlayerShot")
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
