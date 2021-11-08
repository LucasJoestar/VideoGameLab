// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class ScoreGauge : MonoBehaviour
    {
        #region Fields and Properties
        [Header("REFERENCES")]

        [SerializeField] private ScoreAttributes attributes = null;

        [SerializeField] private RectTransform root = null;
        [SerializeField] private Image background = null;
        [SerializeField] private Image fill = null;
        [SerializeField] private Image feedback = null;

        [SerializeField] private RectTransform[] scrolls = new RectTransform[] { };
        [SerializeField] private Image[] scrollImages = new Image[] { };

        [Space(5)]

        [SerializeField, Range(1f, 100f)] public float scrollDuration = 10f;
        #endregion

        #region Score
        private Color backgroundColor = new Color();
        private Color fillColor = new Color();

        private Sequence sequence = null;
        private Sequence shakeSequence = null;
        private Sequence completeSequence = null;

        // -----------------------

        public bool UpdateScoreUI(float _fill, Color _color)
        {
            if (completeSequence.IsActive())
                return false;

            if (sequence.IsActive())
                sequence.Kill();

            fill.color = _color;
            scrollImages[1].color = _color;

            sequence = DOTween.Sequence();
            sequence.Join(fill.DOFillAmount(_fill, attributes.GaugeFillDuration).SetEase(attributes.GaugeFillEase));
            sequence.Join(feedback.DOFillAmount(_fill, attributes.GaugeIncreaseDuration).SetEase(attributes.GaugeIncreaseEase));

            if (_fill == 1f)
            {
                background.DOKill();
                scrollImages[0].DOKill();

                sequence.AppendCallback(() =>
                {
                    if (shakeSequence.IsActive())
                        shakeSequence.Complete();

                    shakeSequence = DOTween.Sequence();
                    shakeSequence.Join(attributes.filledShake.Shake(root));

                    // Fill complete.
                    background.color = fill.color;
                    scrollImages[0].color = fill.color;

                    fill.fillAmount = 0f;
                    feedback.fillAmount = 0f;

                    completeSequence = DOTween.Sequence();
                    completeSequence.Join(fill.DOFillAmount(1f, attributes.GaugeCompleteFillDuration).SetEase(attributes.GaugeCompleteFillEase));
                    completeSequence.Join(feedback.DOFillAmount(1f, attributes.GaugeCompleteDuration).SetEase(attributes.GaugeCompleteEase));

                    completeSequence.OnComplete(() =>
                    {
                        fill.fillAmount = 0f;
                        feedback.fillAmount = 0f;
                    });
                });
            }
            else
            {
                Sequence _wait = DOTween.Sequence();
                _wait.AppendInterval(attributes.GaugeIncreaseDuration);
                _wait.AppendCallback(() =>
                {
                    if (shakeSequence.IsActive())
                        shakeSequence.Complete();

                    shakeSequence = DOTween.Sequence();
                    shakeSequence.Join(attributes.IncreaseShake.Shake(root));
                });

                sequence.Join(_wait);
            }

            return true;
        }

        public void ResetCombo()
        {
            background.DOColor(Color.black, attributes.GaugUncolorDuration).SetEase(attributes.GaugeUncolorEase);
            feedback.DOColor(Color.white, attributes.GaugUncolorDuration).SetEase(attributes.GaugeUncolorEase);

            scrollImages[0].DOColor(Color.black, attributes.GaugUncolorDuration).SetEase(attributes.GaugeUncolorEase);

            if (sequence.IsActive())
                sequence.Kill();

            sequence = DOTween.Sequence();
            sequence.Join(fill.DOFillAmount(0f, attributes.GaugeUnfillDuration).SetEase(attributes.GaugeUnfillEase));
            sequence.Join(feedback.DOFillAmount(0f, attributes.GaugeDecreaseDuration).SetEase(attributes.GaugeDecreaseEase));
        }

        public void ResetGauge()
        {
            if (sequence.IsActive())
                sequence.Complete();

            background.color = backgroundColor;
            fill.color = fillColor;

            scrollImages[0].color = backgroundColor;
            scrollImages[1].color = fillColor;

            fill.fillAmount = 0f;
            feedback.fillAmount = 0f;
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            backgroundColor = background.color;
            fillColor = fill.color;

            fill.fillAmount = 0f;
            feedback.fillAmount = 0f;

            if (scrolls.Length > 0)
            {
                Sequence _sequence = DOTween.Sequence();
                foreach (var _scroll in scrolls)
                {
                    _sequence.Join(_scroll.DOAnchorMin(new Vector2(0f, 1f), scrollDuration).SetEase(Ease.Linear));
                    _sequence.Join(_scroll.DOAnchorMax(new Vector2(0f, 1f), scrollDuration).SetEase(Ease.Linear));
                    _sequence.Join(_scroll.DOPivot(new Vector2(0f, 1f), scrollDuration).SetEase(Ease.Linear));
                }

                _sequence.SetLoops(-1, LoopType.Restart);
            }
        }
        #endregion
    }
}
