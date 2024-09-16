using UnityEngine;

namespace Root.Frogger.ScenarioBeings
{
    public class ScenarioBeing : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private bool flipDirection;
        [SerializeField] private float moveSpeed;

        private Vector2 moveDir;
        
        public virtual void Start()
        {
            this.spriteRenderer.flipX = this.flipDirection;
            this.moveDir = this.flipDirection ? Vector2.right : Vector2.left;
            this.rigidBody.velocity = this.moveDir * moveSpeed;
        }
    }
}
