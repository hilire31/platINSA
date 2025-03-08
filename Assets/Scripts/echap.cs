using UnityEngine;
using UnityEngine.SceneManagement;

public class echap : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Si l'utilisateur appuie sur Entrée
        {
            SceneManager.LoadScene(1); // Charge la scène 1
        }
    }


}