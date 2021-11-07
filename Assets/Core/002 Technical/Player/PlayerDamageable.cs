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

            bool _isDead = base.TakeDamages(_damages);
            collider.enabled = false;

            if (!sequence.IsActive())
                sequence = DOTween.Sequence();

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

            // Blinking
            if (!sequence.IsActive())
            {
                sequence = DOTween.Sequence();
                foreach (var _s in sprites)
                {
                    sequence.Join(_s.DOFade(0f, UniqueBlinkDuration).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));
                }
            }

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

        #region Reset
        [SerializeField] private PlayerController controller = null;
        [SerializeField, Range(0f, 5f)] private float introDuration = 1f;
        private Sequence introSequence = null;

        // -----------------------

        public void ResetPlayer()
        {
            controller.enabled = false;
            collider.enabled = false;

            gameObject.SetActive(true);
            transform.localPosition = Vector3.zero;

            if (introSequence.IsActive())
                introSequence.Complete(true);

            introSequence = DOTween.Sequence();
            introSequence.AppendInterval(introDuration);
            introSequence.AppendCallback(() =>
            {
                collider.enabled = true;
                controller.enabled = true;
            });
        }
        #endregion
    }
}
