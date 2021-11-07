// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;

namespace Shmup
{
    public class PoolableParticle : MonoBehaviour, IPoolable<PoolableParticle>
    {
        #region Particle System
        public ParticleSystem MainParticles = null;
        private Pool<PoolableParticle> pool = null;
        #endregion

        #region IPoolable
        public void OnCreated(Pool<PoolableParticle> _pool)
        {
            pool = _pool;
        }

        public void OnGetFromPool()
        {
            
        }

        public void OnSendToPool()
        {
            transform.SetParent(null);
        }
        #endregion

        #region Particle
        private void OnParticleSystemStopped()
        {
            pool.SendToPool(this);
        }
        #endregion
    }
}
