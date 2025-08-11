using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class DestructibleTileManager : MonoBehaviour
    {
        
        public Tilemap DestructibleTilemap;
        public Dictionary<Vector3Int, int> tileHealth = new Dictionary<Vector3Int, int>();
        
        //singleton
        public static DestructibleTileManager Instance { get; private set; }
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }

            Instance = this;
        }

        public void DamageTile(Vector3Int cellPosition, int damage)
        {

            if (!tileHealth.ContainsKey(cellPosition))
            {
                Log.Info("non-destructible tile");
                return;
            }
            
            Log.Info("damaging tile.");
            tileHealth[cellPosition] -= damage;

            //remove tile both visually and behaviourally(?)
            if (tileHealth[cellPosition] <= 0)
            {
                Log.Info("Removing tile.");
                tileHealth.Remove(cellPosition);
                DestructibleTilemap.SetTile(cellPosition, null);
            }
        }
    }
}
