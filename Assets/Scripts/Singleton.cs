using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    public static Singleton inst;

    public static int MAX_PAZ = 1000;
    private int pau;

    private int diners;

    //Int guarda la missio que s'ha de generar
    private List<KeyValuePair<Vector3, int>> missions = new List<KeyValuePair<Vector3, int>>();
    private List<bool> missionsPassed = new List<bool>();
    private Vector3 actualMission = new Vector3();


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

    public bool MissionsCreated()
    {
        return missions != null && missions.Count > 0;
    }

    public void AddMission(Vector3 mission, int type)
    {
        missions.Add(new KeyValuePair<Vector3, int>(mission, type));
        missionsPassed.Add(false);
    }

    public List<KeyValuePair<Vector3, int>> GetMissions()
    {
        return missions;
    }

    public void SetMission(Vector3 pos)
    {
        actualMission = pos;
    }

    public bool MissionPassed(Vector3 pos)
    {
        return missionsPassed[GetIndexByPos(pos)];
    }

    public void SetMissionPassed()
    {
        missionsPassed[GetIndexByPos(actualMission)] = true;
    }

    private int GetIndexByPos(Vector3 pos)
    {
        int i = 0;
        foreach(var kvp in missions)
        {
            if (kvp.Key == pos) break;
            i++;
        }
        return i;
    }

}
