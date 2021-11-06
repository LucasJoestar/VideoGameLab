using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; 

namespace Shmup
{
    public class ScoreUI : MonoBehaviour
    {
        #region Fields and Properties
        [Header("References")]
        [SerializeField] private TMP_Text scoreText = null; 
        [SerializeField] private Image filledBackground = null;
        [SerializeField] private Image filledForeground = null;

        [Header("Settings")]
        [SerializeField, Range(.1f, 5.0f)] private float fillingSpeed = 1.0f;
        private Sequence sequence = null; 
        #endregion

        #region Methods
        public void UpdateScoreUI(int _totalScore, float _fillingRatio)
        {
            scoreText.text = _totalScore.ToString("0000000000");
            if(sequence.IsActive())
            {
                sequence.Kill(); 
            }
            float _time = (_fillingRatio - filledForeground.fillAmount) / fillingSpeed;
            sequence = DOTween.Sequence();

            sequence.Join(filledForeground.DOFillAmount(_fillingRatio, _time));
            sequence.Play();
        }
        #endregion
    }
}
