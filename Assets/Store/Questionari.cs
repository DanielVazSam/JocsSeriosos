using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questionari : MonoBehaviour
{

    public Toggle[] toggles;

    private bool[] values;


    // Start is called before the first frame update
    void Start()
    {
        values = new bool[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetAllValues()
    {
        for(int i = 0; i < 10; i++) {
            values[i] = toggles[i].isOn;
        }

        Debug.Log(values[0] + ", " +
            values[1] + ", " +
            values[2] + ", " +
            values[3] + ", " +
            values[4] + ", " +
            values[5] + ", " +
            values[6] + ", " +
            values[7] + ", " +
            values[8] + ", " +
            values[9] 
        );
    }

}
