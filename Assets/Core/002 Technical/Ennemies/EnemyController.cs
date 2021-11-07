// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Shmup
{
    public class EnemyController : MonoBehaviour
    {
        public static List<EnemyController> enemies = new List<EnemyController>();
        #region Fields and properties
        [SerializeField] protected Weapons weapons = null;
        [SerializeField] protected EnemyDamageable damageable = null;
        [SerializeField] private bool isAutoFiring = true;

        protected Sequence sequence = null; 
        #endregion

        #region Methods
        public virtual void ApplyBombBehaviour(float _waitingTime, int _bombDamages = 999)
        {
            if (sequence.IsActive())
                sequence.Kill();
            sequence = DOTween.Sequence();
            sequence.AppendInterval(_waitingTime);
            sequence.OnComplete(() => damageable.TakeDamages(_bombDamages));
            sequence.Play();

            weapons.CancelFire();
        }


        public virtual void Activate()
        {
            if (isAutoFiring)
            {
                weapons.Fire(); 
                if(sequence.IsActive())
                {
                    sequence.Kill(); 
                }
                sequence = DOTween.Sequence();
                sequence.AppendInterval(weapons.WeaponsData.FireRateTime);
                sequence.OnComplete(Activate); 
            }
        }

        private void OnEnable()
        {
            enemies.Add(this);
            if (Application.isPlaying)
            {
                Activate();
            }
        }

        private void OnBecameVisible()
        {
            // if(Application.isPlaying)
            // {
            //     Activate();
            // } 
        }

        private void OnDisable()
        {
            enemies.Remove(this);
            if(sequence.IsActive())
            {
                sequence.Kill();
            }
        }
        #endregion
    }
}
