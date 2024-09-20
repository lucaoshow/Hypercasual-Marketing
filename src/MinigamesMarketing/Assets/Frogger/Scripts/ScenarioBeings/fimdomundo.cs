using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class fimdomundo : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // This method is called when another object enters the trigger collider
        private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroys the object that entered the trigger
        Destroy(other.gameObject);
    }
    }
}