// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class PlayerWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private bool useAutoFire = false;
        private bool isAutoFiring = false;

        public bool IsAutoFiring => isAutoFiring;
        #endregion

        #region Methods
        public override void Fire()
        {
            if (useAutoFire)
            {
                isAutoFiring = !isAutoFiring;
                if (isAutoFiring)
                {
                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].MainParticles.Play();
                    }
                }
                else
                {
                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].MainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                }
            }
            else
                base.Fire();
        }
        #endregion
    }
}
