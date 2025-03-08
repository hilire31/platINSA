using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScorescene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Si l'utilisateur appuie sur Entrée
        {
            SceneManager.LoadScene(0); // Charge la scène 1
        }
    }
}
