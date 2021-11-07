using UnityEngine;

namespace Shmup
{
    public class DamageableCollider : MonoBehaviour
    {

        [SerializeField] private Damageable damageable = null;

        private void OnParticleCollision(GameObject other)
        {
            int _damages = other.GetComponent<PoolableProjectile>().WeaponsData.Damages;
            damageable.TakeDamages(_damages);
        }
    }
}