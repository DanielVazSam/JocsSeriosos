using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton inst;

    public static int MAX_PAZ = 1000;
    private int pau;

    private int diners;

    //Int guarda la missio que s'ha de generar (0 alcohol, 1 baralla)
    private List<KeyValuePair<Vector3, int>> missions;
    private List<bool> missionsPassed;
    private Vector3 actualMission; 
    private int nMissionsPassed;

    public struct Person { public int quantity; public int fakeValue; public AlcoholValues.Alcohol alcohol; };
    public static int N_MISSIONS = 8;

    private int fiabilitatAlcoholimetre;
    private List<Person> peopleFailed;
    private List<AlcoholValues.Alcohol> alcohols;


    private void Awake()
    {
        if(Singleton.inst == null)
        {
            Singleton.inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        pau = 0;
        diners = 0;

        missions = new List<KeyValuePair<Vector3, int>>();
        missionsPassed = new List<bool>();
        actualMission = new Vector3();
        nMissionsPassed = 0;

        fiabilitatAlcoholimetre = 100;
        peopleFailed = new List<Person>();
        alcohols = new List<AlcoholValues.Alcohol> {
            AlcoholValues.Cerveza,
            AlcoholValues.Vino,
            AlcoholValues.Ron,
            AlcoholValues.Tequila 
        };
    }

    void Update()
    {
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

    //Mission generator Functions
    #region
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
        if(nMissionsPassed < N_MISSIONS)
        {
            missionsPassed[GetIndexByPos(actualMission)] = true;
            nMissionsPassed++;
        }
    }
    #endregion

    //Alcohol 
    #region
    public void AddPerson(Person person) 
    { 
        peopleFailed.Add(person);
    }

    public List<Person> GetListFinalMision()
    {
        if(nMissionsPassed == N_MISSIONS) return peopleFailed;
        else return null;
    }
    public List<AlcoholValues.Alcohol> GetAlcohols()
    {
        return alcohols;
    }
    public void AddAlcohol(AlcoholValues.Alcohol newAlcohol)
    {
        alcohols.Add(newAlcohol);
    }
    public int GetFiabilitat()
    {
        return fiabilitatAlcoholimetre;
    }
    public void ChangeFiabilitat(int newFiabilitat)
    {
        fiabilitatAlcoholimetre = newFiabilitat > 0 ? newFiabilitat : fiabilitatAlcoholimetre + newFiabilitat;
    }
    #endregion

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
