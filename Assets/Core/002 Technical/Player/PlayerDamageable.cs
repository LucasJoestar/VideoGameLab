// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;
using DG.Tweening; 

namespace Shmup
{
    public class PlayerDamageable : Damageable
    {
        #region Damages
        [SerializeField] protected new Collider collider = null;
        private bool hasShield = false;

        // -----------------------

        public override bool TakeDamages(int _damages)
        {
            // No damage when having a shield.
            if (hasShield)
            {
                _damages = 0;
                hasShield = false;
            }

            bool _isDead = TakeDamages(_damages);
            collider.enabled = false;

            sequence.Join(CameraAspectRatio.Instance.Camera.transform.DOShakePosition(0.05f * blinkLoopCount * 2, .15f));
            sequence.AppendCallback(() =>
            {
                collider.enabled = true;
            });

            return _isDead;
        }

        protected override void OnTakeDamages()
        {
            base.OnTakeDamages();

            // Destroy shield.
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            GameManager.Instance.Defeat();
        }

        public void ActivateShield()
        {
            if (!hasShield)
            {
                hasShield = true;

                // Instantiate shield.
            }
        }
        #endregion
    }
}
