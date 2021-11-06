using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Shmup
{
    public class PlayerDamageable : Damageable
    {
        #region Fields and properties
        [SerializeField] private bool hasShield = false;
        #endregion

        #region Methods
        protected override void OnTakeDamages(int _damages)
        {
            // Si shield > Destruction shield 
            Sequence _sequence = DOTween.Sequence();
            // Player Blinking
            collider.enabled = false; 
            _sequence.Join(CameraAspectRatio.Instance.Camera.transform.DOShakePosition(0.05f * blinkLoopCount * 2, .15f)) ;
            _sequence.Join(sprite.DOFade(0.0f, .05f).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));
            _sequence.OnComplete(() => collider.enabled = true); 

            if(hasShield)
            {
                hasShield = false;
                // Destroy VFX shield
                return; 
            }
            else // Player is dying here
            {
                // Explosion FX + Destruction + screen shake
                _sequence.OnComplete(() => Destroy(gameObject));
                // Game Over 
                for (int i = 0; i < disabledComponents.Length; i++)
                {
                    disabledComponents[i].enabled = false; 
                }
            }
            _sequence.Play();
        }
        #endregion
    }
}
