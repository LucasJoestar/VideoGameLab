// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField] private RectTransform controls = null;
        [SerializeField] private RectTransform credits = null;
        [SerializeField] private TextMeshProUGUI titleText = null;
        [SerializeField] private RectTransform selection= null;

        [Space(5f)]

        [SerializeField] private CanvasGroup darkBackgroundGroup = null;
        [SerializeField] private CanvasGroup globalGroup = null;
        [SerializeField] private CanvasGroup buttonGroup = null;

        [Space(5f)]

        [SerializeField] private TextMeshProUGUI playButton = null;
        [SerializeField] private TextMeshProUGUI restartButton = null;

        [Space(5f)]

        [SerializeField] private Button controlsButton = null;
        [SerializeField] private Button controlsBackButton = null;
        [SerializeField] private Button creditsButton = null;
        [SerializeField] private Button creditsBackButton = null;

        private Sequence menuSequence = null;
        private Sequence buttonSequence = null;
        #endregion

        #region Buttons
        public void StartGame()
        {
            DoButtonSequence();
            buttonSequence.OnComplete(() => Close(1));
        }

        public void Restart()
        {
            DoButtonSequence();
            buttonSequence.OnComplete(() => Close(2));
        }

        public void ShowControls()
        {
            EventSystem.current.SetSelectedGameObject(controlsBackButton.gameObject);

            DoButtonSequence();
            buttonSequence.OnComplete(() =>
            {
                Vector2 _position = controls.anchoredPosition;
                _position.x = attributes.ControlsPos;

                controls.anchoredPosition = _position;
                controls.gameObject.SetActive(true);

                controlsBackButton.interactable = true;

                // Sequence.
                menuSequence = DOTween.Sequence();
                {
                    menuSequence.Join(controls.DOAnchorPosX(0f, attributes.ControlsShowDuration).SetEase(attributes.ControlsShowEase));
                    menuSequence.SetUpdate(true);
                    menuSequence.Play();
                }
            });
        }

        public void CloseControls()
        {
            controlsBackButton.interactable = false;

            DoButtonSequence();
            buttonSequence.OnComplete(() =>
            {
                EventSystem.current.SetSelectedGameObject(controlsButton.gameObject);

                // Sequence.
                menuSequence = DOTween.Sequence();
                {
                    menuSequence.Join(controls.DOAnchorPosX(attributes.ControlsPos, attributes.ControlsCloseDuration).SetEase(attributes.ControlsCloseEase));
                    menuSequence.OnComplete(() =>
                    {
                        controls.gameObject.SetActive(false);
                    });

                    menuSequence.SetUpdate(true);
                    menuSequence.Play();
                }
            });
        }

        public void ShowCredits()
        {
            EventSystem.current.SetSelectedGameObject(creditsBackButton.gameObject);

            DoButtonSequence();
            buttonSequence.OnComplete(() =>
            {
                Vector2 _position = credits.anchoredPosition;
                _position.x = attributes.CreditsPos;

                credits.anchoredPosition = _position;
                credits.gameObject.SetActive(true);

                creditsBackButton.interactable = true;

                // Sequence.
                menuSequence = DOTween.Sequence();
                {
                    menuSequence.Join(credits.DOAnchorPosX(0f, attributes.CreditsShowDuration).SetEase(attributes.CreditsShowEase));
                    menuSequence.SetUpdate(true);
                    menuSequence.Play();
                }
            });
        }

        public void CloseCredits()
        {
            creditsBackButton.interactable = false;

            DoButtonSequence();
            buttonSequence.OnComplete(() =>
            {
                EventSystem.current.SetSelectedGameObject(creditsButton.gameObject);

                // Sequence.
                menuSequence = DOTween.Sequence();
                {
                    menuSequence.Join(credits.DOAnchorPosX(attributes.CreditsPos, attributes.CreditsCloseDuration).SetEase(attributes.CreditsCloseEase));
                    menuSequence.OnComplete(() =>
                    {
                        credits.gameObject.SetActive(false);
                    });

                    menuSequence.SetUpdate(true);
                    menuSequence.Play();
                }
            });
        }

        public void Quit()
        {
            DoButtonSequence();
            buttonSequence.OnComplete(Application.Quit);
        }

        private void DoButtonSequence()
        {
            if (buttonSequence.IsActive())
                buttonSequence.Complete(true);

            if (menuSequence.IsActive())
                menuSequence.Complete(true);

            buttonSequence = DOTween.Sequence();
            buttonSequence.AppendInterval(.15f);
            buttonSequence.SetUpdate(true);
        }
        #endregion

        #region Show / Hide
        private int openState = 1;

        // -----------------------

        public void OpenDefeat() => Open(1);

        public void OpenVictory() => Open(2);

        private void Open(int _variation)
        {
            Time.timeScale = 0f;
            openState = 0;

            if (menuSequence.IsActive())
                menuSequence.Kill(true);
               
            menuSequence = DOTween.Sequence();

            controls.gameObject.SetActive(false);
            credits.gameObject.SetActive(false);

            // Intro.
            if (_variation < 1)
            {
                Sequence _introSequence = DOTween.Sequence();
                Sequence _backgroundSequence = DOTween.Sequence();
                Sequence _buttonSequence = DOTween.Sequence();

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

                    _introSequence.AppendInterval(1f);
                    _backgroundSequence.AppendInterval(1f);
                    _buttonSequence.AppendInterval(1f);
                }

                EventSystem.current.SetSelectedGameObject(playButton.transform.parent.gameObject);

                // Intro sequence.
                {
                    menuRoot.gameObject.SetActive(true);
                    globalGroup.alpha = 1f;

                    // Background.
                    Vector2 _position = background.anchoredPosition;
                    background.anchoredPosition = new Vector2(attributes.OpenBackgroundOriginalPos, _position.y);

                    _introSequence.Append(background.DOAnchorPosX(attributes.OpenBackgroundDestinationPos, attributes.OpenBackgroundMovementDuration)
                                  .SetEase(attributes.OpenBackgroundMovementEase));

                    // Title.
                    title.gameObject.SetActive(true);
                    titleText.gameObject.SetActive(false);

                    _position = title.anchoredPosition;
                    title.anchoredPosition = new Vector2(attributes.OpenTitleOriginalPos, _position.y);

                    _introSequence.Join(title.DOAnchorPosX(attributes.OpenTitleDestinationPos, attributes.OpenTitleMovementDuration)
                                  .SetEase(attributes.OpenTitleMovementEase).SetDelay(attributes.OpenTitleDelay));

                    _introSequence.SetUpdate(true);
                    menuSequence.Join(_introSequence);
                }

                // Background Fade.
                {
                    darkBackground.SetActive(true);
                    darkBackgroundGroup.alpha = 0f;

                    _backgroundSequence.AppendInterval(attributes.OpenBackgroundFadeDelay);
                    _backgroundSequence.Append(darkBackgroundGroup.DOFade(1f, attributes.OpenBackgroundFadeDuration).SetEase(attributes.OpenBackgroundFadeEase));

                    _backgroundSequence.SetUpdate(true);
                    menuSequence.Join(_backgroundSequence);
                }

                // Button Fade.
                {
                    buttonRoot.SetActive(true);
                    buttonGroup.alpha = 0f;

                    _buttonSequence.AppendInterval(attributes.OpenButtonFadeDelay);
                    _buttonSequence.Append(buttonGroup.DOFade(1f, attributes.OpenButtonFadeDuration).SetEase(attributes.OpenButtonFadeEase));

                    _buttonSequence.SetUpdate(true);
                    menuSequence.Join(_buttonSequence);
                }
            }
            else
            {
                title.gameObject.SetActive(false);
                titleText.gameObject.SetActive(true);
                titleText.text = (_variation == 1)
                               ? "You Died Miserably"
                               : "Congratulations!";

                playButton.transform.parent.gameObject.SetActive(false);
                restartButton.transform.parent.gameObject.SetActive(true);
                restartButton.text = (_variation == 1)
                                   ? "Try Again"
                                   : "Beat That Score";

                menuRoot.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(restartButton.transform.parent.gameObject);

                globalGroup.alpha = 0f;
                darkBackgroundGroup.alpha = 0f;
                buttonGroup.alpha = 1f;

                darkBackground.SetActive(true);
                menuRoot.gameObject.SetActive(true);

                menuSequence.AppendInterval(attributes.OpenGlobalFadeDelay);
                menuSequence.Append(globalGroup.DOFade(1f, attributes.OpenGlobalFadeDuration).SetEase(attributes.OpenGlobalFadeEase));
                menuSequence.Join(darkBackgroundGroup.DOFade(1f, attributes.OpenGlobalFadeDuration).SetEase(attributes.OpenGlobalFadeEase));
            }

            menuSequence.SetUpdate(true);
            menuSequence.OnComplete(() =>
            {
                openState = 2;
            });

            menuSequence.Play();
        }

        public void Close(int _variation)
        {
            openState = 0;

            if (menuSequence.IsActive())
                menuSequence.Kill(true);

            menuSequence = DOTween.Sequence();

            // Restart.
            if (_variation == 2)
            {
                Sequence _flashSequence = DOTween.Sequence();
                {
                    Vector3 _scale = flash.rectTransform.localScale;

                    flash.gameObject.SetActive(true);
                    flash.rectTransform.localScale = new Vector3(0f, _scale.y, _scale.z);
                    flash.color = Color.white;

                    _flashSequence.AppendInterval(attributes.FlashDelay);
                    _flashSequence.Append(flash.rectTransform.DOScaleX(1f, attributes.FlashStretchDuration).SetEase(attributes.FlashStretchEase));
                    _flashSequence.AppendCallback(() =>
                    {
                        darkBackground.SetActive(false);
                        menuRoot.gameObject.SetActive(false);

                        switch (_variation)
                        {
                            case 1:
                                GameManager.Instance.StartRun();
                                break;

                            case 2:
                                GameManager.Instance.Restart();
                                break;

                            default:
                                break;
                        }
                    });

                    _flashSequence.Append(flash.DOFade(0f, attributes.FlashFadeDuration).SetEase(attributes.FlashFadeEase));
                    _flashSequence.SetUpdate(true);

                    menuSequence.Join(_flashSequence);
                }
            }
            else
            {
                Sequence _fadeSequence = DOTween.Sequence();
                _fadeSequence.AppendInterval(attributes.CloseFadeDelay);
                _fadeSequence.Append(darkBackgroundGroup.DOFade(0f, attributes.CloseFadeDuration).SetEase(attributes.CloseFadeEase));
                _fadeSequence.AppendCallback(() =>
                {
                    darkBackground.SetActive(false);
                    menuRoot.gameObject.SetActive(false);

                    switch (_variation)
                    {
                        case 1:
                            GameManager.Instance.StartRun();
                            break;

                        case 2:
                            GameManager.Instance.Restart();
                            break;

                        default:
                            break;
                    }
                });

                _fadeSequence.SetUpdate(true);

                menuSequence.Join(_fadeSequence);
            }

            menuSequence.Join(menuRoot.DOScaleY(0f, attributes.CloseDuration).SetEase(attributes.CloseEase));

            menuSequence.SetUpdate(true);
            menuSequence.OnComplete(() =>
            {
                menuRoot.localScale = Vector3.one;
                flash.gameObject.SetActive(false);

                openState = 1;
                Time.timeScale = 1f;
            });

            menuSequence.Play();
        }
        #endregion

        #region Mono Behaviour
        private GameObject lastSelection = null;

        private void Start()
        {
            attributes.OpenMenuInput.Enable();
            Open(-1);

            selection.DORotate(new Vector3(0f, 360f, 0f), attributes.SelectionRotationDuration, RotateMode.LocalAxisAdd)
                     .SetEase(attributes.SelectionRotationEase).SetLoops(-1, LoopType.Restart).SetUpdate(true);
        }

        private void Update()
        {
            // Selection.
            GameObject _selection = EventSystem.current.currentSelectedGameObject;
            if (_selection != null)
            {
                var _rectTransform = _selection.GetComponent<RectTransform>();

                if (selection != lastSelection)
                {
                    lastSelection = _selection;
                    selection.anchorMin = _rectTransform.anchorMin;
                    selection.anchorMax = _rectTransform.anchorMax;

                    selection.SetParent(_rectTransform.parent);
                }


                Vector2 _pos = _rectTransform.anchoredPosition;
                _pos.x -= (_rectTransform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x * .5f) + attributes.SelectionOffset;

                selection.anchoredPosition = _pos;
            }

            // Open / Close menus.
            if (GameManager.Instance.IsLive && attributes.OpenMenuInput.triggered)
            {
                if (menuSequence.IsActive())
                {
                    menuSequence.Complete(true);
                    return;
                }

                switch (openState)
                {
                    // In Transition.
                    case 0:
                        break;

                    case 1:
                        Open(0);
                        break;

                    case 2:
                        Close(0);
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
