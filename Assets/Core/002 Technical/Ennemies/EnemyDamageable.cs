// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] private int score = 100;
        [SerializeField] private WeaponDropper dropper = null;
        #endregion

        #region Methods
        protected override void OnTakeDamages()
        {
            base.OnTakeDamages();

            // Blinking
            if (!sequence.IsActive())
            {
                sequence = DOTween.Sequence();
                foreach (var _s in sprites)
                {
                    sequence.Join(_s.material.DOFade(1f, blinkDuration).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));
                }
            }
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();

            // Increment score.
            ScoreManager.Instance.IncreaseScore(score);
            dropper.Drop();
        }
        #endregion
    }
}
