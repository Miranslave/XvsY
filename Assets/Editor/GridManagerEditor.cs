using Script;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    private bool showDimensions = true;
    private bool showCellSize = true;
    private bool showAppearance = true;
    private bool showDebug = true;

    public override void OnInspectorGUI()
    {
        GridManager grid = (GridManager)target;
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Grid Manager" , EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        
        showDimensions = EditorGUILayout.Foldout(showDimensions, "Grid dimension", true);
        if (showDimensions)
        {
            grid.width = EditorGUILayout.IntSlider("Width", grid.width, 1, 50);
            grid.height = EditorGUILayout.IntSlider("Height", grid.height, 1, 50);
            
            // force update
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(grid);
                SceneView.RepaintAll();
            }
        }

        showCellSize = EditorGUILayout.Foldout(showCellSize, "Cell size", true);
        if (showCellSize)
        {
            grid.cellWidth = EditorGUILayout.Slider("Width", grid.cellWidth, 0.1f, 10f);
            grid.cellHeight = EditorGUILayout.Slider("Height", grid.cellHeight, 0.1f, 10f);
            
            // force update
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(grid);
                SceneView.RepaintAll();
            }
        }


        showAppearance = EditorGUILayout.Foldout(showAppearance, "Appearence", true);
        if (showAppearance)
        {
            grid.lineColor = EditorGUILayout.ColorField("Gizmo color", grid.lineColor);
            grid.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab of a cell", grid.prefab, typeof(GameObject), false);
            grid.highlightPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab for highlight", grid.highlightPrefab, typeof(GameObject), false);
            //grid.sprites = (Sprite[])EditorGUILayout.ObjectField("Sprites", grid.sprites, typeof(Sprite[]), false);
        }
        showDebug = EditorGUILayout.Foldout(showDebug, "Debug menu", false);
        if (showDebug)
        {
            grid._ishighlightcursor = EditorGUILayout.Toggle("ishighlightcursor debug", grid._ishighlightcursor);
            grid.highlightInstance =
                (GameObject)EditorGUILayout.ObjectField("HighlightInstance", grid.highlightInstance, typeof(GameObject),true);
            if (GUILayout.Button("Reinitialiser la grille"))
            {
                // Tu peux appeler une méthode publique ici
                ResetGrid(grid);
                Debug.Log("Reinitialisation demandée !");
            }

            // Pour notifier Unity qu’un changement a été fait
            if (GUI.changed)
            {
                EditorUtility.SetDirty(grid);
            }
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    public void ResetGrid(GridManager grid)
    {
        grid.width = 3;
        grid.height = 3;
        grid.cellWidth = 1;
        grid.cellHeight = 1;
    }
}
