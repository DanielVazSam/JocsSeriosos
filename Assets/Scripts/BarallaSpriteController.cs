using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarallaSpriteController : MonoBehaviour
{

    public Sprite idle;
    public Sprite attack;
    public Sprite hurt;
    public Sprite reduced;

    public SpriteRenderer current;


    // Start is called before the first frame update
    void Start()
    {
        current.sprite = idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdle()
    {
        current.sprite = idle;
    }
    public void SetAttack()
    {
        current.sprite = attack;
    }
    public void SetHurt()
    {
        current.sprite = hurt;
    }
    public void SetReduced()
    {
        current.sprite = reduced;
    }
}
