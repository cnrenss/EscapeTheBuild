using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startgame : MonoBehaviour
{
    public Button ilkoyunbasla;
    // Start is called before the first frame update
    void Start()
    {
        ilkoyunbasla.onClick.AddListener(oyunac);
    }

    public void oyunac()
    {
        SceneManager.LoadScene("the last revelation");
    }
   
}
