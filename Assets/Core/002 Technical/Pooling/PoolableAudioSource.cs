using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PoolableAudioSource : MonoBehaviour, IPoolable<PoolableAudioSource>
    {
        #region Fields and Properties
        public AudioSource MainSource = null;
        private Pool<PoolableAudioSource> pool = null;

        public bool IsInPool = false;
        #endregion

        #region Methods
        #region IPoolable
        public void OnCreated(Pool<PoolableAudioSource> _pool)
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
        }
        #endregion

        #region AudioSource
        private void OnDisable()
        {
            if (!IsInPool)
            {
                pool.SendToPool(this);
            }
        }

        private void Update()
        {
            if (!IsInPool && !MainSource.isPlaying)
                gameObject.SetActive(false);
        }
        #endregion
        #endregion
    }
}