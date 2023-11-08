using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject placementCam;
    public int manager = 0;

    public void ChangeCamera() {
        GetComponent<Animator>().SetTrigger("Change");
    }


    public void ManageCameras() {
        if(manager == 0) {
            ActivePlacementCam();
            manager = 1;
        }
        else {
            ActivePlayerCam();
            manager = 0;
        }
    }

    public void ActivePlayerCam() {
        placementCam.SetActive(false);
        playerCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ActivePlacementCam() {
        playerCam.SetActive(false);
        placementCam.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}