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

    private bool isReduced = false;


    // Start is called before the first frame update
    void Start()
    {
        vida = VIDA_MAX;
        vidaEnemy = VIDA_MAX;
        enemyAction = "Idle";
        text.text = "";
        progressBar.fillAmount = 1;
        enemyProgressBar.fillAmount = 1;
        UpdateLifeProgressBars();
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateLifeProgressBars();
    }

    private void UpdateLifeProgressBars()
    {
        /*
        if (vida > 0)
        {*/
            progressBar.fillAmount = (1.0f * vida / VIDA_MAX);
            enemyProgressBar.fillAmount = (1.0f * vidaEnemy / VIDA_MAX);
        // }
        if (vida <= 0)
        {
            Debug.Log("bro estas muertisimo");
        }
    }


    // COLPEJAR
    public void FunctionAction1()
    {
        if (!isReduced)
        {
            enemyAction = DoEnemyAction();
            if (enemyAction.Equals(COLPEJAR))
            {
                string dialogue = "L'enemic ha atacat, però tu també.\nL'enemic ha rebut un cop.";
                vidaEnemy -= 10;
                if (vidaEnemy <= 0)
                {
                    dialogue += "\nHas cansat l'enemic i l'has aconseguit reduir. Ràpid, esposa'l.";
                    isReduced = true;
                }
                text.text = dialogue;
            }
            else if (enemyAction.Equals(ESQUIVAR))
            {
                string dialogue = "L'enemic ha esquivat el teu atac i ha contratacat.";
                text.text = dialogue;
                vida -= 15;
            }
        }
        else 
        {
            string dialogue = "L'enemic estava reduit i tu has atacat a l'aire.\n" +
                "Enhorabona, has deixat que faci la migdiada i es recuperi.";
            text.text = dialogue;
            SetEnemyReducedFalse();
        }
        UpdateLifeProgressBars();
    }

    // PARAR COP
    public void FunctionAction2()
    {
        if (!isReduced)
        {
            enemyAction = DoEnemyAction();
            if (enemyAction.Equals(COLPEJAR))
            {
                string dialogue = "L'enemic ha atacat, però has detingut el cop.";
                text.text = dialogue;
            }
            else if (enemyAction.Equals(ESQUIVAR))
            {
                string dialogue = "Tots dos estàveu esperant un cop.\nUs heu quedat mirant-vos com dos idiotes.\n" +
                    "Us ha donat temps de recuperar-vos una mica.";
                vida += 10;
                vidaEnemy += 10;
                text.text = dialogue;
            }
        }
        else
        {
            string dialogue = "L'enemic estava reduit i tu has defensat. Ja no sé ni què dir-te...\n" +
                "L'enemic s'ha recuperat i s'ha aixecat.";
            text.text = dialogue;
            SetEnemyReducedFalse();
        }
        UpdateLifeProgressBars();
    }

    // REDUIR
    public void FunctionAction3()
    {
        if (!isReduced)
        {
            float random = Random.Range(0, VIDA_MAX);
            if (random > vidaEnemy)
            {
                string dialogue = "Has aconseguit reduir l'enemic. Ràpid, esposa'l.";
                text.text = dialogue;
                isReduced = true;
            }
            else
            {
                string dialogue = "Has intentat reduir l'enemic, però no has pogut.\nT'has emportat un cop de regal.";
                text.text = dialogue;
                vida -= 15;
            }
        }
        else
        {
            string dialogue = "Has re-reduit l'enemic. Guay...";
            text.text = dialogue;
        }
        UpdateLifeProgressBars();
    }

    // ESPOSAR
    public void FunctionAction4()
    {
        bool handcuff = false;
        float random = Random.Range(0, 100);
        if (!isReduced)
        {
            string dialogue = "L'enemic encara no estava reduit";
            if(random < 6)
            {
                dialogue += ", pero has tingut sort i l'has aconseguit esposar.\nJA ÉS TEU!";
                text.text = dialogue;
                handcuff = true;
            }
            else
            {
                dialogue += ", així que obviament no l'has aconseguit esposar.\nT'enportes un cop de regal.";
                text.text = dialogue;
                vida -= 15;
            }
        }
        else
        {
            if (random < 81)
            {
                string dialogue = "Has aconseguit esposar l'enemic. ENHORABONA!";
                text.text = dialogue;
                handcuff = true;
            }
            else
            {
                string dialogue = "L'enemic estava reduit, però no l'has esposat. Ja s'ha de tenir mala sort...\n" +
                    "L'enemic s'ha aixecat de nou.";
                text.text = dialogue;
                isReduced = false;
            }
        }
        UpdateLifeProgressBars();

        if(handcuff)
        {

        }
    }


    private string DoEnemyAction()
    {
        int value = Random.Range(0, 3);
        string response = "Idle";
        if(value == 1 || value == 0)
        {
            response = COLPEJAR;
        }
        else if(value == 2)
        {
            response = ESQUIVAR;
        }
        return response;
    }


    private void SetEnemyReducedFalse()
    {
        isReduced = false;
        if (vidaEnemy <= 0) vidaEnemy = 20;
        else if (vidaEnemy > 80) vidaEnemy = 100;
        else vidaEnemy += 20;
    }


}
