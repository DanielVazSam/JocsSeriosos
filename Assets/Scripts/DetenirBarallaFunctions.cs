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

    public BarallaSpriteController police;
    public BarallaSpriteController enemy;

    private bool isReduced = false;
    private bool isHandcuff = false;


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

        /*
        if (vida <= 0)
        {
            police.SetReduced();
        }
        */
    }

    private void UpdateLifeProgressBars()
    {
        progressBar.fillAmount = (1.0f * vida / VIDA_MAX);
        enemyProgressBar.fillAmount = (1.0f * vidaEnemy / VIDA_MAX);

        if (vida <= 0)
        {
            // Debug.Log("bro estas muertisimo");
            // transform.gameObject.GetComponent<FinalMinigame>().Final(vida, VIDA_MAX);

            string dialogue = "L'enemic t'ha esgotat. Has perdut...";
            text.text = dialogue;
            police.SetReduced();

            StartCoroutine(GoFinalScore());
        }
    }


    // COLPEJAR
    public void FunctionAction1()
    {
        if (!isHandcuff && vida > 0)
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
                        enemy.SetReduced();
                    }
                    else
                    {
                        StartCoroutine(SetPoliceAttacked());
                        StartCoroutine(SetEnemyHurted());
                    }
                    text.text = dialogue;
                }
                else if (enemyAction.Equals(ESQUIVAR))
                {
                    string dialogue = "L'enemic ha esquivat el teu atac i ha contratacat.";
                    text.text = dialogue;
                    vida -= 15;
                    StartCoroutine(SetPoliceHurted());
                    StartCoroutine(SetEnemyAttacked());
                }
            }
            else
            {
                string dialogue = "L'enemic estava reduit i tu has atacat a l'aire.\n" +
                    "Enhorabona, has deixat que faci la migdiada i es recuperi.";
                text.text = dialogue;
                SetEnemyReducedFalse();
                StartCoroutine(SetPoliceAttacked());
                enemy.SetIdle();
            }
            UpdateLifeProgressBars();
        }
    }

    // PARAR COP
    public void FunctionAction2()
    {
        if (!isHandcuff && vida > 0)
        {
            if (!isReduced)
            {
                enemyAction = DoEnemyAction();
                if (enemyAction.Equals(COLPEJAR))
                {
                    string dialogue = "L'enemic ha atacat, però has detingut el cop.";
                    text.text = dialogue;
                    if(vida > 0) police.SetIdle();
                    StartCoroutine(SetEnemyAttacked());
                }
                else if (enemyAction.Equals(ESQUIVAR))
                {
                    string dialogue = "Tots dos estàveu esperant un cop.\nUs heu quedat mirant-vos com dos idiotes.\n" +
                        "Us ha donat temps de recuperar-vos una mica.";
                    vida += 10;
                    if (vida > 100)
                        vida = 100;
                    vidaEnemy += 10;
                    if (vidaEnemy > 100)
                        vidaEnemy = 100;
                    text.text = dialogue;
                    if (vida > 0) police.SetIdle();
                    enemy.SetIdle();
                }
            }
            else
            {
                string dialogue = "L'enemic estava reduit i tu has defensat. Ja no sé ni què dir-te...\n" +
                    "L'enemic s'ha recuperat i s'ha aixecat.";
                text.text = dialogue;
                SetEnemyReducedFalse();
                if (vida > 0) police.SetIdle();
                enemy.SetIdle();
            }
            UpdateLifeProgressBars();
        }
    }

    // REDUIR
    public void FunctionAction3()
    {
        if (!isHandcuff && vida > 0)
        {
            if (!isReduced)
            {
                float random = Random.Range(0, VIDA_MAX);
                if (random > vidaEnemy)
                {
                    string dialogue = "Has aconseguit reduir l'enemic. Ràpid, esposa'l.";
                    text.text = dialogue;
                    isReduced = true;
                    StartCoroutine(SetPoliceAttacked());
                    enemy.SetReduced();
                }
                else
                {
                    string dialogue = "Has intentat reduir l'enemic, però no has pogut.\nT'has emportat un cop de regal.";
                    text.text = dialogue;
                    vida -= 15;
                    StartCoroutine(SetPoliceHurted());
                    StartCoroutine(SetEnemyAttacked());
                }
            }
            else
            {
                string dialogue = "Has re-reduit l'enemic. Guay...";
                text.text = dialogue;
            }
            UpdateLifeProgressBars();
        }
    }

    // ESPOSAR
    public void FunctionAction4()
    {
        if (!isHandcuff && vida > 0)
        {
            float random = Random.Range(0, 100);
            if (!isReduced)
            {
                string dialogue = "L'enemic encara no estava reduit";
                if (random < 5)
                {
                    dialogue += ", pero has tingut sort i l'has aconseguit esposar.\nJA ÉS TEU!";
                    text.text = dialogue;
                    isHandcuff = true;
                    if (vida > 0) police.SetIdle();
                    enemy.SetReduced();
                }
                else
                {
                    dialogue += ", així que obviament no l'has aconseguit esposar.\nT'enportes un cop de regal.";
                    text.text = dialogue;
                    vida -= 15;
                    StartCoroutine(SetPoliceHurted());
                    StartCoroutine(SetEnemyAttacked());
                }
            }
            else
            {
                if (random < 81)
                {
                    string dialogue = "Has aconseguit esposar l'enemic. ENHORABONA!";
                    text.text = dialogue;
                    isHandcuff = true;
                }
                else
                {
                    string dialogue = "L'enemic estava reduit, però no l'has esposat. Ja s'ha de tenir mala sort...\n" +
                        "L'enemic s'ha aixecat de nou.";
                    text.text = dialogue;
                    if (vidaEnemy <= 0)
                        vidaEnemy += 10;
                    isReduced = false;
                    if (vida > 0) police.SetIdle();
                    enemy.SetIdle();
                }
            }
            UpdateLifeProgressBars();

            if (isHandcuff)
            {
                StartCoroutine(GoFinalScore());
            }
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


    IEnumerator GoFinalScore()
    {
        yield return new WaitForSeconds(2);
        transform.gameObject.GetComponent<FinalMinigame>().Final(vida, VIDA_MAX);
    }



    IEnumerator SetPoliceHurted()
    {
        if (vida > 0)
        {
            police.SetHurt();
            yield return new WaitForSeconds(0.5f);
            if (vida > 0) police.SetIdle();
        }
    }

    IEnumerator SetPoliceAttacked()
    {
        if (vida > 0)
        {
            police.SetAttack();
            yield return new WaitForSeconds(0.5f);
            if (vida > 0) police.SetIdle();
        }
    }

    IEnumerator SetEnemyHurted()
    {
        enemy.SetHurt();
        yield return new WaitForSeconds(0.5f);
        enemy.SetIdle();
    }

    IEnumerator SetEnemyAttacked()
    {
        enemy.SetAttack();
        yield return new WaitForSeconds(0.5f);
        enemy.SetIdle();
    }


}
