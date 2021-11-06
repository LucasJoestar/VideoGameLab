using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PlayerDamageable : Damageable
    {
        #region Fields and properties
        [Header("Resources")]
        [SerializeField] private ParticleSystem explosion = null; 
        #endregion

        #region Methods
        protected override void OnTakeDamages(int _damages)
        {
            // Si shield > Destruction shield 

            // Explosion FX + Destruction + screen shake

            // Game Over 
        }
        #endregion
    }
}
