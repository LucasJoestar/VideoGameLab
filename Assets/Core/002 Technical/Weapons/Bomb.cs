// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class Bomb : Weapons
    {
        #region Fields and properties
        [SerializeField, Range(.01f, 1.0f)] private float waitingDelay = 0.05f;
        #endregion

        #region Methods
        public override void Fire()
        {
            for (int i = 0; i < EnemyController.enemies.Count; i++)
            {
                EnemyController.enemies[i].ApplyBombBehaviour(waitingDelay * i, weaponsData.Damages);
            }
        }
        #endregion
    }
}
