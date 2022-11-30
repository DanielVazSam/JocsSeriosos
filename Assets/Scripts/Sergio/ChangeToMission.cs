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
        Singleton.inst.SetMission(this.transform.position);
        switch (mission)
        {
            case Missions.alcohol:
                SceneManager.LoadScene("AlcoholScene");
                break;
            case Missions.detenirBaralla:
                SceneManager.LoadScene("DetenirBarallaScene");
                break;
        }
    }
}
