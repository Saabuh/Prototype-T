using UnityEditor;
using UnityEngine;

namespace Prototype_S
{
    [CustomEditor(typeof(MapVisualizer))]
    public class MapVisualizerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapVisualizer mapVisualizer = (MapVisualizer) target;

            if (DrawDefaultInspector())
            {
                if (mapVisualizer.autoUpdate)
                {
                    mapVisualizer.VisualizeMap();
                }
            }

            if (GUILayout.Button("Generate"))
            {
                mapVisualizer.VisualizeMap();
            }
        }
    }
}
