using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class AutoExplodeExplosion : MonoBehaviour
    {
        public Animator anim;
        public void Start()
        {
            anim = GetComponent<Animator>();
        }
        public void Update()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject);
            }
        }
    }
}
