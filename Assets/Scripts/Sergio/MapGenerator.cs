using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Quantes missions es desitjen crear
    public int missionsToSpawn;
    public GameObject canvas;
    //Lista de prefabs dels botons a generar
    public GameObject[] buttonMisions;
    //Possibles posicions dels botons
    Vector3[] positions = { new Vector3(-110, -67, 0), 
                            new Vector3(438, 57, 0), 
                            new Vector3(-686, -351, 0), 
                            new Vector3(-521, -77, 0), 
                            new Vector3(133, -351, 0), 
                            new Vector3(617, -327, 0), 
                            new Vector3(782, -67, 0), 
                            new Vector3(710, 362, 0), 
                            new Vector3(148, 261, 0), 
                            new Vector3(-603.5f, 215, 0), 
                            new Vector3(-243, 395, 0) };
    bool[] used;
    // Start is called before the first frame update
    void Start()
    {
        used = new bool[positions.Length];
        for(int i = 0; i < positions.Length; i++) used[i] = false;

        missionsToSpawn = Mathf.Clamp(missionsToSpawn, 0, positions.Length);
        for(int i = 0; i < missionsToSpawn; i++)
        {
            int positionPos;

            do positionPos = Random.Range(0, positions.Length);
            while (used[positionPos]);

            int missionPos = Random.Range(0, buttonMisions.Length);

            GameObject aux = Instantiate(buttonMisions[missionPos], canvas.transform);
            aux.transform.localPosition = positions[positionPos];
            used[positionPos] = true;
        }
    }
}
