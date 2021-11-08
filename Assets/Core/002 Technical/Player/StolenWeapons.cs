using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class StolenWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private SpriteRenderer weaponRenderer = null;
        private int remainingUses = 0;
        #endregion

        #region Methods
        public void SetWeapons(WeaponsData _data, Sprite _weaponSprite, int _ammo)
        {
            enabled = false;
            weaponsData = _data;
            weaponRenderer.sprite = _weaponSprite;
            remainingUses = _ammo;
            enabled = true;
        }
        #endregion
    }

}