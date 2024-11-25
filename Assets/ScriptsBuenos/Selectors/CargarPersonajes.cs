using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPersonajes : MonoBehaviour
{


    public GameObject GokuNPC;
    public GameObject ToadNPC;
    public GameObject WoodyNPC;


    public bool Goku;
    public bool Toad;
    public bool Woody;

    // Start is called before the first frame update
    void Start()
    {

        bool gokuSelected = PlayerPrefs.GetInt("EnemyGoku", 0) == 1;
        bool toadSelected = PlayerPrefs.GetInt("EnemyToad", 1) == 1;
        bool woodySelected = PlayerPrefs.GetInt("EnemyWoody", 0) == 1;

        GokuNPC.SetActive(gokuSelected);
        ToadNPC.SetActive(toadSelected);
        WoodyNPC.SetActive(woodySelected);

    }
}
