using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Autor(a); Luana
//Data: 13/07/2020
//Titulo: Controle do jogo

public class ControllerGame : MonoBehaviour
{
 

    public int score; //estava como private
    public int scorePresentes;

    public Text txtScore;
    public Text txtScorePresentes;

    public int totalScore; //estava como private qualquer coisa trocar
    public int totalScorePresentes;

    public static ControllerGame instance;

    //preciso associar o objeto para guardar a animaçãoda explosao
    public GameObject hitPrefabs; //vai guardar o prefab de explosao
    //public GameObject hitPrefabsParticula;
    //variavel para as vidas
    public Sprite[] imagensVidas;
    public Image barraVida;


    public AudioSource fxGame;
    public AudioClip fxMelColetado;
    public AudioClip fxExplosao; //variavel guarda som de explosao
    public AudioClip fxDie;      //variavel guarda som player morto

    public GameObject gameOver;
    public GameObject gameEnd;


    /* [SerializeField]
     public int ondeEstou;
     public bool jogoComecou;*/
    //private GamerManager _GamerManager;

    void Start()
    {
        instance =this;

        totalScore = PlayerPrefs.GetInt("Score", score);
        Debug.Log(PlayerPrefs.GetInt("Score"));

        totalScorePresentes = PlayerPrefs.GetInt("ScorePresentes", scorePresentes);
        Debug.Log(PlayerPrefs.GetInt("ScorePresentes"));

        
        ApagarChaves();//apaga o que foi gravado
       // LerPontos();

    }


    public void Pontuacao(int qtdPontos)
    {
        
       score += qtdPontos;
       txtScore.text = score.ToString();

        fxGame.PlayOneShot(fxMelColetado);


       totalScore++;
       PlayerPrefs.SetInt("Score", totalScore); //grava os pontos

    }

  //Pontuação do Presente
  public void PontuacaoPresentes(int qdtPontosP)
    {
        scorePresentes += qdtPontosP;
        txtScorePresentes.text = scorePresentes.ToString();

        totalScorePresentes++;
        PlayerPrefs.SetInt("ScorePresentes", totalScorePresentes);
    }


    public void BarraVida(int healthvida)
    {
        barraVida.sprite = imagensVidas[healthvida]; // 3 2 1 0 vidas
    }

    void LerPontos()
       {

           txtScore.text = PlayerPrefs.GetInt("Score").ToString();
           txtScorePresentes.text = PlayerPrefs.GetInt("ScorePresentes").ToString();


       }

       void ApagarChaves()
       {
           PlayerPrefs.DeleteKey("Score");
           PlayerPrefs.DeleteKey("ScorePresentes");
       }

    public void NextScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
    public void RestartGame(string lvlName)
    {
       
        SceneManager.LoadScene(lvlName);
    }

    public void ShowGameEnd()
    {
      
        gameEnd.SetActive(true);
    }
    public void ExitGame(string lvlName)
    {
        
        SceneManager.LoadScene(lvlName);
    }

    public void FecharJogo()
    {
        Debug.Log("Fechou!");
        Application.Quit();
    }
   

}
