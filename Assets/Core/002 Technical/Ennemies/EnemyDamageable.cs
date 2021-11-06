using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health = 0; 
        [SerializeField] private int score = 100;
        #endregion

        #region Methods
        Sequence sequence = null; 
        protected override void OnTakeDamages(int _damages)
        {
            if(sequence.IsActive())
            {
                sequence.Complete(); 
            }
            sequence = DOTween.Sequence();
            // Blinking
            sequence.Join(sprite.DOFade(0.0f, UniqueBlinkDuration).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));

            health -= _damages; 
            if(health <= 0)
            {
                // Emit explosion vfx
                for (int i = 0; i < disabledComponents.Length; i++)
                {
                    disabledComponents[i].enabled = false;
                }
            }
            sequence.Play();
        }

        private void Awake()
        {
            health = maxHealth; 
        }
        #endregion
    }
}
