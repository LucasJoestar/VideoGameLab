// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;
using DG.Tweening; 

namespace Shmup
{
    public class EnemyController : MonoBehaviour
    {
        #region Fields and properties
        [SerializeField] private Weapons weapons = null;
        [SerializeField, Range(.1f, 5.0f)] private float fireInterval = 1.0f;
        [SerializeField] private bool isAutoFiring = true;

        private Sequence sequence = null; 
        #endregion

        #region Methods
        public void Activate()
        {
            if (isAutoFiring)
            {
                weapons.Fire(); 
                if(sequence.IsActive())
                {
                    sequence.Kill(); 
                }
                sequence = DOTween.Sequence();
                sequence.AppendInterval(fireInterval);
                sequence.OnComplete(Activate); 
            }
        }

        private void Start()
        {
            Activate();
        }
        #endregion
    }
}
