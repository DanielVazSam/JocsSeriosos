using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameInterface : MonoBehaviour
{

    public GameObject action1;
    public GameObject action2;
    public GameObject action3;
    public GameObject action4;

    public string action1Text = "Action1Text";
    public string action2Text = "Action2Text";
    public string action3Text = "Action3Text";
    public string action4Text = "Action4Text";

    private IMinigameFunctionsInterface actionFunctions;



    // Start is called before the first frame update
    void Start()
    {
        action1 = GameObject.Find("Action1");
        action2 = GameObject.Find("Action2");
        action3 = GameObject.Find("Action3");
        action4 = GameObject.Find("Action4");

        action1.GetComponentInChildren<Text>().text = action1Text;
        action2.GetComponentInChildren<Text>().text = action2Text;
        action3.GetComponentInChildren<Text>().text = action3Text;
        action4.GetComponentInChildren<Text>().text = action4Text;

        actionFunctions = GameObject.Find("PutHereFunctionsScript").GetComponentInChildren<IMinigameFunctionsInterface>();
        action1.GetComponentInChildren<Button>().onClick.AddListener(delegate { actionFunctions.FunctionAction1(); });
        action2.GetComponentInChildren<Button>().onClick.AddListener(delegate { actionFunctions.FunctionAction2(); });
        action3.GetComponentInChildren<Button>().onClick.AddListener(delegate { actionFunctions.FunctionAction3(); });
        action4.GetComponentInChildren<Button>().onClick.AddListener(delegate { actionFunctions.FunctionAction4(); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
