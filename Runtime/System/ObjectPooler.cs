using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
    [TodoHeader("Fix issue when not enough pools are generated, it should continue to generate extra pools", order = 0)]
    [ScriptHeader("ObjectPooler is a centralized multi object pooling system. It provides a singular end point for all pooling related functions", order = 1)]
    public class ObjectPooler : MonoSingletonGlobal<ObjectPooler>
    {
        [ReadOnly]
        [Note("Pooling Components")] 
        [SerializeField]
        private List<PoolingComponent> poolingComponents;

        protected override void Awake()
        {
            base.Awake();
            
            //Init variables
            poolingComponents = new();
        }

        #region Public calls
        public IPoolingPrefab GetPool(IPoolingPrefab poolingPrefab)
        {
            //Create pool if not exist 
            CreatePoolIfNotExist(poolingPrefab);
           
            //Retrieve pooling component
            return RetrievePool(poolingPrefab);
        }
        
        public T GetPool<T>(IPoolingPrefab poolingPrefab, (float3 pos, quaternion rot, bool enable) pooledEntitySpawnInfo) where T : class
        {
            //Create pool if not exist 
            CreatePoolIfNotExist(poolingPrefab);
           
            //Retrieve pooling component
            var pooledEntity = RetrievePool(poolingPrefab);
            
            //Null check only in editor
#if IN_EDITOR_DEBUGGING
            if (pooledEntity == null)
            {
                LogMessage.DebugError("No Entity found");
                return null;
            }
#endif
            pooledEntity.OnCreated(true, new float3(0), quaternion.identity);

            return pooledEntity as T;
        }

        public void ReturnPool(IPoolingPrefab poolingClone)
        {
            var (poolExists, poolingPrefabId, poolingCloneId) = CheckIfPoolExistsAndReturnIndex(poolingClone);
            
            if (!poolExists) 
                return;
            
            var pool = poolingComponents[poolingPrefabId].pools[poolingCloneId];
            pool.OnCreated(false, new float3(0), quaternion.identity);
        }
        #endregion

        #region Create Pool Helpers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreatePoolIfNotExist(IPoolingPrefab poolingPrefab)
        {
            //Create and set original id for all new pooling prefabs
            poolingPrefab.OriginalId = poolingPrefab.gameObject.GetInstanceID();
                
            if (!CheckIfPoolExists(poolingPrefab.OriginalId))
            {
                //Create pool if doesnt exist
                poolingComponents.Add(new PoolingComponent
                    {
                        //Obj data
                        originalPrefab = poolingPrefab,
                        id = poolingPrefab.OriginalId,

                        //Runtime val
                        poolCount = poolingPrefab.PoolCount,
                        pools = InstantiatePool(poolingPrefab, poolingPrefab.PoolCount),
                    }
                );
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CheckIfPoolExists(int instanceId)
        {
            for (var i = 0; i < poolingComponents.Count; i++)
            {
                if (poolingComponents[i].id == instanceId)
                {
                    //Found prefab pooling component
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<IPoolingPrefab> InstantiatePool(IPoolingPrefab poolingPrefab, int count)
        {
            var pools = new List<IPoolingPrefab>();
            for (var i = 0; i < count; i++)
            {
                var instantiatedGameObject = Instantiate(poolingPrefab.gameObject, this.transform);
                
#if IN_EDITOR_DEBUGGING
                //Names are only necessary for in editor, no need to set names in built player
                instantiatedGameObject.name += (" " + i);
#endif
                
                if (!instantiatedGameObject.TryGetComponent(out IPoolingPrefab instantiatedPool)) 
                    continue;
                
                instantiatedPool.OnSetPool(poolingPrefab.OriginalId);
                pools.Add(instantiatedPool);
            }

            return pools;
        }
        #endregion

        #region Get Pool Helpers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IPoolingPrefab RetrievePool(IPoolingPrefab poolingPrefab)
        {
            var poolingComponentIndex = RetrievePoolingComponentIndexByPoolingPrefabId(poolingPrefab.OriginalId);
            var poolsToPickFrom = poolingComponents[poolingComponentIndex].pools;
            var (_, index) = CheckIfInactivePoolExistAndReturnIndex(poolsToPickFrom);

            return poolsToPickFrom[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int RetrievePoolingComponentIndexByPoolingPrefabId(int poolingPrefabId)
        {
            for (var i = 0; i < poolingComponents.Count; i++)
            {
                if (poolingComponents[i].id == poolingPrefabId)
                {
                    return i;
                }
            }
            
            LogMessage.DebugError("No pooling component found");
            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (bool, int) CheckIfInactivePoolExistAndReturnIndex(IReadOnlyList<IPoolingPrefab> pools)
        {
            for (var i = 0; i < pools.Count; i++)
            {
                if (!pools[i].gameObject.activeSelf)
                    return (true, i);
            }

            return (false, 0);
        }
        #endregion
        
        #region Return Pool Helpers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (bool, int, int) CheckIfPoolExistsAndReturnIndex(IPoolingPrefab poolingClone)
        {
            var originalId = poolingClone.OriginalId;
            var cloneId = poolingClone.CloneId;

            for (var i = 0; i < poolingComponents.Count; i++)
            {
                if (originalId != poolingComponents[i].id)
                    continue;
                
                var pools = poolingComponents[i].pools;
                
                for (var j = 0; j < pools.Count; j++)
                {
                    if (cloneId == pools[j].CloneId)
                    {
                        return (true, i, j);
                    }
                }
            }
            
            LogMessage.DebugError("Cant find parent pooling prefab");
            return (false, -1, -1);
        }
        #endregion
    }
}