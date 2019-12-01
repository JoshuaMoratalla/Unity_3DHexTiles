using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Values : MonoBehaviour
{
    [SerializeField] public Coordinate Tile_Coordinate = null;// x axis in unity and x axis in x,y coord system

    [SerializeField] private Material defaultMaterial;
    //[SerializeField] private Material changedMaterial;


    public Coordinate getTile_Coord {
        get { return Tile_Coordinate; }
        set {
            if (Tile_Coordinate ==null ) {
                Tile_Coordinate = value;
            } else {
                Debug.Log("Failed to Assign Hex Coord; already assigned");
                Debug.Log(value);
            }
        }
    }
    //Get the Tile Row associated with this gameobject's position in the tile set
    public int getTile_Row { 
        get { return Tile_Coordinate.getRow;} 
    }
    //Get the Tile Row associated with this gameobject's position in the tile set
    public int getTile_Column {
        get { return Tile_Coordinate.getColumn; }
    }

    // Update is called once per frame
    public void ChangeTo_DefaultMaterial() {
        this.GetComponent<Renderer>().material = defaultMaterial;
    }
    public void ChangeMaterial(Material material) {
        this.GetComponent<Renderer>().material = material;
    }

    public bool Compare_Tile(Tile_Values tile) {
        if (tile == null) {
           // Debug.Log("Compared a  null tile");
            return false;
        }

        if (getTile_Coord.Compare_Coordinates(tile.getTile_Coord)) {
            return true;
        } else {
            return false;
        }
    }
}

/*

        [SerializeField] private int row = -1; // z axis in unity or y axis in x,y coord system
   [SerializeField] private int column = -1; // x axis in unity and x axis in x,y coord system

   [SerializeField] private Material defaultMaterial;

   public int Row { 
       get { return row; }
       set { 
           if ( row == -1 && value > -1) {
               row = value;
           } 
       }
   }
   public int Column {
       get { return column; }
       set { if (column == -1 && value > -1) {
               column = value;
           } 
       }
   }

   */
// Start is called before the first frame update
// z axis in unity or y axis in x,y coord system
