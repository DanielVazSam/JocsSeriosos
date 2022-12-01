using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    //Quantes missions es desitjen crear
    private int missionsToSpawn = Singleton.N_MISSIONS;
    public GameObject canvas;
    //Lista de prefabs dels botons a generar
    public GameObject[] buttonMisions;
    //Possibles posicions dels botons
    Vector3[] positions = { new Vector3(-110, -67, 0), 
                            new Vector3(438, 57, 0), 
                            new Vector3(-686, -316, 0), 
                            new Vector3(-521, -77, 0), 
                            new Vector3(133, -351, 0), 
                            new Vector3(617, -327, 0), 
                            new Vector3(782, -67, 0), 
                            new Vector3(710, 362, 0), 
                            new Vector3(148, 261, 0), 
                            new Vector3(-603.5f, 215, 0), 
                            new Vector3(-243, 357, 0) };
    bool[] used;
    private Color passed = new Color(0.08254716f, 0.5f, 0.1183335f);
    // Start is called before the first frame update
    void Start()
    {
        if(!Singleton.inst.MissionsCreated())
        {
            used = new bool[positions.Length];
            for (int i = 0; i < positions.Length; i++) used[i] = false;

            missionsToSpawn = Mathf.Clamp(missionsToSpawn, 0, positions.Length);
            for (int i = 0; i < missionsToSpawn; i++)
            {
                int positionPos;

                do positionPos = Random.Range(0, positions.Length);
                while (used[positionPos]);

                int missionPos = Random.Range(0, buttonMisions.Length);

                GameObject aux = Instantiate(buttonMisions[missionPos], canvas.transform);
                aux.transform.localPosition = positions[positionPos];
                used[positionPos] = true;

                Singleton.inst.AddMission(aux.transform.position, missionPos);
            }
        }
        else
        {
            if (Singleton.inst.GetListFinalMision() != null)
            {
                GameObject go = Instantiate(buttonMisions[0], canvas.transform);
            }
            else
            {
                List<KeyValuePair<Vector3, int>> aux = Singleton.inst.GetMissions();
                int i = 0;
                foreach (var mis in aux)
                {
                    GameObject go = Instantiate(buttonMisions[mis.Value], canvas.transform);
                    go.transform.position = mis.Key;
                    if (Singleton.inst.MissionPassed(mis.Key))
                    {
                        go.GetComponent<Image>().color = passed;
                        go.GetComponent<Button>().interactable = false;
                    }
                    i++;
                }
            }
        }
        
    }
}
