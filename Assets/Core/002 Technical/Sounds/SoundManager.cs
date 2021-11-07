using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class SoundManager : MonoBehaviour
    {
        #region Fields and Properties
        public static SoundManager Instance = null;
        private static readonly Pool<PoolableAudioSource> pool = new Pool<PoolableAudioSource>(2);
        [SerializeField] private PoolableAudioSource baseSource = null;
        #endregion

        #region Methods
        public void PlayClipAtPosition(AudioClip _clip, Vector3 _position, float _volumeScale = 1.0f)
        {
            if (_clip == null) return;
            PoolableAudioSource _source = pool.GetFromPool(baseSource);
            _source.transform.position = _position;
            _source.MainSource.PlayOneShot(_clip, _volumeScale);
        }

        // ---------- 
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else Destroy(this);
        }
        #endregion
    }
}