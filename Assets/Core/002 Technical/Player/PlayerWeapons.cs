using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class PlayerWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private bool useAutoFire = false;
        private bool isAutoFiring = false;
        #endregion

        #region Methods
        public override void Fire()
        {
            if (useAutoFire)
            {
                isAutoFiring = !isAutoFiring;
                if (isAutoFiring)
                {
                    AutoFire();
                }
                else
                {
                    if (autoFireSequence.IsActive())
                    {
                        autoFireSequence.Kill();
                    }
                    
                }
            }
            else base.Fire();
        }

        private Sequence autoFireSequence; 
        private void AutoFire()
        {
            base.Fire();
            if(autoFireSequence.IsActive())
            {
                autoFireSequence.Kill();
            }
            autoFireSequence = DOTween.Sequence();
            autoFireSequence.AppendInterval(weaponsData.FireRateTime / 1);
            autoFireSequence.OnComplete(AutoFire);
            autoFireSequence.Play();
        }

        public void IncreaseFireRate(float _multiplier)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                ParticleSystem.EmissionModule _emissionModule = systems[i].MainParticles.emission;
                _emissionModule.rateOverTimeMultiplier = _multiplier; 
            }
        }

        public void IncreaseProjectileSize(float _multiplier)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                ParticleSystem.MainModule _emissionModule = systems[i].MainParticles.main;
                _emissionModule.startSizeMultiplier = _multiplier;
            }
        }
        #endregion
    }
}
