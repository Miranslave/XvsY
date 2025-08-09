using UnityEngine;
using UnityEngine.UIElements;

namespace Script
{
    public class Tile
    {
        public Vector2 gridPosition;
        public Vector3 worldPosition;
        public GameObject occupant;

        public bool IsOccupied => occupant != null;

        public Tile(Vector2 gridPosition,Vector3 worldPosition)
        {
            this.worldPosition = worldPosition;
            this.gridPosition = gridPosition;
            this.occupant = null;
        }
        public Tile()
        {
            this.gridPosition = Vector2.negativeInfinity;
            this.occupant = null;
        }

        public void Innit(GameObject g)
        {
            occupant = g;
        }
        
    }
}
