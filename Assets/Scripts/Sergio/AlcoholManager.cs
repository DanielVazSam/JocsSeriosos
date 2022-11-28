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
    public GameObject enemic;
    public Sprite[] enemicSprites;
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
    private float[] positions = { -11.43f, -3.12f };
    private int nEncerts = 0;
    private bool isDoingAnimation = false;

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
        if(!isDoingAnimation)
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
    }

    //TORNAR A DEMANAR
    public void FunctionAction2()
    {
        if (!isDoingAnimation)
        {
            int quantity = (int)newPerson.quantity - newPerson.fakeValue;
            text.text = quantity == 0 ? "Bones! Jo no he begut" : "Bones! Jo he begut " + quantity + " de " + newPersonAlcohol.name;
        }
    }

    //DEIXAR PASSAR
    public void FunctionAction3()
    {
        if(!isDoingAnimation) multar(3);
    }

    //MULTAR
    public void FunctionAction4()
    {
        if(!isDoingAnimation) multa.SetActive(true);
    }

    //Funció que evalua la correctesa de la decisió del jugador.
    //Es valora que multi a la persona per les coses que ha fet malament i que no multi per una cosa que no ha fet
    public void multar(int cas)
    {
        isDoingAnimation = true;
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
        StartCoroutine(Move(0));
    }

    //Funció que genera nou perfil de persona segons si ha begut o no
    private void GenerateNewPerson()
    {
        isDoingAnimation = true;
        if (numCiutada < maxCiutadans)
        {
            enemic.GetComponent<SpriteRenderer>().sprite = enemicSprites[Random.Range(0, 3)];

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
        StartCoroutine(Move(1));
    }

    //Esquerra: 0, Dreta: 1
    private IEnumerator Move(int axis)
    {
        if(axis == 0) enemic.GetComponent<SpriteRenderer>().flipX = true;
        else enemic.GetComponent<SpriteRenderer>().flipX = false;
        while((axis == 0 && enemic.transform.position.x > positions[axis]) || (axis == 1 && enemic.transform.position.x < positions[axis]))
        {
            float val = axis == 0 ? -0.1f : 0.1f;
            enemic.transform.position += new Vector3(val, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        if(axis == 0) GenerateNewPerson();
        else isDoingAnimation = false;
    }

    private void mostrarLlista()
    {
        foreach(AlcoholValues.Alcohol alcohol in alcohols)
            Debug.Log(alcohol.name);
    }
}
