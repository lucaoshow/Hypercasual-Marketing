using UnityEngine;

namespace Root.Frogger.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 lastDeltaPos;

        void Update()
        {
            if ( Input.touchCount > 0 )
            {
                Touch touch = Input.GetTouch( 0 );
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        this.lastDeltaPos = touch.deltaPosition;
                        break;

                    case TouchPhase.Ended:
                        Vector2 swipeDir = this.GetSwipeDirection( this.lastDeltaPos );
                        this.transform.position += new Vector3( swipeDir.x, swipeDir.y, 0 );
                        this.lastDeltaPos = Vector2.zero;
                        break;

                    default:
                        break;
                }
            }

        }

        private Vector2 GetSwipeDirection(Vector2 delta)
        {
            Vector2[] directions = { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
            Vector2 swipeDir = Vector2.zero;
            float minDotProduct = 1f;
            foreach (Vector2 dir in directions)
            {
                float dotProduct = -Vector2.Dot(delta.normalized, dir);
                if (dotProduct < minDotProduct)
                {
                    swipeDir = dir;
                    minDotProduct = dotProduct;
                }
            }

            return swipeDir;
        }
    }
}
