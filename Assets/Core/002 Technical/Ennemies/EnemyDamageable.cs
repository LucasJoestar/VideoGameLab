// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] private int score = 100;
        #endregion

        #region Methods
        protected override void OnDestroyed()
        {
            base.OnDestroyed();

            // Increment score.
        }
        #endregion
    }
}
