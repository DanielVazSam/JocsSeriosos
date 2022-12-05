using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{

    [SerializeField] GameObject introduction;
    [SerializeField] GameObject storeTutorial;
    [SerializeField] GameObject barallaTutorial;
    [SerializeField] GameObject alcoholTutorial;


    private void Start()
    {
        if (introduction != null) { 
            introduction.SetActive(Singleton.inst.GetIsIntroduction());
        }
        else if (storeTutorial != null)
        {
            storeTutorial.SetActive(Singleton.inst.GetIsStoreTutorial());
        }
        else if (barallaTutorial != null)
        {
            barallaTutorial.SetActive(Singleton.inst.GetIsBarallaTutorial());
        }
        else if (alcoholTutorial != null)
        {
            alcoholTutorial.SetActive(Singleton.inst.GetIsAlcoholTutorial());
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

    public void CloseStoreTutorial()
    {
        Singleton.inst.SetIsStoreTutorial(false);
        storeTutorial.SetActive(false);
    }

    public void CloseBarallaTutorial()
    {
        Singleton.inst.SetIsBarallaTutorial(false);
        barallaTutorial.SetActive(false);
    }

    public void CloseAlcoholTutorial()
    {
        Singleton.inst.SetIsAlcoholTutorial(false);
        alcoholTutorial.SetActive(false);
    }

}
