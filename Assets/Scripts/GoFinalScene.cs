using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFinalScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (Singleton.inst.GetPau() >= Singleton.MAX_PAU)
        {
            // Debug.Log("SE ACABO, MI PANA");
            Utils.LoadScene("FinalScene");
        }
    }
}
