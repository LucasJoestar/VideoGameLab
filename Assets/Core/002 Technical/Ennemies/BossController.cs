using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class BossController : EnemyController
    {
        #region Fields and Properties
        [SerializeField] private Weapons[] patterns = new Weapons[] { };
        [SerializeField] private int patternIndex = 0;
        #endregion

        #region Methods
        public override void Activate()
        {
            weapons = patterns[patternIndex];
            base.Activate();
            patternIndex = patternIndex++ < patterns.Length -1 ? patternIndex : 0;
        }
        #endregion
    }
}