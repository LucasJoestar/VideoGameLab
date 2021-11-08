using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{

    public class WeaponDropper : MonoBehaviour
    {
        public static readonly Pool<DropableWeapon> droppableWeapons = new Pool<DropableWeapon>(3);

        #region Fields and Properties
        [SerializeField] private DropableWeapon dropablePrefab = null;
        [SerializeField] private WeaponsData weaponData = null;
        [SerializeField] private Sprite weaponSprite = null;
        [SerializeField] private int ammo = 10;
        [SerializeField] private int score = 100;

        [SerializeField, Range(0.0f, 1.0f)] private float dropProbability = 1.0f;
        #endregion 

        #region Methods
        public void Drop()
        {
            if (Random.value > dropProbability)
            {
                DropableWeapon _droppedWeapon = droppableWeapons.GetFromPool(dropablePrefab);
                _droppedWeapon.WeaponData = weaponData;
                _droppedWeapon.WeaponSprite = weaponSprite;
                _droppedWeapon.Ammo = ammo;
                _droppedWeapon.Score = score;
                _droppedWeapon.transform.position = transform.position;
            }
        }
        #endregion
    }
}
