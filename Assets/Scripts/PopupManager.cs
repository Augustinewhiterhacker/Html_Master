using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    public GameObject popUpPrefab;
    public GameObject canvasObject;
    
    // Variables publiques pour définir le texte dans l'inspecteur
    public string popUpName;
    public string popUpDescription;

    public void Start()
    {
        ShowPopup();
    }

    // Appelez cette méthode pour créer un popup avec le texte défini dans l'inspecteur
    public void ShowPopup()
    {
        CreatePopUp(popUpName, popUpDescription);
    }

    private void CreatePopUp(string name, string description)
    {
        GameObject createdPopUpObject = Instantiate(popUpPrefab, canvasObject.transform);
        createdPopUpObject.GetComponent<Popup>().setPopUpDescription(description);
        createdPopUpObject.GetComponent<Popup>().setPopUpName(name);
        MovePopUp(createdPopUpObject);
    }

    private void MovePopUp(GameObject createdPopUpObject)
    {
        createdPopUpObject.GetComponent<RectTransform>().DOAnchorPosY(-250, 3f)
            .OnComplete(() => Destroy(createdPopUpObject));
    }
}
