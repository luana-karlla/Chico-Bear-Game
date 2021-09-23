using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Autor: Luana Karlla
//Data: 12/08/2020
//Título: Loading de informação de uma cena para outra

//mensagem na tela



public class LoadingInfo : MonoBehaviour
{
    public static LoadingInfo instance;
    public Text txtCarregando;
    private Animator barraAnim;
    private bool sobe;

   /* private void //Awake()
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

    }*/

    //para iniar a corrotina do botao click, e entao eu chamo a função LoadGameProg()
    public void btClick()
    {
        StartCoroutine(LoadGameProg());
    }
   
    //função que carregara a cena
    IEnumerator LoadGameProg()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
       
        while (!async.isDone)
        {
            txtCarregando.enabled = true;
            yield return null;
        }
    }

   public void AnimaMenu()
    {
        barraAnim = GameObject.FindGameObjectWithTag("BarraAnimTag").GetComponent<Animator>();
        if (sobe == false)
        {
            barraAnim.Play("Move_UI");
            sobe = true;
        }
        else
        {
            barraAnim.Play("Move_UI_Inverso");
            sobe = false;
        }
    
    }
}
