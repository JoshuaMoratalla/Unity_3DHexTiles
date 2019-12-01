using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //https://www.redblobgames.com/grids/hexagons/#pathfinding

    // Start is called before the first frame update
    public const float pan_speed = 20f;
    public const float rot_speed = 2.50f;

    private float camPosX_Min = -20; //relative to rows and columns of thingo
    
    Vector3 defParentPos = new Vector3(0.0f, 10.0f, -10.0f);//  snap to centre thing view facing z axis
    Vector3 defParentRot = new Vector3(0f, 0f, 0f);
    Vector3 defChildRot = new Vector3(45.0f, 0f, 0f);

    [SerializeField] public readonly float upward_limit = 20.0f;
    [SerializeField] public readonly float downward_limit = 90.0f;
    void Start() {
        //transform.position = transform.parent.position;

        SnapToCentre_View();
    }

    // Update is called once per frame
    void FixedUpdate(){

        if (Input.GetKey(KeyCode.LeftShift)) {
            Rotate_View();         
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            SnapToCentre_View();
        } else {
            Pan_View();         
        }

    }


    private void SnapToCentre_View() {
        transform.parent.position = defParentPos;
        transform.parent.eulerAngles = defParentRot;
        transform.eulerAngles = defChildRot;
    
    }

    private void Rotate_View() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {//don't tilt to far up

            if (transform.rotation.eulerAngles.x > upward_limit && -rot_speed + transform.rotation.eulerAngles.x > upward_limit) {
                transform.Rotate(-rot_speed, 0, 0, Space.Self);
            }

        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { //dont tilt too far down
            if (transform.rotation.eulerAngles.x < downward_limit && rot_speed + transform.rotation.eulerAngles.x < downward_limit) {
                transform.Rotate(rot_speed, 0, 0, Space.Self);
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            transform.parent.Rotate(0, -rot_speed, 0, Space.Self);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            transform.parent.Rotate(0, rot_speed, 0, Space.Self);
        }
    }

    private void Pan_View() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            transform.parent.Translate(new Vector3(0, 0, pan_speed) * Time.deltaTime, Space.Self);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            transform.parent.Translate(new Vector3(0, 0, -pan_speed) * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            transform.parent.Translate(new Vector3(-pan_speed, 0, 0) * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            transform.parent.Translate(new Vector3(pan_speed, 0, 0) * Time.deltaTime);
        }
    }
}
