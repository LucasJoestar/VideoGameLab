// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Shmup
{
    public enum PlayerUpgradeType
    {
        Shield,
        IncreaseFireRate,
        IncreaseProjectileSize
    }

    [Serializable]
    public class PlayerUpgrade
    {
        [SerializeField] private Color fillColor = new Color();
        [SerializeField] private int scoreThreshold = 0;
        [SerializeField] private PlayerUpgradeType upgrade = PlayerUpgradeType.Shield;
        [SerializeField] private float increasingValue = 1.0f;

        public Color FillColor => fillColor;
        public int ScoreThreshold => scoreThreshold;
        public PlayerUpgradeType Upgrade => upgrade;
        public float IncreasingValue => increasingValue;
    }

    public class ScoreManager : MonoBehaviour
    {
        #region Global Members
        public static ScoreManager Instance = null;

        [Header("REFERENCES")]

        [SerializeField] private ScoreAttributes attributes = null;
        [SerializeField] private PlayerController player = null;
        [SerializeField] private ScoreGauge gauge = null;


        [Space(5f)]

        [SerializeField] private TextMeshProUGUI multiplier = null;
        [SerializeField] private RectTransform scoreRoot = null;
        [SerializeField] private TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[] { };

        [Space(10f), Header("SETTINGS")]

        [SerializeField] private PlayerUpgrade[] upgrades = new PlayerUpgrade[] { };
        [SerializeField, Range(100, 10000)] private int multiplierTreshold = 500;

        private long score = 0;
        private long lastScore = 0;
        private float multiplierValue = 1f;

        private long scoreMultiplierHelper = 0;
        private long scoreGaugeHelper = 0;

        private int currentUpdateIndex = 0;
        #endregion

        #region Score
        private Sequence scoreSequence = null;
        private Sequence multiplierSequence = null;

        // -----------------------

        public void IncreaseScore(int _increasingValue)
        {
            score = Math.Min(9999999999, score + _increasingValue);
            long _difference = score - scoreMultiplierHelper;
            if (_difference >= multiplierTreshold)
            {
                float _coef = multiplierValue + (Mathf.Floor((float)_difference / multiplierTreshold) * .1f);

                scoreMultiplierHelper += _difference - (_difference % multiplierTreshold);
                IncreaseMultiplier(_coef);
            }

            // Gauge update.
            if (currentUpdateIndex < upgrades.Length)
            {
                scoreGaugeHelper += _increasingValue;

                var _upgrade = upgrades[currentUpdateIndex];
                float _ratio = Mathf.Min(1f, (float)scoreGaugeHelper / _upgrade.ScoreThreshold);

                if (scoreGaugeHelper >= _upgrade.ScoreThreshold)
                {
                    player.UpgradePlayer(_upgrade.Upgrade, _upgrade.IncreasingValue);

                    if (gauge.UpdateScoreUI(_ratio, _upgrade.FillColor))
                    {
                        scoreGaugeHelper -= _upgrade.ScoreThreshold;
                        currentUpdateIndex++;
                    }
                }
                else
                {
                    gauge.UpdateScoreUI(_ratio, _upgrade.FillColor);
                }
            }
        }

        public void UpdateScore()
        {
            string _previousText = lastScore.ToString("0000000000");

            lastScore = Math.Min(score, lastScore + 10);
            scoreSequence = DOTween.Sequence();

            string _scoreText = lastScore.ToString("0000000000");

            int _count;

            for (_count = _scoreText.Length; _count-- > 0;)
            {
                if ((_count < _scoreText.Length - 1) && (_scoreText[_count] == _previousText[_count]))
                    break;

                var _text = scoreTexts[_count];
                scoreSequence.Join(_text.rectTransform.DOAnchorPosY(attributes.ScoreMovement, attributes.ScoreMovementDuration).SetEase(attributes.ScoreMovementEase));
            }

            scoreSequence.AppendCallback(() =>
            {
                for (int _i = scoreTexts.Length; _i-- > _count;)
                {
                    var _text = scoreTexts[_i];
                    _text.text = _scoreText[_i].ToString();
                }
            });

            // Final feedback.
            if (lastScore == score)
            {
                Sequence _flash = DOTween.Sequence();
                {
                    _flash.AppendCallback(() =>
                    {
                        foreach (var _text in scoreTexts)
                        {
                            _text.colorGradientPreset = attributes.FeedbackGradient;
                        }
                    });

                    _flash.AppendInterval(attributes.ScoreFlashDuration);
                    _flash.AppendCallback(() =>
                    {
                        foreach (var _text in scoreTexts)
                        {
                            _text.colorGradientPreset = attributes.ScoreGradient;
                        }
                    });
                }

                scoreSequence.Append(_flash);
                scoreSequence.Join(attributes.ScoreShake.Shake(scoreRoot));
            }
        }

        public void IncreaseMultiplier(float _value)
        {
            multiplierValue = _value;
            if (multiplierSequence.IsActive())
                return;

            float _coef = 1f + ((_value - 1f) * .3f);
            multiplierSequence = DOTween.Sequence();

            Sequence _movement = DOTween.Sequence();
            {
                _movement.Join(multiplier.rectTransform.DOAnchorPosX(attributes.MultiplierMovement, attributes.MultiplierMovementDuration)
                         .SetEase(attributes.MultiplierMovementEase));

                _movement.AppendCallback(() =>
                {
                    multiplier.text = $"<size=18>x </size>{multiplierValue.ToString("0.0")}";
                });

                _movement.Append(attributes.MultiplierShake.Shake(multiplier.rectTransform));
            }

            Sequence _flash = DOTween.Sequence();
            {
                _flash.AppendInterval(attributes.MutliplierFlashDelay);
                _flash.AppendCallback(() =>
                {
                    multiplier.colorGradientPreset = attributes.FeedbackGradient;
                });

                _flash.AppendInterval(attributes.MutliplierFlashDuration);
                _flash.AppendCallback(() =>
                {
                    multiplier.colorGradientPreset = attributes.MultiplierGradient;
                });
            }

            multiplierSequence.Join(_movement);
            multiplierSequence.Join(_flash);
        }

        public void ResetCombo()
        {
            scoreMultiplierHelper = score;
            IncreaseMultiplier(1.0f);

            currentUpdateIndex = 0;
            scoreGaugeHelper = 0;
            gauge.ResetCombo();

            // Remove Player bonus.
        }

        public void ResetScore()
        {
            if (scoreSequence.IsActive())
                scoreSequence.Complete();

            if (multiplierSequence.IsActive())
                multiplierSequence.Complete();

            multiplier.text = $"<size=18>x </size>1.0";
            foreach (var _score in scoreTexts)
            {
                _score.text = "0";
            }

            score = 0;
            lastScore = 0;
            scoreMultiplierHelper = 0;
            multiplierValue = 1.0f;

            currentUpdateIndex = 0;

            gauge.ResetGauge();
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this; 
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if ((lastScore != score) && !scoreSequence.IsActive())
            {
                UpdateScore();
            }
        }
        #endregion
    }
}
