using UnityEditor;
using UnityEngine;

namespace Prototype_S
{
    [CustomEditor(typeof(FoliageGenerator))]
    public class FoliageGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            FoliageGenerator mapGen = (FoliageGenerator) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Generate"))
            {
                mapGen.TreeGenerator();
            }
        }
    }
}