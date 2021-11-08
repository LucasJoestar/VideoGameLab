// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    [CreateAssetMenu(fileName = "Weapons Data", menuName = "SHMUP/Weapons/Data", order = 101)]
    public class WeaponsData : ScriptableObject
    {
        [SerializeField] private PoolableParticle projectiles;
        [SerializeField] private int damages = 1;
        [SerializeField,Range(.1f, 20)] private float fireRate = 1.0f;

        [SerializeField] private AudioClip[] fireClips = new AudioClip[] { };
        [SerializeField] private AudioClip collidingAudioClip = null;
        [SerializeField, Range(.1f, 1.0f)] private float volumeScale = 1.0f;
        public AudioClip CollidingAudioClip => collidingAudioClip;
        public float VolumeScale => volumeScale;

        public int Damages => damages;
        public float FireRateTime { get { return 1 / fireRate; }}
        
        public Pool<PoolableParticle> Pool = new Pool<PoolableParticle>(2);

        public PoolableParticle GetInstance()
        {
            var _instance = Pool.GetFromPool(projectiles);
            return _instance;
        }

        int lastClipIndex = -1;
        public AudioClip GetRandomClip()
        {
            if (fireClips.Length == 0) return null;

            if (fireClips.Length == 1)
                return fireClips[0];

            int _index = Random.Range(0, fireClips.Length - 1);
            if(_index == lastClipIndex)
                _index = Random.Range(0, fireClips.Length - 1);
            lastClipIndex = _index;
            return fireClips[_index];
        }
    }
}
