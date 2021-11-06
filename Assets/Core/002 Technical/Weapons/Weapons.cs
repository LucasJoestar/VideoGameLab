using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Weapons : MonoBehaviour
    {
        #region Fields and properties
        [SerializeField] private WeaponsData weaponsData = null;
        [SerializeField] private LayerMask targetMask = new LayerMask();
        [SerializeField] private Transform[] weaponsAnchor = new Transform[] { }; 
        private ParticleSystem[] systems = new ParticleSystem[] { }; 
        #endregion

        #region Methods
        private void Init()
        {
            systems = new ParticleSystem[weaponsAnchor.Length];
            for (int i = 0; i < weaponsAnchor.Length; i++)
            {
                ParticleSystem _system = Instantiate(weaponsData.Projectiles, weaponsAnchor[i]);
            }
        }
        #endregion 
    }
}
