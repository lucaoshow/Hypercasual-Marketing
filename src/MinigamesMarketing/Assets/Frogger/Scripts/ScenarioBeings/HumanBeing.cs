using UnityEngine;

namespace Root.Frogger.ScenarioBeings
{
    public class HumanBeing : ScenarioBeing
    {
        [SerializeField] private Animator animator;
        [SerializeField] private bool isGirl;
        public override void Start()
        {
            base.Start();
            this.animator.SetBool("isGirl", this.isGirl);
        }
    }
}
