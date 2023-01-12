using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.IO;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public GameObject Runner;
    public int nSpeed;
    public int wallStat;
    
    float fDeg;
    // Start is called before the first frame update

    public SteamVR_Input_Sources handType;
    public SteamVR_Input_Sources handTypeR;
    public SteamVR_Input_Sources headH;
    public SteamVR_Input_Sources GamePad;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean grabActionR;
    public SteamVR_Action_Pose aPose;
    public SteamVR_Action_Boolean grabAction1;
    public SteamVR_Action_Boolean grabActionR1;

    public SteamVR_Action_Vector2 touchPadAction;

    private CVRSystem _vrSystem;

    private SteamVR_TrackedCamera.VideoStreamTexture tcam;

    private TrackedDevicePose_t[] _poses;
    private HmdMatrix34_t _pose;

    float preVal;
    float nextVal;


    public float leftAlertF;
    public float rightAlertF;
    public float forwardAlertF;
    public float backwardAlertF;


    public GameObject popImgL;
    public GameObject popImgR;
    public GameObject popImgF;
    public GameObject popImgB;

    public AudioSource leftSnd;
    public AudioSource rightSnd;
    public AudioSource forwardSnd;
    public AudioSource backwardSnd;

    public string zoneStr;

    public GameObject endBtn;

    string filePath;
    public Text posStr;

    public Text xStr1;
    public Text xStr2;
    public Text zStr1;
    public Text zStr2;

    public Text speedStr;

    public Transform miniCam;

    public string curZone;
    string strAlert;
    int isVisualOK;
    int isSoundOK;

    public float fArrowVal;
    public GameObject arrowObj;
    /* 
    void Awake()
     {
         var err = EVRInitError.None;
         _vrSystem = OpenVR.Init(ref err, EVRApplicationType.VRApplication_Other);
         if (err != EVRInitError.None)
         {
             // handle init error
         }
     }

 */
    FileStream fs;
    StreamWriter sw;

    void Start()
    {
        isVisualOK =1;
        isSoundOK =1;
        strAlert = "";

        filePath = Application.dataPath + "/log.txt";
        Debug.Log(filePath);

        fs = new FileStream(filePath, FileMode.Create);
        sw = new StreamWriter(fs);



        zoneStr = "";
        fDeg = 180f;
        wallStat = 0;
       
        _poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
        preVal = 0.0f;
        nextVal = 0.0f;

        fArrowVal = 0.0f;

        //var err = EVRInitError.None;
       // _vrSystem = OpenVR.Init(ref err, EVRApplicationType.VRApplication_Other);
    }


    float fYdegree = 180f;
    int fTemp = 0;
    int fNextTemp = -1;
    // Update is called once per frame
    void Update()
    {

        // Vector2 trackpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        // Debug.Log(trackpadValue);

        // if (CheckGrab())
        // {
        //     float touchpadInput = Input.GetAxis("ControllerTouchpad");
        //     Debug.Log(touchpadInput);

        //     if (touchpadInput > 0)
        //     {
        //               Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg++, 0f));
        //           }
        //           else if (touchpadInput < 0)
        //           {
        //            Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg--, 0f));
        //           }
        //           else
        //           {
        //            // The touchpad is not being pressed down
        //           }
        // }

        // if(CheckGrabR())
        // {
        //       float touchpadInput = Input.GetAxis("ControllerTouchpad");

        //           if (touchpadInput > 0)
        //           {
        //               Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg++, 0f));
        //           }
        //           else if (touchpadInput < 0)
        //           {
        //            Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg--, 0f));
        //           }
        //           else
        //           {
        //            // The touchpad is not being pressed down
        //           }
        // }

        // Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        // Debug.Log(touchpadValue);


        //GetDeviceToAbsoluteTrackingPose();
        
        if (CheckGrab())
        {
            //Debug.Log("GRAB ACTIONL");
            //Runner.transform.Translate(Vector3.forward * Time.deltaTime * nSpeed);
            Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg--, 0f));
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, fArrowVal++, 0f));
           
        }

        if(CheckGrabR())
        {
            Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg++, 0f));
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, fArrowVal--, 0f));
           
            //Debug.Log("GRAB ACTIONL R");


        }

        if (CheckGrab1())
        {
            //Debug.Log("GRAB ACTIONL");
            //Runner.transform.Translate(Vector3.forward * Time.deltaTime * nSpeed);
            Runner.transform.Translate(Vector3.forward  * Time.deltaTime * float.Parse(speedStr.text));
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, fArrowVal++, 0f));
           
        }

        if(CheckGrabR2())
        {
            Runner.transform.Translate(Vector3.forward  * Time.deltaTime * float.Parse(speedStr.text));
            //arrowObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, fArrowVal--, 0f));
           
            //Debug.Log("GRAB ACTIONL R");
        }


        if (wallStat == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Runner.transform.Translate(Vector3.forward * Time.deltaTime * nSpeed);
            }

            //this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg++, 0f));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Runner.transform.localRotation = Quaternion.Euler(new Vector3(0f, fDeg--, 0f));
            }

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Runner.transform.Translate(Vector3.back * Time.deltaTime * nSpeed);
        }


        // _vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0.0f, _poses);

        // preVal = _poses[0].mDeviceToAbsoluteTracking.m7;

        /*if (preVal!= _poses[0].mDeviceToAbsoluteTracking.m7)
        {
            Runner.transform.Translate(Vector3.forward * Time.deltaTime * nSpeed);
        }*/

        //Vector3 vv = aPose.GetLocalPosition(headH);
        //Debug.Log("vv:"+vv);  

        /*Debug.Log("m0:"+_poses[0].mDeviceToAbsoluteTracking.m0 + " m1:" + _poses[0].mDeviceToAbsoluteTracking.m1 + " m2:" + _poses[0].mDeviceToAbsoluteTracking.m2);

        Debug.Log("m3:"+_poses[0].mDeviceToAbsoluteTracking.m3 + " m4:" + _poses[0].mDeviceToAbsoluteTracking.m4 + " m5:" + _poses[0].mDeviceToAbsoluteTracking.m5);


        Debug.Log("m6:"+_poses[0].mDeviceToAbsoluteTracking.m6 + " m7:" + _poses[0].mDeviceToAbsoluteTracking.m7 + " m8:" + _poses[0].mDeviceToAbsoluteTracking.m8+" m9:" + _poses[0].mDeviceToAbsoluteTracking.m9);
        */

        
        nextVal = Mathf.Round(SteamVR._poses[0].mDeviceToAbsoluteTracking.m7 / .01f) * .01f ;

        fTemp = System.Convert.ToInt32(miniCam.localRotation.y);

        //Debug.Log(fTemp+" minicam: "+ miniCam.localEulerAngles.y);
        //Runner.transform.Rotate(0f, miniCam.localEulerAngles.y, 0f);
        //Vector3 pV1 = new Vector3(Runner.transform.localEulerAngles.x, Runner.transform.localEulerAngles.y + miniCam.localEulerAngles.y, miniCam.localEulerAngles.z);

        /*if (miniCam.localEulerAngles.y >= 60 && miniCam.localEulerAngles.y < 100)
        {
            Runner.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        }
        else if (miniCam.localEulerAngles.y >= -90 && miniCam.localEulerAngles.y <1 )
        {
            Runner.transform.localRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.left);
        }*/

        /*
        if (fTemp>fNextTemp)
        {
            //if(fTemp)
            // Runner.transform.Rotate(0f,fYdegree++, 0f);
            Debug.Log("left");
        }
        else
        {
            Debug.Log("right");
            //Runner.transform.Rotate(0f, fYdegree++, 0f);
        }*/

        //fNextTemp = fTemp;
        //Runner.transform.localRotation = Quaternion.EulerRotation(Runner.transform.localRotation.x, Runner.transform.localRotation.y+90f+miniCam.localRotation.y, Runner.transform.localRotation.z);
        //Vector3 MoveDir = Quaternion.Euler(0f, miniCam.localRotation.y, 0f);

        var quaternion = Quaternion.EulerRotation(Runner.transform.localEulerAngles.x, Runner.transform.localEulerAngles.y + miniCam.localEulerAngles.y, miniCam.localEulerAngles.z);

        Vector3 newDirection = quaternion * Vector3.forward;

        // if (preVal!= nextVal)
        // {
        //     Runner.transform.Translate(Vector3.forward  * Time.deltaTime * float.Parse(speedStr.text));
        
        // }



        sw.WriteLine(zoneStr + "," + SteamVR._poses[0].mDeviceToAbsoluteTracking.m3.ToString() + "," + SteamVR._poses[0].mDeviceToAbsoluteTracking.m7.ToString() + "," + SteamVR._poses[0].mDeviceToAbsoluteTracking.m11.ToString() + "," + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ",경고:" + strAlert + ",게임공간좌표:" + Runner.transform.localPosition.ToString());


        //Debug.Log("!m3:" +SteamVR._poses[0].mDeviceToAbsoluteTracking.m3 + "! m7:" + SteamVR._poses[0].mDeviceToAbsoluteTracking.m7 + "! m11:" + SteamVR._poses[0].mDeviceToAbsoluteTracking.m11);

        preVal = nextVal;

        posStr.text = "X:" + SteamVR._poses[0].mDeviceToAbsoluteTracking.m3 + ", Y: " + SteamVR._poses[0].mDeviceToAbsoluteTracking.m7 + ", Z: " + SteamVR._poses[0].mDeviceToAbsoluteTracking.m11;



        /*
         * 
         * 
          0 minicam: 351.4023 :f 320 ~44
          1 minicam: 96.21291 :r 45 - 135
         -1 minicam: 265.5309 :l 225- 319
          1 minicam: 175.502 :b :136-224
        *
        */


        //f 
        if (SteamVR._poses[0].mDeviceToAbsoluteTracking.m3 <= float.Parse(xStr1.text))
        {
            //if()
            if(miniCam.localEulerAngles.y>=320 && miniCam.localEulerAngles.y <= 360) //F
            {
                strAlert = "왼쪽";
                LeftAlert();
            }

            if (miniCam.localEulerAngles.y >= 0 && miniCam.localEulerAngles.y <= 44) //F
            {
                strAlert = "왼쪽";
                LeftAlert();
            }

            if (miniCam.localEulerAngles.y >= 45 && miniCam.localEulerAngles.y <= 135) //R
            {
                strAlert = "뒤쪽";
                backwardAlert();
            }

            if (miniCam.localEulerAngles.y >= 225 && miniCam.localEulerAngles.y <= 319)//L
            {
                strAlert = "앞쪽";
                forwardAlert();
            }
             if (miniCam.localEulerAngles.y >= 136 && miniCam.localEulerAngles.y <= 224)//B
            {
                strAlert = "오른쪽";
                rightAlert();
            }
            //Debug.Log("***>" + strAlert);
        }
        else if (SteamVR._poses[0].mDeviceToAbsoluteTracking.m3 >= float.Parse(xStr2.text)) 
        {



            if (miniCam.localEulerAngles.y >= 320 && miniCam.localEulerAngles.y <= 360) //F
            {
                strAlert = "오른쪽";
                rightAlert();
            }

            if (miniCam.localEulerAngles.y >= 0 && miniCam.localEulerAngles.y <= 44) //F
            {
                strAlert = "오른쪽";
                rightAlert();
            }

        
            if (miniCam.localEulerAngles.y >= 45 && miniCam.localEulerAngles.y <= 135) //R
            {
                strAlert = "앞쪽";
                forwardAlert();

            }

            if (miniCam.localEulerAngles.y >= 225 && miniCam.localEulerAngles.y <= 319)//L
            {

                strAlert = "뒤쪽";
                backwardAlert();
            }

            if (miniCam.localEulerAngles.y >= 136 && miniCam.localEulerAngles.y <= 224)//B
            {
                strAlert = "왼쪽";
                LeftAlert();
            }


        }
        else if (SteamVR._poses[0].mDeviceToAbsoluteTracking.m11 < float.Parse(zStr1.text))
        {


            if (miniCam.localEulerAngles.y >= 320 && miniCam.localEulerAngles.y <= 360) //F
            {
                strAlert = "앞쪽";
                forwardAlert();
            }

            if (miniCam.localEulerAngles.y >= 0 && miniCam.localEulerAngles.y <= 44) //F
            {
                strAlert = "앞쪽";
                forwardAlert();
            }

         
            if (miniCam.localEulerAngles.y >= 45 && miniCam.localEulerAngles.y <= 135) //R
            {
                strAlert = "왼쪽";
                LeftAlert();

            }

            if (miniCam.localEulerAngles.y >= 225 && miniCam.localEulerAngles.y <= 319)//L
            {

                strAlert = "오른쪽";
                rightAlert();
            }

            if (miniCam.localEulerAngles.y >= 136 && miniCam.localEulerAngles.y <= 224)//B
            {
                strAlert = "뒤쪽";
                backwardAlert();
            }




        }
        else if (SteamVR._poses[0].mDeviceToAbsoluteTracking.m11 > float.Parse(zStr2.text))
        {
            if (miniCam.localEulerAngles.y >= 320 && miniCam.localEulerAngles.y <= 360) //F
            {
                strAlert = "뒤쪽";
                backwardAlert();
            }

            if (miniCam.localEulerAngles.y >= 0 && miniCam.localEulerAngles.y <= 44) //F
            {
                strAlert = "뒤쪽";
                backwardAlert();
            }



            if (miniCam.localEulerAngles.y >= 45 && miniCam.localEulerAngles.y <= 135) //R
            {
                strAlert = "오른쪽";
                rightAlert();
            }

            if (miniCam.localEulerAngles.y >= 225 && miniCam.localEulerAngles.y <= 319)//L
            {
                strAlert = "왼쪽";
                LeftAlert();
            }

            if (miniCam.localEulerAngles.y >= 136 && miniCam.localEulerAngles.y <= 224)//B
            {
                strAlert = "앞쪽";
                forwardAlert();
            }
        }
        else
        {
            strAlert = "";
            hidePop();
        }

    }


    private bool CheckGrab()
    {
        
        return grabAction.GetState(handType);
    }

    private bool CheckGrabR()
    {
        
        return grabActionR.GetState(handTypeR);
    }

    private bool CheckGrab1()
    {
        
        return grabAction1.GetState(handType);
    }

    private bool CheckGrabR2()
    {
        
        return grabActionR1.GetState(handTypeR);
    }


    void LeftAlert()
    {
        hidePop();
        if (!popImgL.activeSelf)
        {
            if(isVisualOK==1)  popImgL.SetActive(true);

            if (isSoundOK==1)
            {
                if (!leftSnd.isPlaying) leftSnd.Play();
            }
        }
    }

    void rightAlert()
    {
        hidePop();
        if (!popImgR.activeSelf)
        {
            if (isVisualOK == 1) popImgR.SetActive(true);
            if (isSoundOK == 1)
            {
                if (!rightSnd.isPlaying) rightSnd.Play();
            }
        }
    }

    void forwardAlert()
    {
        hidePop();
        if (!popImgF.activeSelf)
        {
            if (isVisualOK == 1)  popImgF.SetActive(true);
            if (isSoundOK == 1)
            {
                if (!forwardSnd.isPlaying) forwardSnd.Play();
            }
        }
    }

    void backwardAlert()
    {
        hidePop();
        if (!popImgB.activeSelf)
        {
            if (isVisualOK == 1)  popImgB.SetActive(true);
            if (isSoundOK == 1)
            {
                if (!backwardSnd.isPlaying) backwardSnd.Play();
            }
        }
    }

    void hidePop()
    {
        popImgB.SetActive(false);
        popImgF.SetActive(false);
        popImgL.SetActive(false);
        popImgR.SetActive(false);
    }

    public void popGameEnd()
    {
        Debug.Log("popEnd");
        endBtn.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    string stempZone;


    //public void LogCollisionWall(int nC)

    public void LogCollisionWall()
    {
        stempZone = zoneStr;

        /*if (nC == 0)
        {
            zoneStr = zoneStr + "왼쪽벽 충돌";
        }
        else
        {
            zoneStr = zoneStr + "오른쪽벽 충돌";
        }*/

        zoneStr = zoneStr + " 벽 충돌";
    }

    public void LogCollisionWallClear()
    {


        /*if (nC == 0)
        {
            zoneStr = zoneStr + "왼쪽벽 충돌";
        }
        else
        {
            zoneStr = zoneStr + "오른쪽벽 충돌";
        }*/

        zoneStr = stempZone;
    }


    // shutdown
    /*void OnDestroy()
    {
        OpenVR.Shutdown();
    }
    */


    void OnApplicationQuit()
    {
        sw.Close();
        fs.Close();

        Debug.Log("Application ending after " + Time.time + " seconds");


    }

    

    public void toggle1()
    {
        isVisualOK =1;
        isSoundOK=1;
    }

    public void toggle2()
    {
        isVisualOK = 1;
        isSoundOK = 0;
    }


    public void toggle3()
    {
        isVisualOK = 0;
        isSoundOK = 1;
    }

    public void toggle4()
    {
        isVisualOK = 0;
        isSoundOK = 0;
    }




}
