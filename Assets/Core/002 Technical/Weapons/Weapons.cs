using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Weapons : MonoBehaviour
    {
        #region Fields and properties
        [SerializeField] protected WeaponsData weaponsData = null;
        [SerializeField] private LayerMask targetMask = new LayerMask();
        [SerializeField] private Transform[] weaponsAnchor = new Transform[] { };
        protected ParticleSystem[] systems = new ParticleSystem[] { };
        #endregion

        #region Methods
        private void Init()
        {
            systems = new ParticleSystem[weaponsAnchor.Length];
            for (int i = 0; i < weaponsAnchor.Length; i++)
            {
                systems[i] = Instantiate(weaponsData.Projectiles, weaponsAnchor[i]) ;
                ParticleSystem.CollisionModule _module = systems[i].collision;
                _module.collidesWith = targetMask;
            }
        }

        public virtual void Fire()
        { 
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].Play();
            }
        }

        private void Awake()
        {
            Init();
        }
        #endregion 
    }
}
