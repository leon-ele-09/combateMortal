using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{

    public bool map3;
    public bool map2;
    public bool map1;

    public Image[] SelectionBoxes = new Image[2];
    
    private CharacterStats[] PlayerStats;

    private void Start()
    {
        int savedSelection = PlayerPrefs.GetInt("SelectedMap", 1);
        foreach (var img in this.SelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }

        selectMap(savedSelection);
    }
    private void Update()
    {

    }

    public void selectMap(int index)
    {
        foreach (var img in this.SelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        SelectionBoxes[index-1].gameObject.SetActive(true);

        PlayerPrefs.SetInt("SelectedMap", index);
        PlayerPrefs.Save();

        switch (index)
        {
            case 1:
                map1 = true;
                map2 = false;
                map3 = false;   
                break;

            case 2:
                map1 = false;
                map2 = true;
                map3 = false;
                break;
            case 3:
                map1 = false;
                map2 = false;
                map3 = true;
                break;

            default:
                break;
        }

        Guardar();
    }
    
    public void Guardar()
    {
        PlayerPrefs.SetInt("Mapa1", map1 ? 1 : 0);
        PlayerPrefs.SetInt("Mapa2", map2 ? 1 : 0);
        PlayerPrefs.SetInt("Mapa3", map3 ? 1 : 0);

        PlayerPrefs.Save();
    }

}