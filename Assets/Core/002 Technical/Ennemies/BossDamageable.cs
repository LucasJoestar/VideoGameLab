// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;
using UnityEngine.Events;

namespace Shmup
{
    public class BossDamageable : EnemyDamageable
    {
        #region Global Members
        [SerializeField] private UnityEvent OnSpawn = new UnityEvent();
        [SerializeField] private UnityEvent OnDestroy = new UnityEvent();
        #endregion

        #region Behaviour
        protected override void OnEnable()
        {
            base.OnEnable();
            OnSpawn.Invoke();
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            OnDestroy.Invoke();
        }
        #endregion
    }
}
