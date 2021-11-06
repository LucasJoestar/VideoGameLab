// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using UnityEngine;

namespace Shmup
{
    public class CameraAspectRatio : MonoBehaviour
    {
        #region Global Members
        public static CameraAspectRatio Instance = null; 
        
        [SerializeField, Range(.1f, 1f)] private float gameAspect = .8f / 3f;
        [SerializeField] private new Camera camera = null;
        public Camera Camera => camera; 

        private float lastAspect = 1f;

        // -----------------------

        private void Update()
        {
            float _aspect = (float)Screen.width / Screen.height;
            if (_aspect == lastAspect)
                return;

            lastAspect = _aspect;
            float _scaledHeight = _aspect / gameAspect;

            // Set camera aspect.
            if (_scaledHeight < 1f)
            {
                Rect _rect = new Rect()
                {
                    x = 0f,
                    y = (1f - _scaledHeight) / 2f,
                    width = 1f,
                    height = _scaledHeight
                };

                camera.rect = _rect;
            }
            else
            {
                float scaleWidth = 1f / _scaledHeight;

                Rect _rect = new Rect()
                {
                    x = (1f - scaleWidth) / 2f,
                    y = 0,
                    width = scaleWidth,
                    height = 1f
                };

                camera.rect = _rect;
            }
        }

        private void Awake()
        {
            if(Instance == null)
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
}
