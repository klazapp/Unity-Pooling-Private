using System;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
    public interface IPoolingPrefab
    {
        #region Pooling Utility

        public int PoolCount { get; set; }

        public int OriginalId { get; set; }

        public int CloneId { get; set; }

        public void OnSetPool(int originalId);

        #endregion

        #region Monobehaviour Utility
        public GameObject gameObject { get; }

        #endregion

        #region Runtime

        public void OnCreated(bool enable, float3 pos, quaternion rot);

        #endregion
    }
}