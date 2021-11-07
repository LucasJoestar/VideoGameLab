using UnityEngine;

namespace Shmup
{
    public class PlayerWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private bool useAutoFire = false;
        private bool isAutoFiring = false;

        private float baseFireRate = 0;
        private float baseProjectileSize = 0;
        #endregion

        #region Methods
        protected override void Init()
        {
            base.Init();
            for (int i = 0; i < systems.Length; i++)
            {
                baseFireRate = systems[i].emission.rateOverTimeMultiplier;
                baseProjectileSize = systems[i].main.startSizeMultiplier;
            }
        }

        public override void Fire()
        {
            if (useAutoFire)
            {
                isAutoFiring = !isAutoFiring;
                if (isAutoFiring)
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
            }
            else base.Fire();
        }

        public void IncreaseFireRate(float _multiplier)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                ParticleSystem.EmissionModule _emissionModule = systems[i].emission;
                _emissionModule.rateOverTimeMultiplier = baseFireRate * _multiplier; 
            }
        }

        public void IncreaseProjectileSize(float _multiplier)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                ParticleSystem.MainModule _emissionModule = systems[i].main;
                _emissionModule.startSizeMultiplier = baseProjectileSize * _multiplier;
            }
        }
        #endregion
    }
}
