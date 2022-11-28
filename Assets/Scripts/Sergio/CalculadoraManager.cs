using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculadoraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject calculadora;

    public InputField quantitatInput;
    public InputField grausInput;
    public Dropdown tipusDropdown;
    public InputField resultatText;

    private const int AMPOLLA = 0;
    private const int COPA = 1;
    private const int COMBINAT = 2;
    private const int XARRUP = 3;

    public void OpenClose()
    {
        if(calculadora.activeSelf) calculadora.SetActive(false);
        else calculadora.SetActive(true);
    }

    public void Calcular()
    {
        if (quantitatInput.text == "") Debug.Log("Falta quantitat");
        else if (grausInput.text == "") Debug.Log("Falten graus");
        else
        {
            int quant = int.Parse(quantitatInput.text);
            float graus = float.Parse(grausInput.text);
            float mesuraBeguda = 0.33f;
            switch(tipusDropdown.value)
            {
                case AMPOLLA: mesuraBeguda = 0.33f; break;
                case COPA: mesuraBeguda = 0.1f; break;
                case COMBINAT: mesuraBeguda = 0.05f; break;
                case XARRUP: mesuraBeguda = 0.03f; break;
            }
            Debug.Log($"Quantitat: {quant}, Graus: {graus}, Mesura: {mesuraBeguda}");
            float res = quant * graus * mesuraBeguda;
            resultatText.text = (res.ToString() + "%");
        }
        
    }
}
