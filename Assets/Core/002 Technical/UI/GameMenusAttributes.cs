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

        [Range(0f, 5f)] public float OpenBackgroundFadeDelay = 2f;
        [Range(0f, 5f)] public float OpenBackgroundFadeDuration = 2f;
        public Ease OpenBackgroundFadeEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float OpenButtonFadeDelay = 2f;
        [Range(0f, 5f)] public float OpenButtonFadeDuration = 2f;
        public Ease OpenButtonFadeEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float OpenGlobalFadeDelay = 2f;
        [Range(0f, 5f)] public float OpenGlobalFadeDuration = 2f;
        public Ease OpenGlobalFadeEase = Ease.Linear;

        [Space(10f), Header("CLOSE")]

        [Range(0f, 2f)] public float CloseDuration = .2f;
        public Ease CloseEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float CloseFadeDelay = 2f;
        [Range(0f, 5f)] public float CloseFadeDuration = 2f;
        public Ease CloseFadeEase = Ease.Linear;

        [Space(10f), Header("SWITCH")]

        public float CreditsPos = 500f;

        [Range(0f, 5f)] public float CreditsShowDuration = .5f;
        public Ease CreditsShowEase = Ease.Linear;

        [Range(0f, 5f)] public float CreditsCloseDuration = .5f;
        public Ease CreditsCloseEase = Ease.Linear;

        [Space(5f)]

        public float ControlsPos = 500f;

        [Range(0f, 5f)] public float ControlsShowDuration = .5f;
        public Ease ControlsShowEase = Ease.Linear;

        [Range(0f, 5f)] public float ControlsCloseDuration = .5f;
        public Ease ControlsCloseEase = Ease.Linear;

        [Space(10f), Header("FLASH")]

        [Range(0f, 2f)] public float FlashDelay = .1f;
        [Range(0f, 2f)] public float FlashStretchDuration = .1f;
        public Ease FlashStretchEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 2f)] public float FlashFadeDuration = .1f;
        public Ease FlashFadeEase = Ease.Linear;

        [Space(10f), Header("SELECTION")]

        public float SelectionOffset = 10f;
        [Range(0f, 10f)] public float SelectionRotationDuration = 2f;
        public Ease SelectionRotationEase = Ease.Linear;

        [Space(10f), Header("INPUTS")]

        public InputAction OpenMenuInput = new InputAction();
        #endregion
    }
}
