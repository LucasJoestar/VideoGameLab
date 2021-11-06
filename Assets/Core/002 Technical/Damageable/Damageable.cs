using UnityEngine;

namespace Shmup
{
    public abstract class Damageable : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] protected SpriteRenderer sprite = null;
        [SerializeField] protected new Collider collider = null;
        [SerializeField] protected MonoBehaviour[] disabledComponents = new MonoBehaviour[] { }; 

        [Header("Feedbacks")]
        [SerializeField, Range(1, 10)] protected int blinkLoopCount = 8;
        [Header("Resources")]
        [SerializeField] protected ParticleSystem explosion = null;
        #endregion

        #region Methods
        protected abstract void OnTakeDamages(int _damages);

        private void OnParticleCollision(GameObject other)
        {
            int _damages = other.GetComponentInParent<Weapons>().WeaponsData.Damages; 
            OnTakeDamages(_damages);
        }
        #endregion
    }
}
