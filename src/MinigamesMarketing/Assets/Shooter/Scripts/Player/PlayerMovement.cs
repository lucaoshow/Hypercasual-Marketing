using UnityEngine;

namespace Root.Shooter.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private readonly float LERP_FACTOR = 0.01f;
        private bool touching = false;
        private Vector3 origin;
        private Vector3 target;

        void Update()
        {
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
        }
    }
}
