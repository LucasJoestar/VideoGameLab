using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class EnemyDamageable : Damageable
    {
        #region Fields and Properties
        [SerializeField] protected int health = 1;
        #endregion

        #region Methods
        protected override void OnTakeDamages(int _damages)
        {
            Sequence _sequence = DOTween.Sequence();
        }
        #endregion
    }
}
