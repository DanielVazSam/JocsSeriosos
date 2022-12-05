using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalReturnMapFunction : MonoBehaviour
{
    public void ReturnMap()
    {
        // Debug.Log("Return Map");
        SceneManager.LoadScene("MapTest");
    }
}
