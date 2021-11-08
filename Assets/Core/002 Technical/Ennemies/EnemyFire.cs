// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class EnemyFire : MonoBehaviour
    {
        [SerializeField] private Weapons weapon = null;

        public void Fire()
        {
            weapon.Fire();
        }
    }
}
