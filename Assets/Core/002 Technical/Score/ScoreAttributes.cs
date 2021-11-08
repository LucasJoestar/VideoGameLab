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
    [Serializable]
    public class ShakeTween
    {
        [Range(0f, 2f)] public float Duration = .1f;
        [Range(0f, 100f)] public float Strength = 1f;
        [Range(0f, 250f)] public int Vibrato = 10;
        [Range(0f, 500f)] public float Randomness = 90f;
        public bool Snapping = true;
        public bool FadeOut = true;
        public Ease Ease = Ease.Linear;

        // -----------------------

        public Tween Shake(Transform _transform, float _coef = 1f)
        {
            return _transform.DOShakePosition(Duration * _coef, Strength * _coef, Vibrato * (int)_coef, Randomness * _coef, Snapping, FadeOut).SetEase(Ease);
        }

        public Tween Shake(RectTransform _rectTransform, float _coef = 1f)
        {
            return _rectTransform.DOShakeAnchorPos(Duration * _coef, Strength * _coef, Vibrato * (int)_coef, Randomness * _coef, Snapping, FadeOut).SetEase(Ease);
        }
    }

    [CreateAssetMenu(fileName = "ScoreAttributes", menuName = "SHMUP/Score Attributes", order = 250)]
    public class ScoreAttributes : ScriptableObject
    {
        #region Global Members
        public TMP_ColorGradient FeedbackGradient = null;

        [Space(10f), Header("SCORE")]

        [Range(0f, 50f)] public float ScoreMovement = .1f;
        [Range(0f, 5f)] public float ScoreMovementDuration = 1f;
        public AnimationCurve ScoreMovementEase = new AnimationCurve();

        [Space(5f)]

        public ShakeTween ScoreShake = new ShakeTween();
        public TMP_ColorGradient ScoreGradient = null;
        [Range(0f, 5f)] public float ScoreFlashDuration = 1f;

        [Space(10f), Header("MULTIPLIER")]

        [Range(-50f, 0f)] public float MultiplierMovement = .1f;
        [Range(0f, 5f)] public float MultiplierMovementDuration = 1f;
        public AnimationCurve MultiplierMovementEase = new AnimationCurve();

        public ShakeTween MultiplierShake = new ShakeTween();

        [Space(5f)]

        public TMP_ColorGradient MultiplierGradient = null;

        [Range(0f, 2f)] public float MutliplierFlashDelay = .1f;
        [Range(0f, 5f)] public float MutliplierFlashDuration = 1f;

        [Space(10f), Header("GAUGE INCREASE")]

        [Range(0f, 2f)] public float GaugeIncreaseDuration = .2f;
        public Ease GaugeIncreaseEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float GaugeFillDuration = 1f;
        public Ease GaugeFillEase = Ease.Linear;

        [Space(5)]

        public ShakeTween IncreaseShake = new ShakeTween();
        public ShakeTween filledShake = new ShakeTween();

        [Space(5f)]

        [Range(0f, 2f)] public float GaugeCompleteDuration = .2f;
        public Ease GaugeCompleteEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float GaugeCompleteFillDuration = 1f;
        public Ease GaugeCompleteFillEase = Ease.Linear;

        [Space(10f), Header("GAUGE DECREASE")]

        [Range(0f, 2f)] public float GaugUncolorDuration = .2f;
        public Ease GaugeUncolorEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 2f)] public float GaugeDecreaseDuration = .2f;
        public Ease GaugeDecreaseEase = Ease.Linear;

        [Space(5f)]

        [Range(0f, 5f)] public float GaugeUnfillDuration = 1f;
        public Ease GaugeUnfillEase = Ease.Linear;
        #endregion
    }
}
