using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARToolkitHelper {


	///
	/// Just open camera but do not preview
    public static void OpenCamera()
    {
    	/*
    	NOTE:
			There is 2 class : CameraHolderNoThread and CameraHolder
			CameraHolder: run camera with difference thread together with Unity and tracking thread
			CameraHolderNoThread : run camera in unity theard
			----------------------------------------
			To change class need to edit in UnitArPlayer > UnityARPlayerActivity.java
    	*/ 
        if (Application.platform != RuntimePlatform.Android) return;
        //NOTE: should be wrap in using {} to ensure object are deleted as soon as possible
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("CommandOpenCamera");
    }

    ///
    /// Release camera
    public static void CloseCamera()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("CommandCloseCamera");
    }

    /// ( need test and Fix )
    /// call camera.startPreview()
    public static void StartCapture()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("CommandStartCapture");
    }


    /// ( need test and Fix )
    /// call camera.stopPreview
    public static void StopCapture()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("CommandStopCapture");
    }

    /// ( need test and Fix )
    /// Set camera resolution ( not sure it working right now or not)
    public static void SetCameraResolution(int width = 480, int height = 320)
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("ConfigCameraResoulution", width, height);
    }

    /// ( need test and Fix )
    /// If you set camera resolution at very first time when app starting then you do not need
    /// to call this function. but when camera is opened and start capture. need to call Stopcapture and call this function to
    /// reapply camera config
    public static void ApplyConfig()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        cameraSurfaceClass.CallStatic("ApplyConfig");
    }

   	/// Check camera state is open
    public static bool IsCameraOpen()
    { 
        if (Application.platform != RuntimePlatform.Android) return false;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        return cameraSurfaceClass.CallStatic<bool>("IsCameraOpened");
    }

    /// check camera state is capturing.
    /// the main case using this is when Unity start. it call OnResume and OnPause two time that camera do not open.
    /// we check this to ensure that camera is open by user then if App turn from Pause > Resume depend on camera is on Capturing or not
    /// we re-open camera. ( but i do it in UnityARPlayer.jar already so this function just use to check. )
    public static bool IsCameraCapturing()
    {
        if (Application.platform != RuntimePlatform.Android) return false;
        AndroidJavaClass cameraSurfaceClass = new AndroidJavaClass("org.artoolkit.ar.unity.CameraHolderNoThread");
        return cameraSurfaceClass.CallStatic<bool>("IsCameraCapturing");
    }
}
