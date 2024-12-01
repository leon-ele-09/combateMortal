using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditarTextos : MonoBehaviour
{

    public TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Start()
    {
        
        texto.text = PlayerPrefs.GetInt("Player1Points", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
