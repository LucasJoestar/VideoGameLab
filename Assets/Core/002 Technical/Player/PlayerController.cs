// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using DG.Tweening;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        #region Global Members
        [Header("REFERENCES")]
        [SerializeField] private PlayerAttributes attributes = null;
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private new SphereCollider collider = null;

        [Header("Weapons")]
        [SerializeField] private PlayerWeapons mainWeapons = null;
        [SerializeField] private StolenWeapons secondaryWeapon = null;
        [SerializeField] private Bomb bomb = null;

        [Space(5f)]

        [SerializeField] private PlayerDamageable damageable = null;
        [SerializeField] private LayerMask collisionMask = new LayerMask();
        #endregion

        #region Inputs
        private void InputUpdate()
        {
            // Movement.
            Vector2 _movement = attributes.MoveInput.ReadValue<Vector2>();
            if (_movement != Vector2.zero)
            {
                _movement *= attributes.Speed * Time.smoothDeltaTime;
                Move(_movement);
            }

            // Fire.
            if(attributes.FireMainInput.triggered)
            {
                mainWeapons.Fire();
            }

            if (attributes.FireSecondaryInput.triggered)
            {
                secondaryWeapon.Fire();
            }

            if (attributes.FireBombInput.triggered)
            {
                bomb.FireBomb();
            }
        }
        #endregion

        #region Movement and Collisions
        private static readonly RaycastHit[] castBuffer = new RaycastHit[4];

        // -----------------------

        private void Move(Vector3 _velocity, int _recursiveCount = 1)
        {
            float _radius = collider.radius - Physics.defaultContactOffset;
            float _magnitude = _velocity.magnitude;
            float _distance = _magnitude + (Physics.defaultContactOffset * 2f);
            Vector2 _normalizedVelocity = _velocity.normalized;

            int _count = Physics.SphereCastNonAlloc(rigidbody.position, _radius, _normalizedVelocity, castBuffer, _distance, collisionMask, QueryTriggerInteraction.Collide);
            if (_count == 0)
            {
                MoveObject(_velocity);
                return;
            }

            // Get closest hit infos.
            RaycastHit _mainHit = default;
            for (int _i = 0; _i < _count; _i++)
            {
                RaycastHit _hit = castBuffer[_i];
                if (_hit.collider.isTrigger)
                {
                    if(_hit.collider.TryGetComponent<DropableWeapon>(out DropableWeapon _weapon))
                    {
                        secondaryWeapon.SetWeapons(_weapon.WeaponData, _weapon.WeaponSprite, _weapon.Ammo);
                        ScoreManager.Instance.IncreaseScore(_weapon.Score);
                        _weapon.gameObject.SetActive(false);
                        return;
                    }
                    // Every trigger is a hit.
                    if (collider.enabled)
                    {
                        damageable.TakeDamages(1);
                        return;
                    }             
                }
                else
                {
                    float _hitDistance = _hit.distance - Physics.defaultContactOffset;
                    if (_hitDistance <= 0f)
                    {
                        return;
                    }
                    else if (_hitDistance < _distance)
                    {
                        _mainHit = _hit;
                        _distance = _hitDistance;
                    }
                }
            }

            // Move object and get remaining velocity.
            if ((_distance -= Physics.defaultContactOffset) > 0f)
            {
                _velocity = _normalizedVelocity * (_magnitude - _distance);
                MoveObject(_normalizedVelocity * _distance);
            }

            // Recursion limit.
            if (_recursiveCount == 3)
                return;

            // Compute velocity according to main impact normals.
            _velocity -= _mainHit.normal * Vector3.Dot(_velocity, _mainHit.normal);
            if (_velocity != Vector3.zero)
            {
                Move(_velocity, _recursiveCount + 1);
            }

            // ----- Local Method ----- \\

            void MoveObject(Vector3 _movement)
            {
                transform.position = rigidbody.position
                                   += _movement;
            }
        }
        #endregion

        #region Upgrade
        public void UpgradePlayer(PlayerUpgradeType _type, float _value)
        {
            switch (_type)
            {
                case PlayerUpgradeType.Shield:
                    damageable.ActivateShield();
                    break;
                case PlayerUpgradeType.IncreaseFireRate:
                    mainWeapons.IncreaseFireRate(_value);
                    break;
                case PlayerUpgradeType.IncreaseProjectileSize:
                    mainWeapons.IncreaseProjectileSize(_value);
                    break;
                default:
                    break;
            }
        }

        public void SetSecondaryWeapon(WeaponsData _data, Sprite _weaponSprite)
        {
            // secondaryWeapon.SetWeapons(_data);
        }
        #endregion

        #region Mono Behaviour
        private void Awake()
        {
            attributes.MoveInput.Enable();

            attributes.FireMainInput.Enable();
            attributes.FireSecondaryInput.Enable();
            attributes.FireBombInput.Enable();
        }

        private void Update()
        {
            InputUpdate();
        }

        private void OnDestroy()
        {
            attributes.MoveInput.Disable();

            attributes.FireMainInput.Disable();
            attributes.FireSecondaryInput.Disable();
            attributes.FireBombInput.Disable();
        }
        #endregion
    }
}
