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

    private struct Person { public int quantity; public int fakeValue; };

    private AlcoholValues.Alcohol newPersonAlcohol;
    private Person newPerson;
    private float[] minMaxError = { 0.5f, 5f };
    private int numCiutada = 0;

    void Start()
    {
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
        // consumicions * graus d'alcohol * quantitat d'alcohol per consumició
        float result = (newPerson.quantity - newPerson.fakeValue) * newPersonAlcohol.quantity * newPersonAlcohol.degrees;
        float encert = Random.Range(0f, 100f);
        if (encert > fiabilitatAlcoholimetre)
        {
            Debug.Log("Error!");
            result += Random.Range(minMaxError[0], minMaxError[1]);
        }
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
        //Debug.Log("Deixar passar");
        multa.SetActive(false);
        GenerateNewPerson();
        //alcohols.Add(AlcoholValues.GetRandomAlcohol());
        //mostrarLlista();
    }

    //MULTAR
    public void FunctionAction4()
    {
        Debug.Log("Multar");
        multa.SetActive(true);

    }

    public void multar(int cas)
    {
        multa.SetActive(false);
        switch (cas)
        {
            case 0: Debug.Log("Mentira, mi vida es una mentira");  break;
            case 1: Debug.Log("Si saben como me pongo pa que me invitan");  break;
            case 2: Debug.Log("Tremendo maliante");  break;
        }
        GenerateNewPerson();
    }

    //Funció que genera nou perfil de persona segons si ha begut o no
    private void GenerateNewPerson()
    {
        numCiutada++;
        if (numCiutada < maxCiutadans)
        {
            int i = Random.Range(0, alcohols.Count);
            newPersonAlcohol = alcohols[i];

            newPerson = new Person();
            newPerson.quantity = Random.Range(0, 6);
            newPerson.fakeValue = mentir ? Random.Range(0, (int)Mathf.Min(newPersonAlcohol.quantity, 3)) : 0;

            int quantity = (int)newPerson.quantity - newPerson.fakeValue;
            text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
        }
        else
            Debug.Log("Acabat");
        
    }

    private void mostrarLlista()
    {
        foreach(AlcoholValues.Alcohol alcohol in alcohols)
            Debug.Log(alcohol.name);
    }
}
