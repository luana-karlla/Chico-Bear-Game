using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Autor: Luana Karlla
//Data: 27/08/2020
//Título: level manager, menu fases

public class LevelManager : MonoBehaviour
{
    [System.Serializable] // para que as informações seja acessadas na unity
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
        public bool txtAtivo;
    }
    public GameObject botao; //variavel que vai receber o botao
    public Transform localBtn; //variavel que vai receber o local que ficara o botao que sera criado
    public List<Level> levelList; //variavel de listas dos niveis

    void ListaAdd() //função para criar atraves da programação os botoes
    {
        foreach(Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;
            BotaoLevel btnNew = btnNovo.GetComponent<BotaoLevel>();
            btnNew.levelTxtBTN.text = level.levelText;  //para pegar as informaçoes
            
            if (PlayerPrefs.GetInt("Level"+btnNew.levelTxtBTN.text) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }
            btnNew.desbloqueadoBTN = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + btnNew.levelTxtBTN.text));

            btnNovo.transform.SetParent(localBtn, false);  //false pois que se por true o botao vai ficar enorme
        }
    }
    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
       
    }
    private void Awake()
    {
        Destroy(GameObject.Find("UIManager"));
        Destroy(GameObject.Find("GamerManager"));
    }
    // Start is called before the first frame update
    void Start()
    {
        ListaAdd(); //é preciso chamar a função para que os botoes sejam visualizados na unity
       //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
