using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholManager : MonoBehaviour, IMinigameFunctionsInterface
{
    public List<AlcoholValues.Alcohol> alcohols;

    public Text text;
    public GameObject multa;
    public GameObject enemic;
    public Sprite[] enemicSprites;
    
    public int decrementAlcoholimetre = 5;
    public int maxCiutadans = 6;
    public const float MAX_ALCOHOL = 10f;

    private Singleton.Person newPerson;
    private float[] minMaxError = { 0.5f, 5f };
    private int numCiutada = 0;
    private int nRespostes;
    private float[] positions = { -11.43f, -3.12f };
    private int nEncerts = 0;
    private bool isDoingAnimation = false;
    private bool mentir = false;

    private List<Singleton.Person> peopleFinalMission = new List<Singleton.Person>();
    bool isFinalMission = false;

    void Start()
    {
        //mentir = Singleton.inst.Mentir();
        peopleFinalMission = Singleton.inst.GetListFinalMision();
        if(peopleFinalMission != null)
        {
            isFinalMission = true;
            Debug.Log("Final mission!!!!");
        }

        alcohols = Singleton.inst.GetAlcohols();
        nRespostes = maxCiutadans * 2; //Cada ciutadà pot mentir o beure de més
        text.text = "";
        GenerateNewPerson();
    }

    //Decisions interfícies
    #region
    //FER CONTROL
    public void FunctionAction1()
    {
        if(!isDoingAnimation)
        {
            // consumicions * quantitat d'alcohol per consumició * graus d'alcohol 
            float result = newPerson.quantity * newPerson.alcohol.quantity * newPerson.alcohol.degrees;
            float encert = Random.Range(0f, 100f);
            if (encert > Singleton.inst.GetFiabilitat())
            {
                Debug.Log("Error!");
                result += Random.Range(minMaxError[0], minMaxError[1]);
            }
            result = Mathf.Round(result * 100f) / 100f;
            text.text = "L'acoholímetre ha donat: " + result + "%";

            if(mentir) Singleton.inst.ChangeFiabilitat(-decrementAlcoholimetre);
        } 
    }

    //TORNAR A DEMANAR
    public void FunctionAction2()
    {
        if (!isDoingAnimation)
        {
            int quantity = (int)newPerson.quantity - newPerson.fakeValue;
            text.text = AlcoholText();
        }
    }

    //DEIXAR PASSAR
    public void FunctionAction3()
    {
        if(!isDoingAnimation) Multar(3);
    }

    //MULTAR
    public void FunctionAction4()
    {
        if(!isDoingAnimation) multa.SetActive(true);
    }
    #endregion

    //Funció que evalua la correctesa de la decisió del jugador.
    //Es valora que multi a la persona per les coses que ha fet malament i que no multi per una cosa que no ha fet
    public void Multar(int cas)
    {
        isDoingAnimation = true;
        multa.SetActive(false);
        float alcoholValue = newPerson.quantity * newPerson.alcohol.quantity * newPerson.alcohol.degrees;
        int nEncertsBefore = nEncerts;
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
        if (nEncertsBefore + 2 > nEncerts)
            Singleton.inst.AddPerson(newPerson);
        StartCoroutine(Move(0));
    }

    //Funció que genera nou perfil de persona segons si ha begut o no
    private void GenerateNewPerson()
    {
        isDoingAnimation = true;

        if (numCiutada < maxCiutadans)
        {
            if (isFinalMission) //S'agafa de la llista
            {
                newPerson = peopleFinalMission[numCiutada];
            }
            else //Es genera aleatòriament 
            {
                newPerson = new Singleton.Person();

                int i = Random.Range(0, alcohols.Count);
                newPerson.alcohol = alcohols[i];

                newPerson.quantity = Random.Range(0, 10);
                newPerson.fakeValue = mentir ? Random.Range(0, newPerson.quantity) : 0;
            }
            enemic.GetComponent<SpriteRenderer>().sprite = enemicSprites[Random.Range(0, 3)];
            text.text = AlcoholText();
        }
        else
        {
            if ( ((float)nEncerts) / nRespostes >= 0.85f) 
                Singleton.inst.AddAlcohol(AlcoholValues.GetRandomAlcohol());
            this.GetComponent<FinalMinigame>().Final(nEncerts, nRespostes);
        }
            //Debug.Log($"Acabat! Puntuació: {nEncerts}/{nRespostes}");
        numCiutada++;
        StartCoroutine(Move(1));
    }

    private string AlcoholText()
    {
        int quantity = (int)newPerson.quantity - newPerson.fakeValue;

        string res = "Bones! Jo";
        if (quantity == 0) res += " no he begut";
        else if (newPerson.alcohol.quantity == 0.33f) res += " he begut " + quantity + " ampolles de " + newPerson.alcohol.name;
        else if (newPerson.alcohol.quantity == 0.1f) res += " he begut " + quantity + " copes de " + newPerson.alcohol.name;
        else if (newPerson.alcohol.quantity == 0.05f) res += " he begut " + quantity + " combinats de " + newPerson.alcohol.name;
        else if (newPerson.alcohol.quantity == 0.03f) res += " he begut " + quantity + " xarrups de " + newPerson.alcohol.name;
        return res;
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
