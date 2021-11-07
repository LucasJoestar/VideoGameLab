// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Shmup
{
    public class Bomb : Weapons
    {
        #region Fields and properties
        [SerializeField, Range(.01f, 1.0f)] private float waitingDelay = 0.05f;
        [SerializeField] private Image flashImage = null;
        [SerializeField] private AnimationCurve flashCurve = new AnimationCurve();
        [SerializeField] private float flashDuration = 1.0f;
        private Sequence flashSequence = null;
        #endregion

        #region Methods
        public override void Fire()
        {
            if(flashSequence.IsActive())
            {
                flashSequence.Kill();
            }
            flashSequence = DOTween.Sequence();
            flashSequence.Join(flashImage.DOFade(1.0f, flashDuration).SetEase(flashCurve));
            flashSequence.Play();

            for (int i = 0; i < EnemyController.enemies.Count; i++)
            {
                EnemyController.enemies[i].ApplyBombBehaviour(waitingDelay * i, weaponsData.Damages);
            }
        }
        #endregion
    }
}
