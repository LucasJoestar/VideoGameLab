using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class PlayerWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private bool useAutoFire = false;
        private bool isAutoFiring = false;
        public bool IsAutoFiring => isAutoFiring;
        [SerializeField] private float fireRateMultiplier = 1.0f;
        #endregion

        #region Methods
        public override bool Fire()
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
                return false;
            }
            else return base.Fire();
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
            autoFireSequence.AppendInterval(weaponsData.FireRateTime / fireRateMultiplier);
            autoFireSequence.OnComplete(AutoFire);
            autoFireSequence.Play();
        }

        public void IncreaseFireRate(float _multiplier) => fireRateMultiplier = _multiplier;

        public void IncreaseProjectileSize(float _multiplier)
        {
           
        }
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            fireRateMultiplier = 1.0f;
        }
    }
}
