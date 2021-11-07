// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    [CreateAssetMenu(fileName = "Weapons Data", menuName = "Weapons/Data", order = 101)]
    public class WeaponsData : ScriptableObject
    {
        [SerializeField] private PoolableParticle projectiles;
        [SerializeField] private int damages = 1;

        public int Damages => damages;

        public Pool<PoolableParticle> Pool = new Pool<PoolableParticle>(2);

        public PoolableParticle GetInstance()
        {
            var _instance = Pool.GetFromPool(projectiles);
            return _instance;
        }
    }
}
