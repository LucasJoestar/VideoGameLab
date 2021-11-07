using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PoolableProjectile : PoolableParticle
    {
        [SerializeField] private WeaponsData weaponsData = null;
        public WeaponsData WeaponsData => weaponsData; 
    }
}