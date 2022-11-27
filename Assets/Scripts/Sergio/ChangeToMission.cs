using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMission : MonoBehaviour
{
    public enum Missions { alcohol, detenirBaralla }
    public Missions mission;

    public void ChangeTo()
    {
        switch(mission)
        {
            case Missions.alcohol:
                Debug.Log("Canvi a missió alcoholèmia");
                SceneManager.LoadScene("AlcoholScene");
                break;
            case Missions.detenirBaralla:
                Debug.Log("Canvi a missió detenir baralla");
                SceneManager.LoadScene("DetenirBarallaScene");
                break;
        }
    }
}
