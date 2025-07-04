using Unity.Mathematics;
using UnityEngine;

namespace Prototype_S
{
    public class MapVisualizer : MonoBehaviour
    {

        public int mapWidth;
        public int mapHeight;
        public float noiseScale;
        
        public bool autoUpdate;
        
        public void VisualizeMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

            MapDisplay display = FindAnyObjectByType<MapDisplay>();
            display.DrawNoiseMap(noiseMap);
            
        }
    }
}
