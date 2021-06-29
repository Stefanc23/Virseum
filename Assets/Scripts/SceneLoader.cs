using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SceneLoader : MonoBehaviour
{
    public string aRModeSceneName;
    public string vRModeSceneName;
    
    public void LoadMainMenuScene() {
        StartCoroutine(SwitchTo2D());
    }

    public void LoadARModeScene() {
        SceneManager.LoadScene(aRModeSceneName);
    }
    
    public void LoadVRModeScene() {
        StartCoroutine(SwitchToVR());
    }

    public void Quit() {
        Application.Quit();
    }

    public IEnumerator SwitchToVR() {
        // Device names are lowercase, as returned by `XRSettings.supportedDevices`.
        string desiredDevice = "cardboard"; // Or "cardboard".

        // Some VR Devices do not support reloading when already active, see
        // https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
        if (String.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0) {
            XRSettings.LoadDeviceByName(desiredDevice);

            // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
            yield return null;
        }

        // Now it's ok to enable VR mode.
        XRSettings.enabled = true;
        
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(vRModeSceneName);
    }

    public IEnumerator SwitchTo2D() {
        // Empty string loads the "None" device.
        XRSettings.LoadDeviceByName("");
        
        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Not needed, since loading the None (`""`) device takes care of this.
        // XRSettings.enabled = false;

        // Restore 2D camera settings.
        ResetCameras();
        Screen.orientation = ScreenOrientation.AutoRotation;
        SceneManager.LoadScene(0);

    }

    // Resets camera transform and settings on all enabled eye cameras.
    private void ResetCameras() {
    // Camera looping logic copied from GvrEditorEmulator.cs
        for (int i = 0; i < Camera.allCameras.Length; i++) {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {

            // Reset local position.
            // Only required if you change the camera's local position while in 2D mode.
            cam.transform.localPosition = Vector3.zero;

            // Reset local rotation.
            // Only required if you change the camera's local rotation while in 2D mode.
            cam.transform.localRotation = Quaternion.identity;

            // No longer needed, see issue github.com/googlevr/gvr-unity-sdk/issues/628.
            // cam.ResetAspect();

            // No need to reset `fieldOfView`, since it's reset automatically.
            }
        }
    }
}
