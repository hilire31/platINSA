using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    void Start()
    {
        Invoke("GoToNextScene", 25f); // Après 25 secondes, changer de scène
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Si l'utilisateur appuie sur Entrée
        {
            SceneManager.LoadScene(1); // Charge la scène 1
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(1); // Charge la scène 1 après 25 secondes
    }
}