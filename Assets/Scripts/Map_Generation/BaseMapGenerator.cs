using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public abstract class BaseMapGenerator : MonoBehaviour, IMapGenerator
    {

        //fields
        [SerializeField] protected Tilemap drawMap;
        [SerializeField] protected MapDataEvent onMapGenerated;
        
        [Header("Map Noise Generation Settings")]
        [SerializeField] protected int mapWidth;
        [SerializeField] protected int mapHeight;
        [SerializeField] protected float scale;
        [SerializeField] protected int octaves;
        [SerializeField] protected float exponent;
        [FormerlySerializedAs("terrainSeed")]
        [Header("Noise Seeds")]
        [SerializeField] protected int seed;

        [ContextMenu("Generate Map")]
        public abstract void Generate();

        public void RaiseMapGeneratedEvent(MapData mapData)
        {
            onMapGenerated.Raise(mapData);
        }
    }
}