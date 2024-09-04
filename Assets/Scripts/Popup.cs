using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
   public TMP_Text popUpNameText;
   public TMP_Text popUpDescriptionText;

   public void setPopUpName(string text)
   {
    popUpNameText.text = text;
   }

   public void setPopUpDescription(string text)
   {
    popUpDescriptionText.text = text;
   }
}
