using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Biome/New Biome")]
    public class Biome : ScriptableObject
    {
        [SerializeField] private TileBase biomeTile;

        public TileBase BiomeTile => biomeTile;

    }
}
