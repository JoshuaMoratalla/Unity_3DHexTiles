using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexTile_Set : MonoBehaviour
{

    //Don't make it a mono behaviour or maybe keep it
    //keep it for now but later on change to a class so it can instanitate dynamic Hextile dimensions on combat instances
    public GameObject hexagontile;
    public Material material_flash;
    public Material material_select;
    public int scaling_factor;
    [SerializeField] public  int rows;
    [SerializeField] public  int columns;
    [SerializeField] public GameObject[,] TileSet; //refactor for Tile values


    // Start is called before the first frame update
    //https://answers.unity.com/questions/319802/rotation-when-importing-from-blender.html
    void Start()
    {
        CreateTileSet();
    }

    public GameObject getTile(int column, int row) {
        return this.TileSet[column, row];
    }
    public GameObject getTile(Coordinate coord) {
        return this.TileSet[coord.getColumn, coord.getRow];
    }
    public GameObject getTile(Tile_Values tile) {
        return this.TileSet[tile.getTile_Column, tile.getTile_Row];
    }

    private void CreateTileSet() {
        TileSet = new GameObject[columns, rows];

        Debug.Log("Ranges for columns and rows   |" + rows + "," + columns);
        Vector3 hex_boxsize = hexagontile.GetComponent<BoxCollider>().size;

        float literalSize_x = hex_boxsize.x * scaling_factor;
        float literalSize_y = hex_boxsize.y * scaling_factor;
        float literalSize_z = hex_boxsize.z * scaling_factor;

        float newCentred_x = (-((columns + 0.5f) * literalSize_x) / 2) + (literalSize_x / 2);
        float newCentred_z = (-(((rows - 1f) * (0.75f * literalSize_z)) + literalSize_z) / 2) + (literalSize_z / 2);

        for (int z = 0; z < rows; z++) {
            for (int x = 0; x < columns; x++) {
                float current_z_offset = z * literalSize_z * 0.75f;
                float current_x_offset;

                if (z % 2 == 0) {
                    current_x_offset = x * literalSize_x;
                } else {
                    current_x_offset = (x * literalSize_x) + (literalSize_x / 2);

                }
             
                GameObject newTile = Instantiate(hexagontile, new Vector3(newCentred_x + current_x_offset, -literalSize_y, newCentred_z + current_z_offset), new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                
                
                newTile.transform.parent = this.transform;
                newTile.GetComponent<Tile_Values>().getTile_Coord = new Coordinate(x, z);
                TileSet[x, z] = newTile;

             //   Debug.Log(x + "|" + z);
            }
        }
    }

    // Update is called once per frame

    public List<Coordinate> HexLine(Tile_Values start_tile, Tile_Values end_tile, int direction) {

        List<Coordinate> output_list = new List<Coordinate>();

        Coordinate retrieved = start_tile.getTile_Coord;
        
        Debug.Log("Should Enter");
        for (int a = 0 ; !end_tile.getTile_Coord.Compare_Coordinates(retrieved); a++) {
            Debug.Log("Dit Enter");
            Debug.Log("1");
            retrieved = GetNeighbour(retrieved, direction);
            Debug.Log("2");

            if(a >= 20) {
                Debug.Log("Exceeded 20 increments");
                break;
            } else if ((-1 < retrieved.getColumn && retrieved.getColumn < this.columns) &&
                (-1 < retrieved.getRow && retrieved.getRow < this.rows)) {
                Debug.Log("3");
                output_list.Add(retrieved);
                Debug.Log("4");
            }  else {
                Debug.Log("5");
                Debug.Log(string.Format("Caught Error:Hexline {0},{1} will reach out to {2},{3}, an out of bounds tile", start_tile.getTile_Column , start_tile.getTile_Row, retrieved.getColumn, retrieved.getRow));
                break;
            }
        }
        
        string plop = "";
        
        foreach (Coordinate coord in output_list) {
            Debug.Log(coord.ToString());
            plop += coord.ToString();
        }
        return output_list;
    }

    public List<Coordinate> HexLine(Coordinate start_coord, int range, int direction) {
        List<Coordinate> output_list = new List<Coordinate>();

        int tile_x = start_coord.getColumn;
        int tile_z = start_coord.getRow;

        Coordinate retrieved = null;
        
        for (int a = 0; a < range; a++) {

            retrieved = GetNeighbour(start_coord, direction);


            if ((-1 < tile_x && tile_x < this.columns) &&
                (-1 < tile_z && tile_z < this.rows)) {
                // Debug.Log(string.Format("Tile  x|z : {0}|{1}", tile_x, tile_z));
                // tile_set.getTile(tile_x, tile_z).GetComponent<Renderer>().material = line_highlight;
                output_list.Add(retrieved);
            } else {
                Debug.Log(string.Format("Caught Error:Hexline {0},{1} will reach out to {2},{3}", start_coord.getColumn, start_coord.getRow, tile_x, tile_z));
                //break;
            }
        }
        
        return output_list;
    }

    public List<Coordinate> HexLine(Coordinate start_coord, Coordinate end_tile, int direction) {

        List<Coordinate> output_list = new List<Coordinate>();

        int tile_x = start_coord.getColumn;
        int tile_z = start_coord.getRow;

        Coordinate retrieved = null;

        
            for (; !end_tile.Compare_Coordinates(retrieved); ) {

                retrieved = GetNeighbour(start_coord, direction);


                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {
                    // Debug.Log(string.Format("Tile  x|z : {0}|{1}", tile_x, tile_z));
                    // tile_set.getTile(tile_x, tile_z).GetComponent<Renderer>().material = line_highlight;
                    output_list.Add(retrieved);
                } else {
                    Debug.Log(string.Format("Caught Error:Hexline {0},{1} will reach out to {2},{3}", start_coord.getColumn, start_coord.getRow, tile_x, tile_z));
                    //break;
                }
            }
       
        return output_list;


       
    }

    public List<Coordinate> HexLine(int x, int y, int range, int direction) {
        List<Coordinate> output_list = new List<Coordinate>();

        int tile_x = x;
        int tile_z = y;

        Coordinate retrieved = null;

        if (tile_z % 2 == 0) {
            for (int a = 0; a < range; a++) {
                retrieved = GetNeighbour(new Coordinate(x,y), direction);

                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {

                    output_list.Add(retrieved);
                } else {

                }
            }
        } else {
            for (int a = 0; a < range; a++) {

                retrieved = GetNeighbour(new Coordinate(x,y) , direction);

                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {

                    output_list.Add(retrieved);
                } else {

                }

            }
        }
        return output_list;

    }

    public List<Coordinate> HexCone(Coordinate start_coord, int range, int direction) {
        int augmented_direction = direction;

        if (direction != 300) {
            augmented_direction += 60;
        } else {
            augmented_direction = 0;
        }

        List<Coordinate> coords = new List<Coordinate>();
        for (int a = 0; a <= range; a++) {
            if (a!= 0) {
                Coordinate coord = GetCoord_byRange(start_coord, augmented_direction, a);

                coords.AddRange(HexLine(coord, range, augmented_direction));

            } else {
                coords.AddRange(HexLine(start_coord, range, augmented_direction));
            }
        }

        return null;
    }
   
    public Hashtable HexCircleArea_HT(Coordinate start_coord, int range) {
      
        
        Hashtable ht = new Hashtable();
        for (int a = 1; a <= range; a++) {
            List<Coordinate> surroundingTiles = new List<Coordinate>();

            if (a > 1) {
                ht.Add(a , HexCircleLine(start_coord, a));
            } else { 
                ht.Add(1, GetAllNeighbours(start_coord));
                         }

        }

        //Use hashtable or dictionary for unique coordinates handling

        return ht;
    }
 
    public List<Coordinate> HexCircleArea_List(Coordinate start_coord, int range) {

        List<Coordinate> coord_list = new List<Coordinate>();

        for (int a = 1; a <= range; a++) {
            List<Coordinate> surroundingTiles = new List<Coordinate>();

            if (a > 1) {
                coord_list.AddRange(HexCircleLine(start_coord, a));
            } else {
                coord_list.AddRange(GetAllNeighbours(start_coord));
            }

        }

       



        return coord_list;

    }

    private List<Coordinate> HexCircleLine(Coordinate start_coord , int range) {

        HashSet<Coordinate> coordinateSet = new HashSet<Coordinate>();
        //go 240 down by a range

        Coordinate coord = GetCoord_byRange(start_coord,240, range);


        for (int a = 0; a < 360; a+=60) {
            List<Coordinate> list = HexLine(coord, range, a);

            foreach (Coordinate line_coord in list) {
                coordinateSet.Add(line_coord);
            }
            coord = list[range];

        }

        return coordinateSet.ToList();
        
    }

    public List<Coordinate>[] HexConvergence(Coordinate startcoord, int range) {
        List<Coordinate>[] coordinates = new List<Coordinate>[range];
        for (int a = 0; a < range; a++) {
            List<Coordinate> coordinate_list = new List<Coordinate>();
            if (a != 0) {
                
                for (int b = 0; b < 360; a += 360) {
                    coordinate_list.Add(GetCoord_byRange(startcoord, b, range));
                }
            
            } else {
                coordinate_list.Add(startcoord);
            }

            coordinates[a] = coordinate_list;
        }

        return coordinates;
    }

    public bool IsOdd(int val) {
        if (val % 2 == 0) {
            return false;
        } else {
            return true;
        }
    }


    public Coordinate GetCoord_byRange(Coordinate coord, int direction, int range) {

        Coordinate coord_buff = coord;
        for (int a = 0; a <= range; a++) {
            coord_buff = GetNeighbour(coord_buff, direction);
        }
        return coord_buff;
        //get a single tile by direction and range aswell as its initinal starting tile position
    }

    public Coordinate GetCoord_byRange(Tile_Values tile, int direction, int range) {

        Coordinate coord_buff = tile.getTile_Coord;
        for (int a = 0; a <= range; a++) {
            coord_buff = GetNeighbour(coord_buff, direction);
        }
        return coord_buff;
        //get a single tile by direction and range aswell as its initinal starting tile position
    }

    public bool IsNeighbour(Tile_Values main_tile, Tile_Values other_tile) {


        int main_col = main_tile.getTile_Column;
        int main_row = main_tile.getTile_Row;

        int other_col = other_tile.getTile_Column;
        int other_row = other_tile.getTile_Row;

        if (IsOdd(main_row)) {
            return IsNeighbour_Odd(main_col, main_row, other_col, other_row);
        } else {
            return IsNeighbour_Even(main_col, main_row, other_col, other_row);
        }

    }

    public bool IsNeighbour(Coordinate main_tile, Coordinate other_tile) {


        int main_col = main_tile.getColumn;
        int main_row = main_tile.getRow;

        int other_col = other_tile.getColumn;
        int other_row = other_tile.getRow;

        if (IsOdd(main_row)) {
            return IsNeighbour_Odd(main_col, main_row, other_col, other_row);
        } else {
            return IsNeighbour_Even(main_col, main_row, other_col, other_row);
        }

    }

    private bool IsNeighbour_Even(int main_col, int main_row, int other_col, int other_row) {
        if (main_col + 1 == other_col) { //0 or 360
            return true;
        } else if (main_row + 1 == other_row) {//60
            return true;
        } else if (main_col - 1 == other_col && main_row + 1 == other_row) {//120
            return true;
        } else if (main_col - 1 == other_col) {//180
            return true;
        } else if (main_col - 1 == other_col && main_row - 1 == other_row) {//240
            return true;
        } else if (main_row - 1 == other_row) {//300
            return true;
        } else {
            return false;
        }

    }

    private bool IsNeighbour_Odd(int main_col, int main_row, int other_col, int other_row) {
        if (main_col + 1 == other_col) {
            return true;
        } else if (main_col + 1 == other_col && main_row + 1 == other_row) {
            return true;
        } else if (main_row + 1 == other_row) {
            return true;
        } else if (main_col - 1 == other_col) {
            return true;
        } else if (main_row - 1 == other_row) {
            return true;
        } else if (main_col + 1 == other_col && main_col - 1 == other_col) {
            return true;
        } else {
            return false;
        }
    }

    private List<Coordinate> GetAllNeighbours(Coordinate coord) {
        //for circule thingo and possibly others;
      

            //get all neighbours of a coordinate from its 6 sourrounding tiles
            List<Coordinate> output_list = new List<Coordinate>();
            for (int a = 0; a <= 360; a += 60) {
                output_list.Add( GetNeighbour(coord, a));
            }
        return output_list;
    }




    private Coordinate GetNeighbour(Coordinate coord, int direction) {
        int row_pos = coord.getRow;
        int col_buff = coord.getColumn;
        int row_buff = coord.getRow;
        if (direction == 00.0f) {
            col_buff += 1;
        } else if (direction == 60.0f) {
            if (IsOdd(row_pos)) {
                col_buff += 1;
                row_buff += 1;
            } else {
                row_buff += 1;
            }
        } else if (direction == 120.0f) {
            if (IsOdd(row_pos)) {
                row_buff += 1;
            } else {
                col_buff -= 1;
                row_buff += 1;
            }
        } else if (direction == 180.0f) {
            col_buff -= 1;
        } else if (direction == 240.0f) {
            if (IsOdd(row_pos)) {
                row_buff -= 1;
            } else {
                col_buff -= 1;
                row_buff -= 1;
            }
        } else if (direction == 300.0f) {
            if (IsOdd(row_pos)) {
                col_buff += 1;
                row_buff -= 1;
            } else {
                row_buff -= 1;
            }
        } else {
            Debug.Log(string.Format("Break", col_buff, row_buff));
        }
        return new Coordinate(col_buff, row_buff);
    }
}
