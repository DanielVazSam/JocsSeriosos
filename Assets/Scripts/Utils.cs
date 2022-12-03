using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{

    [SerializeField] GameObject introduction;


    private void Start()
    {
        if (introduction != null) { 
            introduction.SetActive(Singleton.inst.GetIsIntroduction());
        }
    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseIntroduction()
    {
        Singleton.inst.SetIsIntroduction(false);
        introduction.SetActive(false);
    }

}
