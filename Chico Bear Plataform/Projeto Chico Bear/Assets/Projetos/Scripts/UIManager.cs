using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Autor(a); Luana
//Data: 08/09/2020
//Titulo: UIManager menus

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
   // public Text pontosUI;
    [SerializeField]
    private GameObject pausePainel;
    [SerializeField]
    private Button btPause,btPause_Return;
    //[SerializeField]
   // private Button btNovamente,btLoja; // btMLevel



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
            DontDestroyOnLoad(this.gameObject);
        }
      /* else
        {
            Destroy(gameObject);
        }*/

        //SceneManager.sceneLoaded += Carrega; //quando mudar de cena ser carregado com os pontos.
    }
    void Carrega(Scene cena, LoadSceneMode modo)
    {
        //pontosUI = GameObject.Find("txtScore").GetComponent<Text>();         //para pegar e manter par a proxima cena os pontos coletados
        pausePainel = GameObject.Find("PausePanel");                         //nome que foi renomeado no inspector da unity  - PausePanel
        //btPause = GameObject.Find("btPausar").GetComponent<Button>();        //botao pause abrir
       //btPause_Return = GameObject.Find("btPlayer").GetComponent<Button>(); //botao pause  retornar- 
        //btNovamente = GameObject.Find("btMenuReplay").GetComponent<Button>();   //entre parenteses é o nome que esta no bt no inspector da unity
       // btLoja = GameObject.Find("btMenuLoja").GetComponent<Button>();      
        //btMLevel = GameObject.Find("btMenuFases").GetComponent<Button>();


        //LigaDesligaPainel(); //qdo apaguei daqui o painel nao deixou eu clicar

       // btPause.onClick.AddListener(Pause); //chama o metodo Pause
       //btPause_Return.onClick.AddListener(PauseReturn); //chama o metodo ao clicar
       // btMLevel.onClick.AddListener(Levels);
      // btLoja.onClick.AddListener(BtToBuy); //para depois

       //btNovamente.onClick.AddListener(PlayReturn);
        
    }

    public void StartUI() 
    {
      LigaDesligaPainel();
    }

   /* public void UpdateUI()

        {
            pontosUI.text = ControllerGame.instance.totalScore.ToString();  //se for necessario deletar essa função UpdateUI
            
        }*/

        void LigaDesligaPainel()
        {
        StartCoroutine(Tempo());
       
    }

        public void Pause()
        {
       
        pausePainel.SetActive(true); //preciso aticar o painel true

        pausePainel.GetComponent<Animator>().Play("Move_Pause");  //para habilitar a animação do Menu Pausar
        Time.timeScale = 0;
        StartCoroutine(Tempo());
       
    }
    public void PauseReturn()
    {
        pausePainel.SetActive(false);

        pausePainel.GetComponent<Animator>().Play("Move_Pauser");  //para habilitar a animação do Menu Pausar
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
        
    }

    IEnumerator EsperaPause()
    {
        
        yield return new WaitForSeconds(0.8f); //desativa o menu
        pausePainel.SetActive(false); 
    }

    IEnumerator Tempo()
        {
           
            yield return new WaitForSeconds(0.001f); //(0.001f)
            pausePainel.SetActive(false);
        }
   
   /* public void BtToBuy()
    {
        SceneManager.LoadScene(1);
    }


   public void PlayReturn()
    {
        SceneManager.LoadScene(0);

    }*/

    }
