using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Weapons : MonoBehaviour
    {
        #region Fields and properties
        [SerializeField] private WeaponsData weaponsData = null;
        [SerializeField] private LayerMask targetMask = new LayerMask();
        [SerializeField] private Transform[] weaponsAnchor = new Transform[] { };
        [SerializeField] private bool useAutoFire = false;
        private ParticleSystem[] systems = new ParticleSystem[] { };
        private bool isAutoFiring = false; 
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

        public void Fire()
        {
            if(useAutoFire)
            {
                isAutoFiring = !isAutoFiring;
                if(isAutoFiring)
                {
                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].Play();
                    }                    
                }
                else
                {
                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                }
                return;
            }

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
