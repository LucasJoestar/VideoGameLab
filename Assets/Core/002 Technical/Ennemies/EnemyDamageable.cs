using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] private int health = 1;
        [SerializeField] private int score = 100; 
        #endregion

        #region Methods
        protected override void OnTakeDamages(int _damages)
        {
            Sequence _sequence = DOTween.Sequence();
            // Blinking
            collider.enabled = false;
            _sequence.Join(sprite.DOFade(0.0f, .05f).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));
            _sequence.OnComplete(() => collider.enabled = true);

            health -= _damages; 
            if(health < 0)
            {
                // Emit explosion vfx
                _sequence.OnComplete(() => Destroy(gameObject));
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
