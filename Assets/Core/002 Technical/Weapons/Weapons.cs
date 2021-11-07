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
        #endregion

        #region Methods
        public virtual void Fire()
        { 
            for (int _i = 0; _i < systems.Length; _i++)
            {
                systems[_i].MainParticles.Play();
                // systems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting); 
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

                _instance.transform.SetParent(_parent, true);
                _instance.transform.localPosition = Vector3.zero;
                _instance.transform.localRotation = Quaternion.identity;

                systems[_i] = _instance;

                ParticleSystem.CollisionModule _module = systems[_i].MainParticles.collision;
                _module.collidesWith = targetMask;
            }
        }

        protected virtual void OnDisable()
        {
            if (Application.isPlaying)
            {
                for (int _i = 0; _i < systems.Length; _i++)
                {
                    //systems[i].MainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                    var _instance = systems[_i];
                    weaponsData.Pool.SendToPool(_instance);
                }
            }
        }
        #endregion 
    }
}
