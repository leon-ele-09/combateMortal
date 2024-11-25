using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarJugadores : MonoBehaviour
{

    public GameObject GokuPlayer;
    public GameObject ToadPlayer;
    public GameObject WoodyPlayer;

    public bool GokuP;
    public bool ToadP;
    public bool WoodyP;



    // Start is called before the first frame update
    void Start()
    {
        bool gokuSelected = PlayerPrefs.GetInt("PlayerGoku", 0) == 1;
        bool toadSelected = PlayerPrefs.GetInt("PlayerToad", 1) == 1;
        bool woodySelected = PlayerPrefs.GetInt("PlayerWoody", 0) == 1;

        GokuPlayer.SetActive(gokuSelected);
        ToadPlayer.SetActive(toadSelected);
        WoodyPlayer.SetActive(woodySelected);

    }


}
