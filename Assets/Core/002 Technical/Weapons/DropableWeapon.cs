using UnityEngine;

namespace Shmup
{
    public class DropableWeapon : MonoBehaviour , IPoolable<DropableWeapon>
    {
        public WeaponsData WeaponData = null;
        public Sprite WeaponSprite = null;
        public int Ammo = 10;
        public int Score = 100;

        #region Pool
        private Pool<DropableWeapon> pool = null;
        public bool IsInPool = false;
        public void OnCreated(Pool<DropableWeapon> _pool)
        {
            pool = _pool;
        }

        public void OnGetFromPool()
        {
             IsInPool = false;
        }

        public void OnSendToPool()
        {
            IsInPool = true;
        }

        private void OnDisable()
        {
            if (!IsInPool)
            {
                WeaponData = null;
                WeaponSprite = null;
                Ammo = 10;
                Score = 100;
                pool.SendToPool(this);
            }
        }
        #endregion
    }
}