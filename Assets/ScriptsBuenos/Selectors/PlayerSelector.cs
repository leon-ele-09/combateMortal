using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{

    public bool GokuPlayer;
    public bool ToadPlayer;
    public bool WoodyPlayer;

    public Image[] SelectionBoxes = new Image[3];
    public GameObject[] Prefabs = new GameObject[3];
    private CharacterStats[] PlayerStats;

    private void Start()
    {
        int savedSelection = PlayerPrefs.GetInt("SelectedPlayer", 1);
        foreach (var img in this.SelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }

        selectPlayer(savedSelection);
    }
    private void Update()
    {

    }

    public void selectPlayer(int index)
    {
        foreach (var img in this.SelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        SelectionBoxes[index].gameObject.SetActive(true);

        PlayerPrefs.SetInt("SelectedPlayer", index);
        PlayerPrefs.Save();

        switch (index)
        {
            case 0:
                PersonajeGokuPlayer();
                break;
            case 1:
                PersonajeToadPlayer();
                break;

            case 2:
                PersonajeWoodyPlayer();
                break;

            default:
                PersonajeGokuPlayer();
                break;
        }


    }
    public void PersonajeGokuPlayer()
    {
        GokuPlayer = true;
        ToadPlayer = false;
        WoodyPlayer = false;
        Guardar();
    }

    public void PersonajeToadPlayer()
    {
        GokuPlayer = false;
        ToadPlayer = true;
        WoodyPlayer = false;
        Guardar();
    }

    public void PersonajeWoodyPlayer()
    {
        GokuPlayer = false;
        ToadPlayer = false;
        WoodyPlayer = true;
        Guardar();
    }

    public void Guardar()
    {
        PlayerPrefs.SetInt("PlayerGoku", GokuPlayer ? 1 : 0);
        PlayerPrefs.SetInt("PlayerToad", ToadPlayer ? 1 : 0);
        PlayerPrefs.SetInt("PlayerWoody", WoodyPlayer ? 1 : 0);
        PlayerPrefs.Save();
    }

}