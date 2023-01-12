using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunController : MonoBehaviour
{
    public Main MainObj;

    // public GameObject popImgL;
    // public GameObject popImgR;


    public GameObject arrowObj;
    public Main mainObj;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        cam.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, cam.transform.localPosition.y, this.gameObject.transform.localPosition.z);

    }
    private void OnTriggerEnter(Collider other)
    {


        Debug.Log("충돌::"+other.gameObject.name);

        if (other.gameObject.name.Equals("CubeA"))        {
            MainObj.zoneStr = "A지역";
            
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else if (other.gameObject.name.Equals("CubeB"))
        {
            MainObj.zoneStr = "B지역";
            //mainObj.fArrowVal = 90f;
           
            
        }
        else if (other.gameObject.name.Equals("CubeC"))
        {
            MainObj.zoneStr = "C지역";
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            //mainObj.fArrowVal = 90f;

        }
        else if (other.gameObject.name.Equals("CubeD"))
        {
            MainObj.zoneStr = "D지역";
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
            //mainObj.fArrowVal = 90f;

        }
        else if (other.gameObject.name.Equals("CubeE"))
        {
            MainObj.zoneStr = "E지역";
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            //mainObj.fArrowVal = -90f;

        }
        else if (other.gameObject.name.Equals("CubeF"))
        {
            MainObj.zoneStr = "F지역";
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
            //mainObj.fArrowVal = 90f;

        }
        else if (other.gameObject.name.Equals("CubeG"))
        {
            MainObj.zoneStr = "G지역";
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            //mainObj.fArrowVal = 90f;
        }
        else if (other.gameObject.name.Equals("CubeEND"))
        {
            MainObj.zoneStr = "게임완료";
            MainObj.popGameEnd();
        }

        if (other.gameObject.tag.Equals("wallL"))
        {
            MainObj.LogCollisionWall();
        }
        else if (other.gameObject.tag.Equals("wallR"))
        {
            MainObj.LogCollisionWall();
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("wallL"))
        {
            MainObj.LogCollisionWallClear();
        }
        else if (other.gameObject.tag.Equals("wallR"))
        {
            MainObj.LogCollisionWallClear();
        }
    }

}
