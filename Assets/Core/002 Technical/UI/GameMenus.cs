// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class GameMenus : MonoBehaviour
    {
        #region Global Members
        [Header("REFERENCES")]

        [SerializeField] private GameMenusAttributes attributes = null;

        [SerializeField] private GameObject darkBackground = null;
        [SerializeField] private RectTransform background = null;
        [SerializeField] private RectTransform title = null;
        [SerializeField] private RectTransform menuRoot = null;
        [SerializeField] private GameObject buttonRoot = null;
        [SerializeField] private Image flash = null;

        [Space(5f)]

        [SerializeField] private TextMeshProUGUI playButton = null;
        [SerializeField] private TextMeshProUGUI restartButton = null;
        #endregion

        #region Buttons
        public void StartGame()
        {
            Close(GameManager.Instance.StartRun);
        }

        public void Restart()
        {
            Close(GameManager.Instance.Restart);
        }

        public void ShowControls()
        {

        }

        public void ShowCredits()
        {

        }

        public void Quit()
        {
            Application.Quit();
        }
        #endregion

        #region Show / Hide
        private int openState = 1;
        private Sequence menuSequence = null;

        // -----------------------

        public void OpenDefeat()
        {
            Open(1);
        }

        public void OpenVictory()
        {
            Open(2);
        }

        private void Open(int _variation)
        {
            Time.timeScale = 0f;
            openState = 0;

            if (menuSequence.IsActive())
                menuSequence.Kill(true);
               
            menuSequence = DOTween.Sequence();
            Sequence _flashSequence = DOTween.Sequence();

            // Intro.
            if (_variation < 1)
            {
                Sequence _introSequence = DOTween.Sequence();
                if (_variation == 0)
                {
                    restartButton.transform.parent.gameObject.SetActive(true);
                    restartButton.text = "Restart";

                    playButton.transform.parent.gameObject.SetActive(true);
                    playButton.text = "Resume";
                }
                else
                {
                    restartButton.transform.parent.gameObject.SetActive(false);

                    buttonRoot.SetActive(false);
                    darkBackground.SetActive(false);

                    _introSequence.AppendInterval(1f);
                    _flashSequence.AppendInterval(1f);
                }

                // Intro sequence.
                {
                    menuRoot.gameObject.SetActive(true);
                    buttonRoot.SetActive(false);

                    // Background.
                    Vector2 _position = background.anchoredPosition;
                    background.anchoredPosition = new Vector2(attributes.OpenBackgroundOriginalPos, _position.y);

                    _introSequence.Append(background.DOAnchorPosX(attributes.OpenBackgroundDestinationPos, attributes.OpenBackgroundMovementDuration)
                                  .SetEase(attributes.OpenBackgroundMovementEase));

                    // Title.
                    _position = title.anchoredPosition;
                    title.anchoredPosition = new Vector2(attributes.OpenTitleOriginalPos, _position.y);

                    _introSequence.Join(title.DOAnchorPosX(attributes.OpenTitleDestinationPos, attributes.OpenTitleMovementDuration)
                                  .SetEase(attributes.OpenTitleMovementEase).SetDelay(attributes.OpenTitleDelay));

                    _introSequence.SetUpdate(true);
                }

                _flashSequence.AppendInterval(attributes.OpenFlashDelay);
                menuSequence.Join(_introSequence);
            }
            else
            {
                playButton.transform.parent.gameObject.SetActive(false);
                restartButton.transform.parent.gameObject.SetActive(true);
                restartButton.text = (_variation == 1)
                                   ? "Retry"
                                   : "Beat That Score";

                menuRoot.gameObject.SetActive(false);
            }

            // Flash
            {
                Vector3 _scale = flash.rectTransform.localScale;

                flash.gameObject.SetActive(true);
                flash.rectTransform.localScale = new Vector3(0f, _scale.y, _scale.z);
                flash.color = Color.white;

                _flashSequence.Append(flash.rectTransform.DOScaleX(1f, attributes.FlashStretchDuration).SetEase(attributes.FlashStretchEase));
                _flashSequence.AppendCallback(() =>
                {
                    darkBackground.SetActive(true);
                    menuRoot.gameObject.SetActive(true);
                    buttonRoot.SetActive(true);
                });

                _flashSequence.Append(flash.DOFade(0f, attributes.FlashFadeDuration).SetEase(attributes.FlashFadeEase));
                _flashSequence.SetUpdate(true);
            }

            menuSequence.Join(_flashSequence);

            menuSequence.SetUpdate(true);
            menuSequence.OnComplete(() =>
            {
                flash.gameObject.SetActive(false);

                openState = 2;
            });

            menuSequence.Play();
        }

        public void Close(Action _callback)
        {
            openState = 0;

            if (menuSequence.IsActive())
                menuSequence.Kill(true);

            menuSequence = DOTween.Sequence();
            Sequence _flashSequence = DOTween.Sequence();
            {
                Vector3 _scale = flash.rectTransform.localScale;

                flash.gameObject.SetActive(true);
                flash.rectTransform.localScale = new Vector3(0f, _scale.y, _scale.z);
                flash.color = Color.white;

                _flashSequence.AppendInterval(attributes.CloseFlashDelay);
                _flashSequence.Append(flash.rectTransform.DOScaleX(1f, attributes.FlashStretchDuration).SetEase(attributes.FlashStretchEase));
                _flashSequence.AppendCallback(() =>
                {
                    menuRoot.localScale = Vector3.one;

                    darkBackground.SetActive(false);
                    menuRoot.gameObject.SetActive(false);

                    _callback?.Invoke();
                });

                _flashSequence.Append(flash.DOFade(0f, attributes.FlashFadeDuration).SetEase(attributes.FlashFadeEase));
                _flashSequence.SetUpdate(true);
            }

            menuSequence.Join(menuRoot.DOScaleY(0f, attributes.CloseDuration).SetEase(attributes.CloseEase));
            menuSequence.Join(_flashSequence);

            menuSequence.SetUpdate(true);
            menuSequence.OnComplete(() =>
            {
                flash.gameObject.SetActive(false);

                openState = 1;
                Time.timeScale = 1f;
            });

            menuSequence.Play();
        }
        #endregion

        #region Mono Behaviour
        private void Start()
        {
            attributes.OpenMenuInput.Enable();
            Open(-1);
        }

        private void Update()
        {
            // Open / Close menus.
            if (GameManager.Instance.IsLive && attributes.OpenMenuInput.triggered)
            {
                switch (openState)
                {
                    // In Transition.
                    case 0:
                        menuSequence.Complete();
                        break;

                    case 1:
                        Open(0);
                        break;

                    case 2:
                        Close(null);
                        break;

                    default:
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            attributes.OpenMenuInput.Disable();
        }
        #endregion
    }
}
