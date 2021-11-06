// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Shmup
{
    /// <summary>
    /// Base poolable object interface.
    /// </summary>
    public interface IPoolable
    {
        void OnCreated();

        void OnGetFromPool();

        void OnSendToPool();
    }

    public class Pool<T> where T : MonoBehaviour, IPoolable
    {
        #region Global Members
        public const int DefaultExpandSize = 3;

        public int ExpandSize = DefaultExpandSize;

        private T[] pool = new T[] { };
        private int poolIndex = -1;

        // -----------------------

        public Pool(int _size, int _expandSize = DefaultExpandSize)
        {
            pool = new T[_size];
            ExpandSize = _expandSize;
        }
        #endregion

        #region Pool Behaviour
        public T GetFromPool(T _prefab)
        {
            T _instance;
            if (poolIndex == -1)
            {
                _instance = Object.Instantiate(_prefab);
                _instance.OnCreated();
            }
            else
            {
                _instance = pool[poolIndex];
                poolIndex--;
            }

            _instance.gameObject.SetActive(true);
            _instance.OnGetFromPool();

            return _instance;
        }

        public void SendToPool(T _instance)
        {
            poolIndex++;
            if (poolIndex == pool.Length)
            {
                Expand();
            }

            _instance.OnSendToPool();
            _instance.gameObject.SetActive(false);

            pool[poolIndex] = _instance;
        }

        // -----------------------

        public void Clear(bool _doDestroy)
        {
            if (_doDestroy)
            {
                foreach (T _object in pool)
                    Object.Destroy(_object);
            }

            pool = new T[] { };
            poolIndex = -1;
        }

        private void Expand()
        {
            Array.Resize(ref pool, pool.Length + ExpandSize);
        }
        #endregion
    }
}
