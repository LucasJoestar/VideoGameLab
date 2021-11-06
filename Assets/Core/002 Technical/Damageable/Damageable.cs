using UnityEngine;

namespace Shmup
{
    public abstract class Damageable : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] protected SpriteRenderer sprite = null;
        #endregion

        #region Methods
        protected abstract void OnTakeDamages(int _damages);

        private void OnParticleCollision(GameObject other)
        {

            OnTakeDamages(0);
        }
        #endregion
    }
}
