using UnityEngine;

namespace Shmup
{
    public class DropableWeapon : MonoBehaviour, IPoolable<DropableWeapon>
    {
        [SerializeField] private new SpriteRenderer renderer = null;
        public SpriteRenderer Renderer => renderer;
        public WeaponsData WeaponData { get; set; } = null;
        public Sprite WeaponSprite { get; set; } = null;
        public int Ammo { get; set; } = 10;
        public int Score { get; set; } = 100;

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
        #endregion

        #region Methods
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