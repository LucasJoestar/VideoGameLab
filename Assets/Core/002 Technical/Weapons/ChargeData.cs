using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    [CreateAssetMenu(fileName = "Charge Data", menuName = "SHMUP/Weapons/ChargeData", order = 101)]
    public class ChargeData : ScriptableObject
    {
        public float ShakeDuration = 1.0f;
        public float ShakeStrength = 1.0f;
        public float ChargingSpeed = 1.0f;
        public float ReturnSpeed = 1.0f;
    }
}