using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamerManager : MonoBehaviour
{
    public static GamerManager instance;

    [SerializeField]
    //public int ondeEstou;
    public bool jogoComecou;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Carrega(Scene cena, LoadSceneMode modo) 
    { 
   
            StarGame();
    }

    void StarGame() //me serve pra nada por enquanto
    {
   
        UIManager.instance.StartUI();
        jogoComecou = true;
    }
}
