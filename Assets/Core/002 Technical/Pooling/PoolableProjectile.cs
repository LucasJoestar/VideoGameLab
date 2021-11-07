using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PoolableProjectile : PoolableParticle
    {
        [SerializeField] private WeaponsData weaponsData = null;
        public WeaponsData WeaponsData => weaponsData;

        private void OnParticleCollision(GameObject other)
        {
            if(weaponsData.CollidingAudioClip != null)
            {
                SoundManager.Instance.PlayClipAtPosition(weaponsData.CollidingAudioClip, other.transform.position, weaponsData.VolumeScale);
            }
        }
    }
}