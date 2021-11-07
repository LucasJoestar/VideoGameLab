using System;
using UnityEngine;

namespace Shmup
{
    public class ScoreManager : MonoBehaviour
    {
        #region Fields and Properties
        public static ScoreManager Instance = null;

        public event Action<PlayerUpgradeType, float> OnUpgradeUnlocked;
        private int totalScore = 0;
        private int lastScore = 0;
        private int currentUpdateIndex = 0; 
        [SerializeField] private PlayerUpgrade[] upgrades = new PlayerUpgrade[] { };
        [SerializeField] private ScoreUI ui = null;
        #endregion

        #region Methods
        public void IncreaseScore(int _increasingValue)
        {
            lastScore += _increasingValue;
            totalScore += _increasingValue;
            float _ratio = (float)lastScore / (float)upgrades[upgrades.Length - 1].ScoreThreshold;

            if (currentUpdateIndex < upgrades.Length && lastScore >= upgrades[currentUpdateIndex].ScoreThreshold)
            {
                OnUpgradeUnlocked?.Invoke(upgrades[currentUpdateIndex].Upgrade, upgrades[currentUpdateIndex].IncreasingValue);
                currentUpdateIndex++;
            }

            ui.UpdateScoreUI(totalScore, _ratio);
        }

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
        #endregion
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

    public enum PlayerUpgradeType
    {
        Shield,
        IncreaseFireRate,
        IncreaseProjectileSize
    }
}
