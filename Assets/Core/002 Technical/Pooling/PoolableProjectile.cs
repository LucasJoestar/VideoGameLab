using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PoolableProjectile : PoolableParticle
    {
        [SerializeField] private WeaponsData weaponsData = null;

        private void OnParticleCollision(GameObject other)
        {
            if(weaponsData.CollidingAudioClip != null)
            {
                SoundManager.Instance.PlayClipAtPosition(weaponsData.CollidingAudioClip, other.transform.position, weaponsData.VolumeScale);
            }
            Damageable _target = other.GetComponentInParent<Damageable>(); 
            if(_target != null)
            {
                _target.TakeDamages(weaponsData.Damages); 
            }
        }
    }
}