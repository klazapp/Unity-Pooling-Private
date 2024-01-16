using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.Klazapp.Utility
{
    [Serializable]
    public class PoolingComponent
    {
            
#if UNITY_EDITOR
        [SerializeReference]
#endif
        
        //Obj data
        public IPoolingPrefab originalPrefab;
        public int id;

        //Runtime pools
#if UNITY_EDITOR
        [SerializeReference]
#endif
        public List<IPoolingPrefab> pools;
        public int poolCount;
    }
}