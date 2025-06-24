using System;
using UnityEngine;

namespace Prototype_S
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject worldItemPrefab;
        
        //provides the ItemSpawner with the template prefab needed to spawn items
        private void Awake()
        {
            ItemSpawner.Initialize(worldItemPrefab);
        }
    }
}
