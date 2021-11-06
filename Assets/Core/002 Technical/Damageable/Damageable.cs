using UnityEngine;

namespace Shmup
{
    public abstract class Damageable : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] protected int health = 1;
        [SerializeField] protected SpriteRenderer sprite = null;
        #endregion

        #region Methods
        protected abstract void OnTakeDamages();

        private void OnParticleCollision(GameObject other)
        {
            OnTakeDamages();
        }
        #endregion
    }
}
