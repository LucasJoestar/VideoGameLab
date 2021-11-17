using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class SecondaryWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private SpriteRenderer weaponRenderer = null;
        private int remainingUses = 0;
        public int RemainingUses => remainingUses;
        #endregion

        #region Methods
        public override bool Fire()
        {
            bool _fire = base.Fire(); 
            if (_fire)
            {
                remainingUses--;
                if(remainingUses <= 0)
                {
                    weaponRenderer.sprite = null;
                    weaponsData = null;
                    enabled = false;
                }
            }
            return _fire;
        }

        public void SetWeapons(WeaponsData _data, Sprite _weaponSprite, int _ammo)
        {
            if(weaponsData == _data)
            {
                remainingUses += _ammo;
                return;
            }
            enabled = false;
            weaponsData = _data;
            weaponRenderer.sprite = _weaponSprite;
            remainingUses = _ammo;
            enabled = true;
        }
        #endregion
    }

}