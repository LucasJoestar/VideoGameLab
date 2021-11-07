// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine.Playables;
using UnityEngine;

namespace Shmup
{
    public class GameManager : MonoBehaviour
    {
        #region Global Members
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayableDirector partOne = null;
        [SerializeField] private PlayableDirector partTwo = null;
        #endregion

        #region Game Life
        public bool IsLive = false;

        // -----------------------

        public void StartRun()
        {
            IsLive = true;

            partOne.Play();
        }

        public void Restart()
        {
            IsLive = true;

            partOne.Stop();
            partTwo.Stop();

            partOne.Play();
        }

        public void Stop()
        {
            IsLive = false;
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
