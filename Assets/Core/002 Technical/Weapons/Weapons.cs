// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class Weapons : MonoBehaviour
    {
        #region Fields and properties
        [SerializeField] protected WeaponsData weaponsData = null;
        [SerializeField] private LayerMask targetMask = new LayerMask();
        [SerializeField] private Transform[] weaponsAnchor = new Transform[] { };

        protected PoolableParticle[] systems = new PoolableParticle[] { };

        public WeaponsData WeaponsData => weaponsData;
        private bool weaponsAreReady = false;
        #endregion

        #region Methods
        public virtual void Fire()
        {
            if(weaponsAreReady)
            {
                SoundManager.Instance.PlayClipAtPosition(weaponsData.GetRandomClip(), transform.position, weaponsData.VolumeScale);
                for (int _i = 0; _i < systems.Length; _i++)
                {
                    systems[_i].MainParticles.Play(true);
                }
            }
        }

        public virtual void CancelFire()
        {
            if(weaponsAreReady)
            {
                for (int _i = 0; _i < systems.Length; _i++)
                {
                    systems[_i].MainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            systems = new PoolableParticle[weaponsAnchor.Length];
        }

        protected virtual void OnEnable()
        {
            for (int _i = 0; _i < weaponsAnchor.Length; _i++)
            {
                var _instance = weaponsData.GetInstance();
                Transform _parent = weaponsAnchor[_i];
                
                _instance.transform.position = weaponsAnchor[_i].transform.position;
                _instance.transform.rotation = weaponsAnchor[_i].transform.rotation;

                systems[_i] = _instance;

                ParticleSystem.CollisionModule _module = systems[_i].MainParticles.collision;
                _module.collidesWith = targetMask;
            }
            weaponsAreReady = true;
        }

        private void Update()
        {
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].transform.position = weaponsAnchor[i].transform.position;
            }
        }

        protected virtual void OnDisable()
        {
            if (!GameManager.IsQuitting)
            {
                for (int _i = 0; _i < systems.Length; _i++)
                {
                    if ((systems[_i] != null) && systems[_i].MainParticles.isEmitting)
                    {
                        systems[_i].MainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                        systems[_i].SendToPoolOnInvisible = true;
                    }
                }
                weaponsAreReady = false;
            }
        }
        #endregion 
    }
}
