using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalMinigame : MonoBehaviour
{

    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    public Text scoreText;
    public Text pauText;
    public Text dinersText;

    public string title = "Minigame Name";

    private int MAX_SCORE;
    private int score;

    private int pau;
    private int diners;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Final(int s, int MS)
    {
        foreach (GameObject objectToHide in objectsToHide)
        {
            objectToHide.gameObject.SetActive(false);
        }
        foreach (GameObject objectToShow in objectsToShow)
        {
            objectToShow.gameObject.SetActive(true);
        }

        GameObject.Find("FinalMinigameTitle").GetComponent<Text>().text = title;

        MAX_SCORE = MS;
        if(s < 0) score = 0;
        else score = s;


        Debug.Log("score = " + score);
        Debug.Log("MAX_SCORE = " + MAX_SCORE);


        int prop = 100 * score / MAX_SCORE;
        int rangePau = Random.Range(4, 8);
        int rangeDiners = Random.Range(1, 3);


        int newPau = prop * rangePau;
        int newDiners = prop * rangeDiners;


        if(Singleton.inst != null)
        {
            Singleton.inst.AddPau(newPau);
            Singleton.inst.AddDiners(newDiners);
            Singleton.inst.SetMissionPassed();
        }


        scoreText.text = prop.ToString();
        pauText.text = newPau.ToString();
        dinersText.text = newDiners.ToString();
    }

}
