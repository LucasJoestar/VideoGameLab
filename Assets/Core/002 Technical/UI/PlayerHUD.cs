// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shmup
{

    public class PlayerHUD : MonoBehaviour
    {
        [Header("Secondary Weapons")]
        [SerializeField] private Image secondaryWeaponsIcon = null;
        [SerializeField] private TextMeshProUGUI secondaryWeaponsAmmo = null;
        [Header("Bomb")]
        [SerializeField] private TextMeshProUGUI bombAmmo = null;

        public Image SecondaryWeaponsIcon => secondaryWeaponsIcon;
        public TextMeshProUGUI SecondaryWeaponsAmmo => secondaryWeaponsAmmo;
        public TextMeshProUGUI BombAmmo => bombAmmo;
    }
}