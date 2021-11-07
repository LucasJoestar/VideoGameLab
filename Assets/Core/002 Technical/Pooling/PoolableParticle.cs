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

        public bool IsInPool = false;
        public bool SendToPoolOnInvisible = false;
        #endregion

        #region IPoolable
        public void OnCreated(Pool<PoolableParticle> _pool)
        {
            pool = _pool;
        }

        public void OnGetFromPool()
        {
            IsInPool = false;
        }

        public void OnSendToPool()
        {
            IsInPool = true;
            SendToPoolOnInvisible = false;
        }
        #endregion

        #region Particle
        private void OnDisable()
        {
            if (!IsInPool)
            {
                pool.SendToPool(this);
            }
        }

        private void OnBecameInvisible()
        {
            if(!IsInPool && SendToPoolOnInvisible)
            {
                pool.SendToPool(this);
            }
        }
        #endregion
    }
}
