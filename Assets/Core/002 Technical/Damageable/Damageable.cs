// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;

namespace Shmup
{
    public abstract class Damageable : MonoBehaviour
    {
        #region Fields and Properties
        [Header("REFERENCES")]

        [SerializeField] protected SpriteRenderer[] sprites = new SpriteRenderer[] { };
        [SerializeField] protected MonoBehaviour[] disabledComponents = new MonoBehaviour[] { };

        [Header("HEALTH")]

        [SerializeField] protected int maxHealth = 100;
        [SerializeField] protected int health = 0;

        [Header("FEEDBACKS")]

        [SerializeField] protected PoolableParticle explosion = null;
        [SerializeField] protected AudioClip[] explosionClips = new AudioClip[] { }; 
        [SerializeField, Range(1, 10)] protected int blinkLoopCount = 8;
        [SerializeField, Range(.1f, 3.0f)] protected float blinkDuration = .4f; 

        public float UniqueBlinkDuration => blinkDuration / (blinkLoopCount * 2);

        public static readonly Pool<PoolableParticle> explosionPool = new Pool<PoolableParticle>(3);
        #endregion

        #region Methods
        protected Sequence sequence = null;

        // -----------------------

        public virtual bool TakeDamages(int _damages)
        {
            health -= _damages;
            if (health <= 0)
            {
                OnDestroyed();
                return true;
            }

            OnTakeDamages();
            return false;
        }

        protected virtual void OnTakeDamages()
        {
            // Blinking
            if (!sequence.IsActive())
            {
                sequence = DOTween.Sequence();
                foreach (var _s in sprites)
                {
                    sequence.Join(_s.DOFade(0f, UniqueBlinkDuration).SetLoops(blinkLoopCount * 2, LoopType.Yoyo));
                }
            }
        }

        protected virtual void OnDestroyed()
        {
            // Emit explosion vfx
            var _explosion = explosionPool.GetFromPool(explosion);
            _explosion.transform.position = transform.position;
            SoundManager.Instance.PlayClipAtPosition(explosionClips[Random.Range(0,explosionClips.Length-1)],transform.position);

            gameObject.SetActive(false);

            for (int i = 0; i < disabledComponents.Length; i++)
            {
                disabledComponents[i].enabled = false;
            }
        }
        #endregion

        #region Mono Behaviour
        protected virtual void OnEnable()
        {
            health = maxHealth;
        }
        #endregion
    }
}
