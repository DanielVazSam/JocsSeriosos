using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMission : MonoBehaviour
{
    public enum Missions { alcohol, detenirBaralla}
    public Missions mission;

    public void ChangeTo()
    {
        switch(mission)
        {
            case Missions.alcohol:
                Debug.Log("Canvi a missió alcoholèmia");
                break;
            case Missions.detenirBaralla:
                Debug.Log("Canvi a missió detenir baralla");
                break;
        }
    }
}
