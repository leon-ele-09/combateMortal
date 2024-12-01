using UnityEngine;
using UnityEngine.UI;

public class EnemySelector : MonoBehaviour
{

    public bool Goku;
    public bool Toad;
    public bool Woody;

    public Image[] enemySelectionBoxes = new Image[3];
    public GameObject[] enemyPrefabs = new GameObject[3];
    private CharacterStats[] enemyStats;

    private void Start()
    {
        int savedSelection = PlayerPrefs.GetInt("SelectedEnemy", 1);

        foreach (var img in this.enemySelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }

        selectEnemy(savedSelection);
    }
    private void Update()
    {

    }

    public void selectEnemy(int index)
    {
        foreach (var img in this.enemySelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        enemySelectionBoxes[index].gameObject.SetActive(true);

        PlayerPrefs.SetInt("SelectedEnemy", index);
        PlayerPrefs.Save();

        switch (index)
        {
            case 0:
                PersonajeGoku();
                break;
            case 1:
                PersonajeToad();
                break;

            case 2:
                PersonajeWoody();
                break;

            default:
                PersonajeGoku();
                break;
        }


    }
    public void PersonajeGoku()
    {
        Goku = true;
        Toad = false;
        Woody = false;
        Guardar();
    }

    public void PersonajeToad()
    {
        Goku = false;
        Toad = true;
        Woody = false;
        Guardar();
    }

    public void PersonajeWoody()
    {
        Goku = false;
        Toad = false;
        Woody = true;
        Guardar();
    }

    public void Guardar()
    {
        PlayerPrefs.SetInt("EnemyGoku", Goku ? 1 : 0);
        PlayerPrefs.SetInt("EnemyToad", Toad ? 1 : 0);
        PlayerPrefs.SetInt("EnemyWoody", Woody ? 1 : 0);
        PlayerPrefs.Save();
    }

}