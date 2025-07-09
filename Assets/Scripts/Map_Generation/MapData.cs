using UnityEngine;

namespace Prototype_S
{
    public struct MapData
    {
        public int mapHeight;
        public int mapWidth;

        public MapData(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
        }
    }
}
