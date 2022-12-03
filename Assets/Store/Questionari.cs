using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questionari : MonoBehaviour
{

    public Toggle[] toggles;
    public GameObject questions;
    public Button[] buttons; //0 -> enviar, 1 -> reparar
    public OpenCloseQuestions scriptOpenClose;

    private bool[] values;
    private List<bool> answers = new List<bool>();
    private const int MAX_PRICE = 200;
    private int repairPrice;


    // Start is called before the first frame update
    void Start()
    {
        values = new bool[10];
        GenerateQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Repair()
    {
        if (repairPrice <= Singleton.inst.GetDiners())
        {
            Singleton.inst.RepairAlcoholimeter(repairPrice);
            scriptOpenClose.CloseQuestions();
        }
    }

    public void GetAllValues()
    {
        int corrects = 0;
        for(int i = 0; i < 10; i++) {
            values[i] = toggles[i].isOn;
            Transform toggleQuestion = questions.transform.GetChild(i).GetChild(2);
            toggleQuestion.GetComponent<Toggle>().interactable = false;
            toggleQuestion.GetChild(0).GetComponent<Image>().color = values[i] == answers[i] ? Color.green : Color.red;
            if(values[i] == answers[i]) corrects++;
        }
        buttons[0].gameObject.SetActive(false);
        repairPrice = (int)(MAX_PRICE - 100 * (corrects / 10f));
        buttons[1].transform.GetChild(0).GetComponent<Text>().text = "Reparar (" + repairPrice.ToString() + ")";
        buttons[1].gameObject.SetActive(true);
        Debug.Log($"L'usuari ha fet {corrects}/10 bé");
    }

    public void GenerateQuestions()
    {
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(false);

        List<AlcoholValues.Alcohol> alcohols = Singleton.inst.GetAlcohols();
        answers = new List<bool>();
        foreach(Transform t in questions.transform)
        {
            int index = Random.Range(0, alcohols.Count);
            t.GetChild(0).GetComponent<Text>().text = alcohols[index].name;

            int[] randomValue = { 0, Random.Range(-5, 5), Random.Range(-5, 5) };
            int randomDegrees = alcohols[index].degrees + randomValue[Random.Range(0,3)];
            string degrees = randomDegrees.ToString() + "%";

            answers.Add(randomDegrees == alcohols[index].degrees);

            t.GetChild(1).GetComponent<Text>().text = degrees;

            t.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
            t.GetChild(2).GetComponent<Toggle>().interactable = true;
            t.GetChild(2).GetComponent<Toggle>().isOn = true;
        }
    }

}
