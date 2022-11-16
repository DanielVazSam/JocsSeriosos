using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholManager : MonoBehaviour, IMinigameFunctionsInterface
{
    public AlcoholValues.Alcohol[] alcohols = new AlcoholValues.Alcohol[] { 
        AlcoholValues.Cerveza, 
        AlcoholValues.Vino, 
        AlcoholValues.Ron, 
        AlcoholValues.Tequila };

    public Text text;
    public bool mentir = false;

    

    private struct Person { public int quantity; public int fakeValue; };

    private AlcoholValues.Alcohol newPersonAlcohol;
    private Person newPerson;

    void Start()
    {
        text.text = "";
        GenerateNewPerson();
    }

    //FER CONTROL
    public void FunctionAction1()
    {
        Debug.Log("Fer control");
        text.text = "L'acoholímetre ha donat: " + newPersonAlcohol.quantity;
    }

    //TORNAR A DEMANAR
    public void FunctionAction2()
    {
        Debug.Log("");
        int quantity = (int)newPerson.quantity - newPerson.fakeValue;
        text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
    }

    //DEIXAR PASSAR
    public void FunctionAction3()
    {
        Debug.Log("Deixar passar");
        GenerateNewPerson();
    }

    //MULTAR
    public void FunctionAction4()
    {
        Debug.Log("Multar");
    }

    private void GenerateNewPerson()
    {
        int i = Random.Range(0, alcohols.Length);
        newPersonAlcohol = alcohols[i];

        newPerson = new Person();
        newPerson.quantity = Random.Range(0, 6);
        newPerson.fakeValue = mentir ? Random.Range(0, (int)Mathf.Min(newPersonAlcohol.quantity, 3)) : 0;

        int quantity = (int)newPerson.quantity - newPerson.fakeValue;
        text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
    }
}
