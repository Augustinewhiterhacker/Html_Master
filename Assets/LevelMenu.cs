using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    // Tableau contenant les noms des scènes, dans l'ordre de déverrouillage
    public string[] sceneNames;

    private void Awake()
    {
        // Initialisation du tableau avec les noms des scènes
        sceneNames = new string[] { "Main Menu", "Level1", "Level2", "Level3", "Level4", "Level5", "Quizz" };

        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        // Utiliser le nom de la scène basé sur l'index
        if (levelId >= 0 && levelId < sceneNames.Length)
        {
            string sceneName = sceneNames[levelId];
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("ID de niveau invalide ou scène non trouvée.");
        }
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
