using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex_SelectionManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Material highlight_Material;
    [SerializeField] private Material selected_Material;
    [SerializeField] public string selectable_Tag = "HexTiles";
    [SerializeField] private Material defaultMaterial;

    [SerializeField] public HexTile_Set tile_set; 

    // need to add a variable for most recent button/spell/action for the player to perform
    // aka press a to attack , then after clicks performs attack with inputs
    // same would also happen for spells and consumables


    // https://www.youtube.com/watch?v=_yf5vzZ2sYE

    private Tile_Values selected_Tile1 = null;
    private Tile_Values selected_Tile2 = null;
    private Tile_Values currentTile = null;
    private Tile_Values[] effectTiles = null;

    public GameObject FirstHex;
    public GameObject SecondHex;
    void Start() {
       //get the hex tile set to compare with

    }
    

    // Update is called once per frame
    void Update() {
        //SingleClickTest();
        DoubleClickTest();
       

        //maybe colored at end


    }



    public void SingleClickTest() {
        Ray cursor_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cursor_ray, out RaycastHit cursor_hit, Mathf.Infinity, LayerMask.GetMask(selectable_Tag))) {
            Tile_Values cursor_tile = cursor_hit.transform.GetComponent<Tile_Values>();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !cursor_tile.Compare_Tile(selected_Tile1)) {
                select_Tile(cursor_tile);

            } else if (!cursor_tile.Compare_Tile(currentTile)) {
                if (!cursor_tile.Compare_Tile(selected_Tile1)) {
                    highlight_Tile(cursor_tile);
                }


            }
        } else {
            if (currentTile != null) {
                currentTile.ChangeTo_DefaultMaterial();
            }
         
            currentTile = null;

            Debug.Log("Not poiting to anything");
            // }

        }

        ColorTiles();

        //DebugNullTiles();




    }


    public void DebugNullTiles() {

        string select;
        string current;
        if (selected_Tile1 == null) {
            select = "NULLSELECT";
        } else {
            select = string.Format("{0} | {1}", selected_Tile1.getTile_Column, selected_Tile1.getTile_Row);
        }

        if (currentTile == null) {
            current = "NULLCURRENT";
        } else {
            current = string.Format("{0} | {1}", currentTile.getTile_Column, currentTile.getTile_Row);
        }
        Debug.Log(string.Format("Status: Select - {0} | Current - {1}", select, current));
    }

    public void DoubleClickTest() {
        Ray cursor_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cursor_ray, out RaycastHit cursor_hit, Mathf.Infinity, LayerMask.GetMask(selectable_Tag))) {
            Tile_Values cursor_tile = cursor_hit.transform.GetComponent<Tile_Values>();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !cursor_tile.Compare_Tile(selected_Tile1)) {
                

                //two tiles

                //does regular operations on first click
                //on second click, must find if they are or are not linear 

                if (selected_Tile1 == null) {

                    select_Tile(cursor_tile);
              
                } else if (selected_Tile2 == null) {
                    
                   // List<Coordinate> line = 

                    //create a linear path based on selected Tile 1 and the new one
                    // may or may not need second selected tile as we can just use cursor tile again
                }


                // 

            } else if (!cursor_tile.Compare_Tile(currentTile)) {
                //if (!cursor_tile.Compare_Tile(selected_Tile1)) {
                //     highlight_Tile(cursor_tile);
                // }
                Debug.Log("In section");

                if (selected_Tile1 == null) {
                    //regular operations of highlighting the final tile
                    highlight_Tile(cursor_tile);
                    Debug.Log("Assign Current Tile");
                } else {
                    // effect tile will be done based on current action(right now testing for linear things on a selected tile)
                    //using both current and select tile
                    //create a line of hexes between current and selected based on the angle of the 60 degree segments

                    highlight_Tile(cursor_tile);
                    Vector3 directionVector = cursor_tile.transform.position - selected_Tile1.transform.position;
                    float directionAngle = Vector3.Angle(Vector3.right, directionVector);
                    Debug.Log("Angle between cursor and selected tile is :" + directionAngle);



                    /*
                     pi = 22.0/7.0
                    def eulerToDegree(euler):
                        return ( (euler) / (2 * pi) ) * 360
                     */


                    //secondary operations of creating a linear line from selected tile to current cursor tile
                    //snaps to linear depending on the angle
                    //needs to be close to a new angle to change aka threshold between snapping to next angle
                }

                //regular highlighting on tile
                // second one will perform a line from point a to b
                // must be linear so only color on change of 60 degree increment

            }



        } else {
            // if (currentTile != null) {
            currentTile.ChangeTo_DefaultMaterial();
            currentTile = null;

            Debug.Log("Not poiting to anything");
            // }

        }

        ColorTiles();

        string select;
        string current;

        if (selected_Tile1 == null) {
            select = "NULLSELECT";
        } else {
            select = string.Format("{0} | {1}", selected_Tile1.getTile_Column, selected_Tile1.getTile_Row);
        }

        if (currentTile == null) {
            current = "NULLCURRENT";
        } else {
            current = string.Format("{0} | {1}", currentTile.getTile_Column, currentTile.getTile_Row);
        }
        Debug.Log(string.Format("Status: Select - {0} | Current - {1}", select, current));


    }
    public void ColorTiles() {
        if (currentTile != null) {
            currentTile.ChangeMaterial(highlight_Material);
        } 
        if (selected_Tile1 != null) {
            selected_Tile1.ChangeMaterial(selected_Material);
        }
    }
    public void ClickPoints(GameObject firstpoint, GameObject secondpoint) {

        //https://docs.unity3d.com/ScriptReference/Vector3.Angle.html
        //Vector3 directionVector = secondpoint.transform.position - firstpoint.transform.position;
        // float directionAngle = Vector3.Angle( firstpoint.transform.position, secondpoint.transform.position);
        Vector3 directionVector = secondpoint.transform.position - firstpoint.transform.position;
        float directionAngle = Vector3.Angle(Vector3.right, directionVector);


        //make margin of error closer

        Debug.Log(directionAngle);
    }
    //maybe set default to highlight on deselect
    public void highlight_Tile(Tile_Values newTile) {
        if (currentTile != null) {
            if (currentTile.Compare_Tile(selected_Tile1)) {
                newTile.ChangeMaterial(selected_Material);
            } else {
                currentTile.ChangeTo_DefaultMaterial();
            }
           // newTile.ChangeMaterial(highlight_Material);
            currentTile = newTile;
        } else {
          //  newTile.ChangeMaterial(highlight_Material);
            currentTile = newTile;
        }
    }

    public void select_Tile(Tile_Values newTile) {
        if (selected_Tile1 != null) {
            selected_Tile1.ChangeTo_DefaultMaterial();

         //   newTile.ChangeMaterial(selected_Material);
            selected_Tile1 = newTile;

        } else {
          //  newTile.ChangeMaterial(selected_Material);
            selected_Tile1 = newTile;
        }
    }
}


