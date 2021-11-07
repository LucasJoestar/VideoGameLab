using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Shmup
{
    public class ChargeWeapon : Weapons
    {
        #region Fields and Properties
        private static readonly float minY = -5.75f;
        [SerializeField] private ChargeData chargeData = null;
        private Sequence chargingSequence = null;

        #endregion

        #region Methods
        public override void Fire()
        {
            if (chargingSequence.IsActive())
                chargingSequence.Kill();
            chargingSequence = DOTween.Sequence();
            chargingSequence.Append(transform.DOShakePosition(chargeData.ShakeDuration, chargeData.ShakeStrength));
            float _origin = transform.position.y;
            float _dist = _origin - minY;
            chargingSequence.Append(transform.DOLocalMoveY(minY, _dist / chargeData.ChargingSpeed));
            chargingSequence.Append(CameraAspectRatio.Instance.transform.DOShakePosition(chargeData.ShakeDuration));
            chargingSequence.Append(transform.DOLocalMoveY(_origin, _dist / chargeData.ReturnSpeed));
            chargingSequence.Play();
        }

        public override void CancelFire()
        {

        }
        #endregion
    }
}