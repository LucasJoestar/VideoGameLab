// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class GameManager : MonoBehaviour
    {
        #region Global Members
        public static GameManager Instance { get; private set; }
        #endregion

        #region Game Life
        public bool IsLive = false;

        // -----------------------

        public void StartRun()
        {
            IsLive = true;
        }

        public void Restart()
        {
            IsLive = true;
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