/*
 * 
 * 
 * 
 *  if (selectedTile == null) {
            //Pressed mouse button
            if (Input.GetKeyDown(KeyCode.Mouse0)) { 
                Ray draw_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit draw_hit;
                if (Physics.Raycast(draw_ray, out draw_hit, Mathf.Infinity, LayerMask.GetMask(selectable_Tag))){
                    selectedTile = draw_hit.transform.GetComponent<Tile_Values>();
                    selectedTile.ChangeMaterial(selected_Material);
                }



                Debug.Log(string.Format("Tile: {0} | {1}", currentTile.GetComponent<Tile_Values>().getTile_Column, currentTile.GetComponent<Tile_Values>().getTile_Row));
                Hex_Shaper hexshape =this.GetComponent<Hex_Shaper>();
                
               
                
            } else { //have not pressed button and detecting new tiles across the place

                Ray draw_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit draw_hit;
                //Debug.Log(string.Format("Layer mask at value {0} is {1}",8, LayerMask.GetMask("HexTiles")));
                if (Physics.Raycast(draw_ray, out draw_hit,Mathf.Infinity,LayerMask.GetMask(selectable_Tag))) {
                    Tile_Values cursortile = draw_hit.transform.GetComponent<Tile_Values>();
                    if (selectedTile.Compare_Tile(cursortile)) {
                    
                    }

                } else {
                   // Debug.DrawRay(Camera.main.transform.position, draw_ray.direction, Color.red, 100000);
                    //Debug.DrawLine(Camera.main.transform.position, draw_hit.transform.position,Color.blue);
                    Debug.Log("Not pointing at anything");
                }

            }
            
        } else {
          //make shapes here
        }
 * 
 */
