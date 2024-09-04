using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuMain;
    public GameObject levelsPanel;

    private void Start()
    {
        // Vérifiez si PlayerPrefs indique qu'on vient d'un Game Over
        if (PlayerPrefs.GetInt("FromGameOver", 0) == 1)
        {
            // Activez le Levels Panel et désactivez le Menu Main
            if (menuMain != null) menuMain.SetActive(false);
            if (levelsPanel != null) levelsPanel.SetActive(true);
            
            // Réinitialisez la valeur pour ne pas activer le Levels Panel à chaque chargement
            PlayerPrefs.SetInt("FromGameOver", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // Assurez-vous que le Menu Main est actif et le Levels Panel désactivé
            if (menuMain != null) menuMain.SetActive(true);
            if (levelsPanel != null) levelsPanel.SetActive(false);
        }
    }
}
