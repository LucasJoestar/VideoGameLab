// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shmup
{
    [CreateAssetMenu(fileName = "PlayerAttributes", menuName = "SHMUP/Player Attributes", order = 250)]
    public class PlayerAttributes : ScriptableObject
    {
        #region Global Members
        [Space(10f), Header("SPEED")]

        [Range(1f, 100f)] public float Speed = 5f;

        [Space(10f), Header("INPUTS")]

        public InputAction MoveInput = new InputAction();

        public InputAction FireMainInput = new InputAction();
        public InputAction FireSecondaryInput = new InputAction();
        public InputAction FireBombInput = new InputAction();
        #endregion
    }
}
