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


    public void Final(int score, int MAX_SCORE)
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

        this.MAX_SCORE = MAX_SCORE;
        this.score = score;

        scoreText.text = score.ToString();
        pauText.text = "9999";
        dinersText.text = "9999";
    }

}
