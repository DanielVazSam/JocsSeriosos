using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseCalculadora : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject calculadora;

    public void OpenClose()
    {
        if(calculadora.activeSelf) calculadora.SetActive(false);
        else calculadora.SetActive(true);
    }
}
