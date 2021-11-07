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
        public void SetWeapons(WeaponsData _data)
        {
            weaponsData = _data;
            weaponRenderer.sprite = _data.WeaponSprite;
            remainingUses = _data.Ammo;
            enabled = false;
            enabled = true;
        }
        #endregion

    }

}