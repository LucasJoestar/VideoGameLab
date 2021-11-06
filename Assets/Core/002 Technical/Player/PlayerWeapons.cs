using UnityEngine;

namespace Shmup
{
    public class PlayerWeapons : Weapons
    {
        #region Fields and Properties
        [SerializeField] private bool useAutoFire = false;
        private bool isAutoFiring = false;
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
                        systems[i].Play();
                    }
                }
                else
                {
                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                }
            }
            else base.Fire();
        }
        #endregion
    }
}
