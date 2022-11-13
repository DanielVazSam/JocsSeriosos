using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetenirBarallaFunctions : MonoBehaviour, IMinigameFunctionsInterface
{

    private static string COLPEJAR = "Colpejar";
    private static string ESQUIVAR = "Esquivar";

    public const int VIDA_MAX = 100;
    public int vidaEnemy;
    public int vida;

    public string enemyAction;

    public Text text;
    public Image enemyProgressBar;
    public Image progressBar;


    // Start is called before the first frame update
    void Start()
    {
        vida = VIDA_MAX;
        vidaEnemy = VIDA_MAX;
        enemyAction = "Idle";
        text.text = "";
        progressBar.fillAmount = 1;
        enemyProgressBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifeProgressBars();
    }


    // COLPEJAR
    public void FunctionAction1()
    {
        enemyAction = DoEnemyAction();
        Debug.Log("enemyAction = " + enemyAction);
        if(enemyAction.Equals(COLPEJAR))
        {
            string dialogue = "L'enemic ha atacat, però tu també.\nL'enemic ha rebut un cop.";
            text.text = dialogue;
            vidaEnemy -= 20;
        }
        else if(enemyAction.Equals(ESQUIVAR))
        {
            string dialogue = "L'enemic ha esquivat el teu atac i ha contratacat.";
            text.text = dialogue;
            vida -= 20;
        }
    }

    // PARAR COP
    public void FunctionAction2()
    {
        enemyAction = DoEnemyAction();
        Debug.Log("enemyAction = " + enemyAction); 
        if (enemyAction.Equals(COLPEJAR))
        {
            // vidaEnemy -= 20;
        }
    }

    // REDUIR
    public void FunctionAction3()
    {
        Debug.Log("Reduir");
    }

    // ESPOSAR
    public void FunctionAction4()
    {
        Debug.Log("Esposar");
    }


    private string DoEnemyAction()
    {
        int value = Random.Range(1, 3);
        string response = "Idle";
        if(value == 1)
        {
            response = COLPEJAR;
        }
        else if(value == 2)
        {
            response = ESQUIVAR;
        }
        return response;
    }


    private void UpdateLifeProgressBars()
    {
        progressBar.fillAmount = (1.0f * vida / VIDA_MAX);
        enemyProgressBar.fillAmount = (1.0f * vidaEnemy / VIDA_MAX);
    }

}
