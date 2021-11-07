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
        [SerializeField] private new BoxCollider collider = null;
        #endregion

        #region Methods
        private RaycastHit[] hits = new RaycastHit[] { }; 

        public override void Fire()
        {
            base.Fire();
            // get enemies from static array and stop attack and take damages
        }
        #endregion
    }
}
