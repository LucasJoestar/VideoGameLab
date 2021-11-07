// ===== Video Game Lab Game Jam - https://github.com/LucasJoestar/VideoGameLab ===== //
//
// Notes:
//
// ================================================================================== //

using System;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Shmup
{
    /// <summary>
    /// Base poolable object interface.
    /// </summary>
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        void OnCreated(Pool<T> _pool);

        void OnGetFromPool();

        void OnSendToPool();
    }

    public abstract class Pool
    {
        protected static List<Pool> allPools = new List<Pool>();

        // -----------------------

        public static void ResetAll()
        {
            foreach (Pool _pool in allPools)
            {
                _pool.Reset();
            }
        }

        public abstract void Reset();
    }

    public class Pool<T> : Pool where T : MonoBehaviour, IPoolable<T>
    {
        #region Global Members
        public const int DefaultExpandSize = 3;

        private List<T> freeInstances = new List<T>();

        public int ExpandSize = DefaultExpandSize;

        private T[] pool = new T[] { };
        private int poolIndex = -1;

        // -----------------------

        public Pool(int _size, int _expandSize = DefaultExpandSize)
        {
            pool = new T[_size];
            ExpandSize = _expandSize;

            allPools.Add(this);
        }
        #endregion

        #region Pool Reset
        public override void Reset()
        {
            for (int _i = freeInstances.Count; _i-- > 0;)
            {
                var _instance = freeInstances[_i];
                SendToPool(_instance);
            }
        }
        #endregion

        #region Pool Behaviour
        public T GetFromPool(T _prefab)
        {
            T _instance;
            if (poolIndex == -1)
            {
                _instance = Object.Instantiate(_prefab);
                _instance.OnCreated(this);
            }
            else
            {
                _instance = pool[poolIndex];
                poolIndex--;
            }

            freeInstances.Add(_instance);
            _instance.transform.SetParent(null);

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
            freeInstances.Remove(_instance);
        }

        // -----------------------

        public void Clear(bool _doDestroy)
        {
            if (_doDestroy)
            {
                foreach (T _object in pool)
                    Object.Destroy(_object.gameObject);
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
