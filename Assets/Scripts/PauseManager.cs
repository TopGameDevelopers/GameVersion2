using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
   public Camera MainCam;
   public Camera PauseCam;

   public void Unpause()
   {
      Time.timeScale = 1f;
      MainCam.gameObject.SetActive(true);
      PauseCam.gameObject.SetActive(false);
   }
}
