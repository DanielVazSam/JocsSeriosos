using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholManager : MonoBehaviour, IMinigameFunctionsInterface
{
    public List<AlcoholValues.Alcohol> alcohols = new List<AlcoholValues.Alcohol> { 
        AlcoholValues.Cerveza, 
        AlcoholValues.Vino, 
        AlcoholValues.Ron, 
        AlcoholValues.Tequila };

    public Text text;
    public GameObject multa;
    public bool mentir = false;
    public static float fiabilitatAlcoholimetre = 100;
    public float decrementAlcoholimetre = 5;
    public int maxCiutadans = 6;
    public const float MAX_ALCOHOL = 10f;

    private struct Person { public int quantity; public int fakeValue; };

    private AlcoholValues.Alcohol newPersonAlcohol;
    private Person newPerson;
    private float[] minMaxError = { 0.5f, 5f };
    private int numCiutada = 0;
    private int nRespostes;
    private int nEncerts = 0;

    void Start()
    {
        nRespostes = maxCiutadans * 2; //Cada ciutadà pot mentir o beure de més
        text.text = "";
        GenerateNewPerson();
    }

    public static void RepararAlcholimetre()
    {
        fiabilitatAlcoholimetre = 100;
    }

    //FER CONTROL
    public void FunctionAction1()
    {
                      // consumicions * quantitat d'alcohol per consumició * graus d'alcohol 
        float result = newPerson.quantity * newPersonAlcohol.quantity * newPersonAlcohol.degrees;
        float encert = Random.Range(0f, 100f);
        if (encert > fiabilitatAlcoholimetre)
        {
            Debug.Log("Error!");
            result += Random.Range(minMaxError[0], minMaxError[1]);
        }
        result = Mathf.Round(result * 100f) / 100f;
        text.text = "L'acoholímetre ha donat: " + result + "%";

        fiabilitatAlcoholimetre -= decrementAlcoholimetre;
    }

    //TORNAR A DEMANAR
    public void FunctionAction2()
    {
        int quantity = (int)newPerson.quantity - newPerson.fakeValue;
        text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
    }

    //DEIXAR PASSAR
    public void FunctionAction3()
    {
        multar(3);
    }

    //MULTAR
    public void FunctionAction4()
    {
        multa.SetActive(true);
    }

    //Funció que evalua la correctesa de la decisió del jugador.
    //Es valora que multi a la persona per les coses que ha fet malament i que no multi per una cosa que no ha fet
    public void multar(int cas)
    {
        multa.SetActive(false);
        float alcoholValue = newPerson.quantity * newPersonAlcohol.quantity * newPersonAlcohol.degrees;
        switch (cas)
        {
            case 0: //Mentida
                if (newPerson.fakeValue > 0) nEncerts++;
                if (alcoholValue <= MAX_ALCOHOL) nEncerts++;
                break;
            case 1: //Excés
                if (newPerson.fakeValue == 0) nEncerts++;
                if (alcoholValue > MAX_ALCOHOL) nEncerts++; 
                break;
            case 2: //Ambdues
                if (newPerson.fakeValue > 0) nEncerts++;
                if (alcoholValue > MAX_ALCOHOL) nEncerts++;
                break;
            case 3: //Cap
                if (newPerson.fakeValue == 0) nEncerts++;
                if (alcoholValue <= MAX_ALCOHOL) nEncerts++;
                break;
        }
        GenerateNewPerson();
    }

    //Funció que genera nou perfil de persona segons si ha begut o no
    private void GenerateNewPerson()
    {
        
        if (numCiutada < maxCiutadans)
        {
            int i = Random.Range(0, alcohols.Count);
            newPersonAlcohol = alcohols[i];

            newPerson = new Person();
            newPerson.quantity = Random.Range(0, 10);
            newPerson.fakeValue = mentir ? Random.Range(0, newPerson.quantity) : 0;
            if(newPerson.fakeValue > 0) Debug.Log("Mentida! Ha restat " + newPerson.fakeValue);

            int quantity = (int)newPerson.quantity - newPerson.fakeValue;
            text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
        }
        else
            Debug.Log($"Acabat! Puntuació: {nEncerts}/{nRespostes}");
        numCiutada++;

    }

    private void mostrarLlista()
    {
        foreach(AlcoholValues.Alcohol alcohol in alcohols)
            Debug.Log(alcohol.name);
    }
}
