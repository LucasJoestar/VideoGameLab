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

        [SerializeField] private Image background = null;
        [SerializeField] private Image fill = null;
        [SerializeField] private Image feedback = null;

        [Header("SETTINGS")]

        [SerializeField, Range(.1f, 5.0f)] private float fillingSpeed = 1.0f;
        #endregion

        #region Score
        private Color backgroundColor = new Color();
        private Color fillColor = new Color();

        private Sequence sequence = null;

        // -----------------------

        public void UpdateScoreUI(float _fillingRatio)
        {
            if(sequence.IsActive())
                sequence.Kill();

            float _time = (_fillingRatio - fill.fillAmount) / fillingSpeed;
            sequence = DOTween.Sequence();

            sequence.Join(fill.DOFillAmount(_fillingRatio, _time));
            sequence.Play();
        }

        public void ResetGauge()
        {
            if (sequence.IsActive())
                sequence.Complete();

            background.color = backgroundColor;
            fill.color = fillColor;

            fill.fillAmount = 0f;
            feedback.fillAmount = 0f;
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            backgroundColor = background.color;
            fillColor = fill.color;
        }
        #endregion
    }
}
