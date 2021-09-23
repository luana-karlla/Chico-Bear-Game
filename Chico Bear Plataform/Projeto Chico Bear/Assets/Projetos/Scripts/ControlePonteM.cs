using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Autor: Luana Karlla
//Data: 29/07/2020
//Título: Ponte Madeira movimento para direita e esquerda

public class ControlePonteM : MonoBehaviour
{
    public Transform ponteMadeira, pontoA, pontoB;   //1 variaveis que permitiram controlar as madeiras e os pontos
    public float     velocidadePonteM; // 2 variavel que permitira controlar a velocidade da madeira entre os pontos

    private Vector3 pontoDestino; //5 variavel que permitira a madeira retornar ao pontoA

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //3 Para garantir a posição inicial que será o pontoA
        /*lembrete: sempre associe o script ao objeto que nao vai atrapalhar ou seja ao 1 gameobjet q leva os demais
         objetos pai e filho*/
        ponteMadeira.position = pontoA.position; //pega a variavel e posicao, depois ponto e a posicao
        pontoDestino = pontoB.position; // 7 para realmente a madeira ir de um ponto A para o ponto B e vice versa

    }

    // Update is called once per frame
    void Update()
    {
        //4 é a chave para que a madeira se movimente do pontoA para o pontoB
        ponteMadeira.position = Vector3.MoveTowards(ponteMadeira.position, pontoDestino, velocidadePonteM * Time.deltaTime);

        //6 Fazer teste de ponto de destino, onde há a troca de posição
        if(ponteMadeira.position == pontoDestino)
        {
            if(pontoDestino == pontoA.position)
            {
                pontoDestino = pontoB.position;
            }
            else if(pontoDestino == pontoB.position)
            {
                pontoDestino = pontoA.position;
            }
        }

    }
}
