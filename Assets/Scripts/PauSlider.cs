using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauSlider : MonoBehaviour
{

    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        // slider.value = 0;
        slider.value = 1.0f * Singleton.inst.GetPau() / Singleton.MAX_PAU;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        slider.value = 1.0f * Singleton.inst.GetPau() / Singleton.MAX_PAU;
    }
    */
}
