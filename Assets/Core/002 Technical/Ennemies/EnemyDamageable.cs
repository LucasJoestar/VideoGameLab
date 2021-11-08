// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using System;
using UnityEngine;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] private int score = 100;
        #endregion

        #region Methods
        protected override void OnTakeDamages()
        {
            if (!Array.Exists(sprites, s => s.isVisible))
                return;

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
        }
        #endregion
    }
}
