using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using Valve.VR;

public class TrackingHMD : MonoBehaviour
{
    private CVRSystem _vrSystem;
    private TrackedDevicePose_t[] _poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];

    // initialize
    void Awake()
    {
        var err = EVRInitError.None;
        _vrSystem = OpenVR.Init(ref err, EVRApplicationType.VRApplication_Other);
        if (err != EVRInitError.None)
        {
            // handle init error
        }
    }

    // get tracked device poses
    void Update()
    {
        // get the poses of all tracked devices
        _vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0.0f, _poses);

        // send the poses to SteamVR_TrackedObject components
        //SteamVR_Events.NewPoses.Send(_poses);

        Debug.Log(_poses[0].mDeviceToAbsoluteTracking.m0+"--"+ _poses[0].mDeviceToAbsoluteTracking.m4+"--"+ _poses[0].mDeviceToAbsoluteTracking.m8);
        


    }

    // shutdown
    void OnDestroy()
    {
        OpenVR.Shutdown();
    }
}
