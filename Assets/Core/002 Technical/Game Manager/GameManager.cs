// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using System;
using UnityEngine.Playables;
using UnityEngine;

namespace Shmup
{
    public class GameManager : MonoBehaviour
    {
        #region Global Members
        public static GameManager Instance { get; private set; }

        [Header("REFERENCES")]

        [SerializeField] private GameMenus menus = null;

        [Header("TIMELINES")]

        [SerializeField] private PlayableDirector partOne = null;
        [SerializeField] private PlayableDirector partTwo = null;
        #endregion

        #region Game Life
        public bool IsLive = false;
        private Sequence endSequence = null;

        // -----------------------

        public void StartRun()
        {
            IsLive = true;

            partOne.Play();
        }

        public void Restart()
        {
            Pool.ResetAll();

            partOne.Stop();
            partTwo.Stop();

            StartRun();
        }

        public void Victory()
        {
            Stop(menus.OpenVictory);
        }

        public void Defeat()
        {
            Stop(menus.OpenDefeat);
        }

        private void Stop(Action _callback)
        {
            if (endSequence.IsPlaying())
                return;

            endSequence = DOTween.Sequence();
            endSequence.AppendInterval(1f);
            endSequence.AppendCallback(() =>
            {
                IsLive = false;

                partOne.Pause();
                partTwo.Pause();

                _callback();
            });

            endSequence.Play();
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            Instance = this;
        }

        private void OnApplicationFocus(bool focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        #endregion
    }
}
