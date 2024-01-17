using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ModelSwitcher modelSwitcher; // store ModelSwitcher script
    private static readonly int CameraIndex = Animator.StringToHash("cameraIndex");

    public void SwitchCamera(String clickedTag)
    {
        if (Camera.main != null)
        {
            Animator cameraAnim = Camera.main.GetComponent<Animator>();
            string[] currTags  = modelSwitcher.GetCurrentTags();
            if (clickedTag == currTags[0])
            { // if clicked tag is deck
                cameraAnim.SetInteger(CameraIndex, 0); // set camera to main camera
            }
            else if (clickedTag == currTags[1])
            { // if clicked tag is wheels
                cameraAnim.SetInteger(CameraIndex, 1); // set camera to wheels
            }
            else
            {
                cameraAnim.SetInteger(CameraIndex, 0); // reset camera to main camera
            }
        }
    }
}
