using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    public static Singleton inst;

    public static int MAX_PAZ = 1000;
    private int pau;

    private int diners;


    private void Awake()
    {
        if(Singleton.inst == null)
        {
            Singleton.inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        pau = 0;
        diners = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        // Debug.Log("Pau = " + pau);

        if(Input.GetKeyDown(KeyCode.P))
        {
            AddPau(10);
            Debug.Log("Pau = " + pau);
        }
    }


    public int GetPau()
    {
        return pau;
    }

    public void AddPau(int plus)
    {
        pau += plus;
    }

    public int GetDiners()
    {
        return diners;
    }

    public void AddDiners(int plus)
    {
        diners += plus;
    }

}
