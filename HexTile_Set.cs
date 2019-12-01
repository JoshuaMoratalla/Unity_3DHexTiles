using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile_Set : MonoBehaviour
{

    //Don't make it a mono behaviour
    //keep it for now but later on change to a class so it can instanitate dynamic Hextile dimensions on combat instances
    public GameObject hexagontile;
    public Material material_flash;
    public Material material_select;
    public int scaling_factor;
    [SerializeField] public  int rows;
    [SerializeField] public  int columns;
    [SerializeField] public GameObject[,] TileSet;


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
    public List<Coordinate> HexLine(Coordinate start_tile, int range, float direction) {
        List<Coordinate> output_list = new List<Coordinate>();

        int tile_x = start_tile.getColumn;
        int tile_z = start_tile.getRow;

        Coordinate retrieved = null;

        if (start_tile.getRow % 2 == 0) {
            for (int a = 0; a < range; a++) {

                retrieved = GetNeighbour_Tile_Even(start_tile, direction);


                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {
                    // Debug.Log(string.Format("Tile  x|z : {0}|{1}", tile_x, tile_z));
                    // tile_set.getTile(tile_x, tile_z).GetComponent<Renderer>().material = line_highlight;
                    output_list.Add(retrieved);
                } else {
                    Debug.Log(string.Format("Caught Error:Hexline {0},{1} will reach out to {2},{3}", start_tile.getColumn, start_tile.getRow, tile_x, tile_z));
                    //break;
                }
            }
        } else {
            for (int a = 0; a < range; a++) {

                retrieved = GetNeighbour_Tile_Odd(start_tile, direction);

                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {
                    //     Debug.Log(string.Format("Tile  x|z : {0}|{1}", tile_x, tile_z));
                    // tile_set.getTile(tile_x, tile_z).GetComponent<Renderer>().material = line_highlight;
                    output_list.Add(retrieved);
                } else {
                    Debug.Log(string.Format("Caught Error:Hexline {0},{1} will reach out to {2},{3}", start_tile.getColumn, start_tile.getRow, tile_x, tile_z));
                }

            }
        }
        return output_list;
    }

    public List<Coordinate> HexLine(int x, int y, int range, float direction) {
        List<Coordinate> output_list = new List<Coordinate>();

        int tile_x = x;
        int tile_z = y;

        Coordinate retrieved = null;

        if (tile_z % 2 == 0) {
            for (int a = 0; a < range; a++) {
                retrieved = GetNeighbour_Tile_Even(x, y, direction);

                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {

                    output_list.Add(retrieved);
                } else {

                }
            }
        } else {
            for (int a = 0; a < range; a++) {

                retrieved = GetNeighbour_Tile_Odd(x, y, direction);

                if ((-1 < tile_x && tile_x < this.columns) &&
                    (-1 < tile_z && tile_z < this.rows)) {

                    output_list.Add(retrieved);
                } else {

                }

            }
        }
        return output_list;

    }

    public List<Coordinate> HexCone(Coordinate start_tile, int range, float direction) {

        //use your current position as the start
        // go towards one of direction boundaries of the cone
        // forloop or increment across based on range
        // increment that increment on the opposite cone boundary
        return null;
    }

    public List<Coordinate> HexCircle(Coordinate start_tile, int range) {
        //Create HashSet
        //Find Neighbours of start tile
        //store those neighbours,

        //Find neighbours of the first neightbours 


        return null;
    }

    public List<Coordinate> HexConvergence(Coordinate start_tile) {
        return null;
    }

    public bool IsOdd(int val) {
        if (val % 2 == 0) {
            return false;
        } else {
            return true;
        }
    }




    public bool isNeighbour(Tile_Values main_tile, Tile_Values other_tile) {


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

    public bool isNeighbour(Coordinate main_tile, Coordinate other_tile) {


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




    private Coordinate GetNeighbour_Tile_Even(int column, int row, float direction) {
        int col_buff = column;
        int row_buff = row;
        if (direction == 00.0f) {
            col_buff += 1;
        } else if (direction == 60.0f) {
            if (IsOdd(row)) {
                col_buff += 1;
                row_buff += 1;
            } else {
                row_buff += 1;
            }
        } else if (direction == 120.0f) {
            if (IsOdd(row)) {
                row_buff = 1;
            } else {
                col_buff = -1;
                row_buff = +1;
            }
        } else if (direction == 180.0f) {
            col_buff -= 1;
        } else if (direction == 240.0f) {
            if (IsOdd(row)) {
                row_buff += 1;
            } else {
                col_buff -= 1;
                row_buff -= 1;
            }
        } else if (direction == 300.0f) {
            if (IsOdd(row)) {
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

    private Coordinate GetNeighbour_Tile_Even(Coordinate coord, float direction) {
        int hex_Row = coord.getRow;
        int col_buff = coord.getColumn;
        int row_buff = coord.getRow;
        if (direction == 00.0f) {
            col_buff += 1;
        } else if (direction == 60.0f) {
            if (IsOdd(hex_Row)) {
                col_buff += 1;
                row_buff += 1;
            } else {
                row_buff += 1;
            }
        } else if (direction == 120.0f) {
            if (IsOdd(hex_Row)) {
                row_buff = 1;
            } else {
                col_buff = -1;
                row_buff = +1;
            }
        } else if (direction == 180.0f) {
            col_buff -= 1;
        } else if (direction == 240.0f) {
            if (IsOdd(hex_Row)) {
                row_buff += 1;
            } else {
                col_buff -= 1;
                row_buff -= 1;
            }
        } else if (direction == 300.0f) {
            if (IsOdd(hex_Row)) {
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

    private Coordinate GetNeighbour_Tile_Odd(int column, int row, float direction) {

        int col_buff = column;
        int row_buff = row;
        if (direction == 00.0f) {
            col_buff = +1;

        } else if (direction == 60.0f) {
            if (IsOdd(row)) {
                row_buff += 1;
            } else {
                col_buff += 1;
                row_buff += 1;
            }
        } else if (direction == 120.0f) {
            if (IsOdd(row)) {
                col_buff -= 1;
                row_buff += 1;
            } else {
                row_buff += 1;
            }
        } else if (direction == 180.0f) {
            col_buff -= 1;
        } else if (direction == 240.0f) {
            if (IsOdd(row)) {
                col_buff -= 1;
                row_buff -= 1;
            } else {
                row_buff = -1;
            }
        } else if (direction == 300.0f) {
            if (IsOdd(row)) {
                row_buff -= 1;
            } else {
                col_buff += 1;
                row_buff = -1;
            }
        } else {
            Debug.Log(string.Format("Break", col_buff, row_buff));
        }
        return new Coordinate(col_buff, row_buff);
    }
    private Coordinate GetNeighbour_Tile_Odd(Coordinate coord, float direction) {

        int row_Val = coord.getRow;
        int col_buff = coord.getColumn;
        int row_buff = coord.getRow;
        if (direction == 00.0f) {
            col_buff = +1;

        } else if (direction == 60.0f) {
            if (IsOdd(row_Val)) {
                row_buff += 1;
            } else {
                col_buff += 1;
                row_buff += 1;
            }
        } else if (direction == 120.0f) {
            if (IsOdd(row_Val)) {
                col_buff -= 1;
                row_buff += 1;
            } else {
                row_buff += 1;
            }
        } else if (direction == 180.0f) {
            col_buff -= 1;
        } else if (direction == 240.0f) {
            if (IsOdd(row_Val)) {
                col_buff -= 1;
                row_buff -= 1;
            } else {
                row_buff = -1;
            }
        } else if (direction == 300.0f) {
            if (IsOdd(row_Val)) {
                row_buff -= 1;
            } else {
                col_buff += 1;
                row_buff = -1;
            }
        } else {
            Debug.Log(string.Format("Break", col_buff, row_buff));
        }
        return new Coordinate(col_buff, row_buff);
    }
}
