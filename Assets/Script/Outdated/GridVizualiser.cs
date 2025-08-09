using System;
using Script;
using UnityEngine;

[ExecuteAlways]
public class GridVizualiser : MonoBehaviour
{
    [Header("Grid parameters")]
    [Min(1)]
    public int width = 3, height = 3;
    [Min(0)]
    public float cellWidth = 1f, cellHeight = 1f;

    public bool debug;
    public Color lineColor = Color.green;
    public GridManager gm;
    private void Awake()
    {
        gm = GetComponent<GridManager>();
        width = gm.width;
        height = gm.height;
        cellHeight = gm.cellHeight;
        cellWidth = gm.cellWidth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Vector3 origin = this.transform.position;
        Vector3 offset  = new Vector3(height * cellHeight / 2f,width * cellWidth / 2f);

        // Lignes verticales
        for (int x = 0; x <= width; x++)
        {
            Vector3 start = new Vector3(x * cellWidth, 0, 0) + origin - offset;
            Vector3 end = start + new Vector3(0, height * cellHeight, 0);
            Gizmos.DrawLine(start, end);
        }

        // Lignes horizontales
        for (int y = 0; y <= height; y++)
        {
            Vector3 start = new Vector3(0, y * cellHeight, 0) + origin -offset;
            Vector3 end = start + new Vector3(width * cellWidth, 0, 0);
            Gizmos.DrawLine(start, end);
        }
    }
}
