using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Autor: Luana Karlla
//Data: 17/07/2020
//Título: IA do inimigo Rato

public class IAMouse : MonoBehaviour
{
    public Transform      enemie;
    public SpriteRenderer enemieSprite;   //Para controlar a imagem na posição 1 ou -1
    public Transform[]    position;  //para controlar as posições no inspector, por isso usa vetor/matriz
    public float          speed;           //controlar a velocidade do inimigo
    public bool           isRight;          //verificar se o inimigo está olhando para a direita ou não

    private int           idTarget;         //vai controlar o alvo de onde vai chegar o inimigo


    // Start is called before the first frame update
    void Start()
    {
        //pegar as caracteristicas do objeto Rato como transform, spriteRenderer atraves do GetComponent..
        enemieSprite = enemie.gameObject.GetComponent<SpriteRenderer>();
        enemie.position = position[0].position; //pegar a posição do ponto A
        idTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //para testar se o inimigo tem um objeto, se está estanciado, em funcionamento.
        if (enemie != null)
        {
            //vector3 por estar trabalhando com os tres eixos
            //Time.deltaTime vai armonizar a velocidade independente do pc, cel, placa que esteja o jogo
            enemie.position = Vector3.MoveTowards(enemie.position, position[idTarget].position, speed * Time.deltaTime);

            //Teste para saber que ponto o inimigo está, ponto A ou B
            if (enemie.position == position[idTarget].position)
            {
                idTarget += 1;                   //1+1=2 sai de um ponto A para o outro B
                if (idTarget == position.Length)
                {
                    idTarget = 0;               //volta para a posição inicial
                }
            }
            //testando se o inimigo está olhando para a direita ou não
            if (position[idTarget].position.x < enemie.position.x && isRight == true)
            {
                Flip();
            }
            else if (position[idTarget].position.x > enemie.position.x && isRight == false)
            {
                Flip();
            }
        }

    }
    void Flip()
    {
        isRight = !isRight;
        enemieSprite.flipX = !enemieSprite.flipX;  //true = fase ou ao contrario (troca)
    }
}
