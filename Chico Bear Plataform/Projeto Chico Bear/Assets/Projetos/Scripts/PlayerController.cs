using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
//acessar o Canvas
using UnityEngine.UI;
//para cena
using UnityEngine.SceneManagement;

//Autor: Luana Karlla
//Data: 02/07/2020
//Título: Executando multiplos pulos

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;          //controlar o animator e Rigidbody2d (fisica)
    private Rigidbody2D playerRigidbody2d;
    private SpriteRenderer srPlayer; //ajudara a habilitar e desabilitar qdo player sofrer dano
    private bool playerInvencivel;

    //Player Morrendo
    public GameObject PlayerDie;

    public Transform groundCheck;            //controlar o transforms do objeto groundCheck na unity
    public bool isGround = false;            //controlar o estado que o player está no chao

    public float speed;                      //controlar a velocidade do personagem

    public float touchRun = 0.0f;             //tocar usuario para jogo no (celular)

    public bool facinRight = true;          //quando o player virar para o eixo -x

    //variaveis para o Pulo
    public bool jump = false;
    public int numberJumps = 0;
    public int maximoJump = 5;
    public float jumpForce;

    //variaveis de vida do Player
    public int vidas = 3;
    public Color hitcolor;
    public Color noHitcolor; //a variavel permite na função dano qdo player sofrer dano fique transparente



    private ControllerGame _ControllerGame;   //Referece ao Script Controller Game

    //variaveis controle por toque
    /*[SerializeField]
    private Joystick _joystick;*/

    private float _inputHorizontal = 0;

    //audio do pulo
    public AudioSource fxGame;
    public AudioClip fxPulo;

    //private int presentes; //armazena o numero de presentes
    public int tempoFase; //armazena o tempo da fase
    public Text txtTime;
    //public Text txtPresente, txtTime;

    public GameObject particula;
    //public GameObject endFase;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>(); //gerenciador

        _ControllerGame = FindObjectOfType(typeof(ControllerGame)) as ControllerGame;

        StartCoroutine("ContagemRegressiva");

    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        playerAnimator.SetBool("IsGrounded", isGround);

        Debug.Log(isGround.ToString());

        //touchRun = Input.GetAxisRaw("Horizontal"); //para o eixo x
        //touchRun = _joystick.Horizontal*speed;
        touchRun = _inputHorizontal * speed;
        Debug.Log(touchRun.ToString());

        //definir o pulo através de condição
       if (Input.GetButtonDown("Jump"))  //apetar a tecla de espaço
        {
            jump = true;
        }

        SetaMovimentos();     //chama a função criada

    }

    void MovePlayer(float movimentoH)    //criar função para o movimento direita e esquerda eixo x
    {
        playerRigidbody2d.velocity = new Vector2(movimentoH * speed, playerRigidbody2d.velocity.y);

        if (movimentoH < 0 && facinRight || (movimentoH > 0 && !facinRight)) //mudar o sinal do movimento
        {
            Flip();                    //chamar aqui a função Flip
        }

    }

    private void FixedUpdate()          //da propria unity
    {
        MovePlayer(touchRun);

        if (jump) //executar o pulo, quando for true
        {
            JumpPlayer();
        }
    }
    public void JumpPlayer()
    {
        if (isGround)
        {
            numberJumps = 0;  //como o player está no chão a contagem é = 0.
        }

        if (isGround || numberJumps < maximoJump)
        {
            playerRigidbody2d.AddForce(new Vector2(0f, jumpForce));
            isGround = false;
            numberJumps++;

            //som do pulo
            fxGame.PlayOneShot(fxPulo);
        }
        jump = false;
    }

    void Flip()                         //função que faz a mundança do player para o eixo -x (direção)
    {
        facinRight = !facinRight;       //inverte sinal, aqui fica false
        //Vector3 theScale = transform.localScale;     //Vector3 por causa dos eixos x,y,z do transform
        //theScale.x *= -1;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    void SetaMovimentos()               //permitir que seja feito os movimentos do personagem;
    {
        playerAnimator.SetFloat("EixoY", playerRigidbody2d.velocity.y);
        //se tiver movimento do personagem no eixo x
        playerAnimator.SetBool("Walk", playerRigidbody2d.velocity.x != 0 && isGround);  // condição falsa ou verdadeira (denpende)
        //chamar o pulo
        playerAnimator.SetBool("Jump", !isGround);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coletaveis":
                _ControllerGame.Pontuacao(1); //vai pegar os potes de mel de um em mum
                Destroy(collision.gameObject);
                break;

            case "Presente":
                _ControllerGame.PontuacaoPresentes(1);
                Instantiate(particula, collision.transform.position, collision.transform.rotation);
                Destroy(collision.gameObject);
                break;

            //permite o player ao tocar o inimigo seja distruido
            case "Inimigo":

                //para a explosao
                GameObject tempExplosao = Instantiate(_ControllerGame.hitPrefabs, transform.position, transform.localRotation);
                Destroy(tempExplosao, 0.5f);

                //Adiciona força ao pulo
                Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 200));

                //som do pulo da explosao
                _ControllerGame.fxGame.PlayOneShot(_ControllerGame.fxExplosao);

                //destroi o inimigo
                Destroy(collision.gameObject);
                break;
        }

        if (collision.gameObject.tag == "Player")
        {
            ControllerGame.instance.ShowGameEnd();
            //endFase.gameObject.SetActive(true);

        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            //para que o player fique na prataforma
            case "PlatafMadeira":
                this.transform.parent = collision.transform;
                break;

            case "Inimigo":
                Hurt();
                break;

        }

        if (collision.gameObject.tag == "Mar")
        {
            ControllerGame.instance.ShowGameOver();
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstaculo":
                Hurt();
                break;

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            //para que o player fique na prataforma
            case "PlatafMadeira":
                this.transform.parent = null;
                break;

        }
    }
   
    //função que detecta que o player perdeu vidas e perde
    void Hurt()
    {
        if (!playerInvencivel)
        {
            playerInvencivel = true;

            vidas--; //vidas = vidas -1
            StartCoroutine("Dano");
            _ControllerGame.BarraVida(vidas);
            Debug.Log("Perdeu uma vida");

            if (vidas < 1)
            {
                GameObject pDieTemp = Instantiate(PlayerDie, transform.position, Quaternion.identity);
                Rigidbody2D rbDie = pDieTemp.GetComponent<Rigidbody2D>();
                rbDie.AddForce(new Vector2(150f, 500f));

                _ControllerGame.fxGame.PlayOneShot(_ControllerGame.fxDie);

                //reiniciar o jogo quando o player morre
                Invoke("CarregaJogo", 4f);
                gameObject.SetActive(false);
                

            }
        }
    }
    //função carregar a cena quando o player morrer
    void CarregaJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
       
    //função (efeito) que habilita e desabilitar o player ao sofrer dano
    IEnumerator Dano()
    {
        srPlayer.color = noHitcolor; //posso trocar por hitcolor
        yield return new WaitForSeconds(0.1f);

        for (float i = 0; i < 1; i += 0.1f)
        {
            srPlayer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            srPlayer.enabled = true;
            yield return new WaitForSeconds(0.1f);

        }
        srPlayer.color = Color.white;
        playerInvencivel = false;
    }

    public void SetInputHorizontal(float valor)
    {
        _inputHorizontal = valor;
    }
     IEnumerator ContagemRegressiva()
    {
        txtTime.text = tempoFase.ToString();
        yield return new WaitForSeconds(1);

        tempoFase -= 1;
        if(tempoFase > -1)
        {
            StartCoroutine("ContagemRegressiva");
        }else
        {
            //o que vai acontecer quando o tempo acabar
            //SceneManager.LoadScene("Fases");
            Invoke("CarregaJogo", 2f);
            gameObject.SetActive(false);
        }
    }
    

}
