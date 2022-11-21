using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMinigame : MonoBehaviour
{

    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    public int MAX_SCORE;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Final(int MAX_SCORE, int score)
    {
        foreach (GameObject objectToHide in objectsToHide)
        {
            objectToHide.gameObject.SetActive(false);
        }
        foreach (GameObject objectToShow in objectsToShow)
        {
            objectToShow.gameObject.SetActive(true);
        }

        this.MAX_SCORE = MAX_SCORE;
        this.score = score;
    }

}
