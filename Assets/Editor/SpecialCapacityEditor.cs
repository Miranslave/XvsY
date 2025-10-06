using UnityEditor;
using UnityEngine;

namespace Script.Editor
{
    [CustomEditor(typeof(SpecialCapacity), true)]
    public class SpecialCapacityEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SpecialCapacity capacity = (SpecialCapacity)target;

            if (capacity.Icon != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(capacity.Icon.texture, GUILayout.Width(64), GUILayout.Height(64));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            DrawDefaultInspector();
        }
    }
}