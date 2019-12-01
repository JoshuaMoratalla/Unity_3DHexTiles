using UnityEngine;
using System.Collections;

public class Coordinate : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private int column = -1;
    [SerializeField] private int row = -1; // z axis in unity or y axis in x,y coord system

    public Coordinate(int x, int y) {
        this.column = x;
        this.row = y;
    }

    public int getRow {
        get { return row; }
       
    }
    public int getColumn {
        get { return column; }
        
    }

    public bool Compare_Coordinates(Coordinate other_coordinate) {

        if (other_coordinate == null) {
         //   Debug.Log("Compared Null coordinate");
         //   Debug.Log("Coordinate origin"+getColumn + " | " + getRow);
            return false;
        }
        if (getRow == other_coordinate.getRow &&
            getColumn == other_coordinate.getColumn) {
            return true;
        } else {
            return false;
        }
    }

}
