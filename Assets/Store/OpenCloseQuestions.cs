using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseQuestions : MonoBehaviour
{

    public GameObject questions;


    // Start is called before the first frame update
    void Start()
    {
        CloseQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenQuestions()
    {
        questions.GetComponent<Questionari>().GenerateQuestions();
        questions.SetActive(true);
    }

    public void CloseQuestions()
    {
        questions.SetActive(false);
    }


}
