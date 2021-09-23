using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Autor(a) = Luana Karlla
//Data: 02/07/2020
//Título: Controller da Camera

public class ControllerCamera : MonoBehaviour
{

    public float offsetX = 3f;
    public float smooth = 0.1f;

    public float limitedUp = 2f;    //camera nas posições
    public float limitedDown = 0f;
    public float limitedLeft = 0f;
    public float limitedRight = 100f;

    private Transform player;
    private float playerX;
    private float playerY;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;  //FindObjectOfType permite encontrar o script especifico
    }

    // Update is called once per frame
    void FixedUpdate()     //maquinas rapidas deixa o jogo rapido, lento deixa o jogo lentro, essa funçao equaliza o jogo
    {
        if(player!= null)
        {
            //player ter valores do transform
            playerX = Mathf.Clamp(player.position.x + offsetX, limitedLeft, limitedRight);
            playerY = Mathf.Clamp(player.position.y, limitedDown, limitedUp);

            transform.position = Vector3.Lerp(transform.position, new Vector3(playerX, playerY, transform.position.z), smooth);
            //smooth faz a interpolação dos pontos
        }
    }
}
