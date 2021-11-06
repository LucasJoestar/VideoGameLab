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
    [CreateAssetMenu(fileName = "GameMenusAttributes", menuName = "SHMUP/Game Menus Attributes", order = 250)]
    public class GameMenusAttributes : ScriptableObject
    {
        #region Global Members
        [Header("OPEN")]

        public float OpenBackgroundOriginalPos = -500f;
        public float OpenBackgroundDestinationPos = 0f;

        [Space(5f)]

        [Range(0f, 2f)] public float OpenBackgroundMovementDuration = .2f;
        public Ease OpenBackgroundMovementEase = Ease.Linear;

        [Space(5f)]
        public float OpenTitleOriginalPos = 500f;
        public float OpenTitleDestinationPos = 0f;

        [Space(5f)]

        [Range(0f, 5f)] public float OpenTitleDelay = 1f;
        [Range(0f, 2f)] public float OpenTitleMovementDuration = .2f;
        public Ease OpenTitleMovementEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float OpenFlashDelay = 2f;

        [Space(10f), Header("CLOSE")]

        [Range(0f, 2f)] public float CloseDuration = .2f;
        public Ease CloseEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 2f)] public float CloseFlashDelay = .2f;

        [Space(10f), Header("FLASH")]

        [Range(0f, 2f)] public float FlashStretchDuration = .1f;
        public Ease FlashStretchEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 2f)] public float FlashFadeDuration = .1f;
        public Ease FlashFadeEase = Ease.Linear;

        [Space(10f), Header("INPUTS")]

        public InputAction OpenMenuInput = new InputAction();
        #endregion
    }
}
