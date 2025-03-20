using UnityEngine;

public class player : MonoBehaviour
{
    private float inputHorizontal;
    private Rigidbody2D rb;
    [SerializeField] private int velocidade = 5;
    [SerializeField] private Animator animacao;
    public SpriteRenderer sprite;

    public AudioSource audioSource;
    public AudioClip somAndando;  // Som ao andar
    public AudioClip somPulo;  // Som ao pular
    public AudioClip somDoubleJump;  // Som ao fazer double jump

    private bool isJumping = false;  // Controla o primeiro pulo
    private bool canDoubleJump = false;  // Permite o segundo pulo (double jump)
    private int numberOfJumps = 0;  // Conta o número de pulos (1 para o primeiro pulo, 2 para o double jump)

    [SerializeField] private float jumpForce = 300f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animacao = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();  // Obtém o componente de áudio
    }

    private void Update()
    {
        // Captura o movimento horizontal do jogador
        inputHorizontal = Input.GetAxis("Horizontal");

        // Chama a função para inverter o sprite
        spriteFlip(inputHorizontal);

        // Lógica para pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)  // Primeiro pulo
            {
                rb.AddForce(Vector2.up * jumpForce);
                animacao.SetTrigger("pulo");
                isJumping = true;
                numberOfJumps = 1;

                audioSource.PlayOneShot(somPulo);  // Toca som do pulo
            }
            else if (canDoubleJump)  // Double Jump
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
                animacao.SetTrigger("double");
                numberOfJumps = 2;
                canDoubleJump = false;

                audioSource.PlayOneShot(somDoubleJump);  // Toca som do double jump
            }
        }

        // Se o jogador está andando e não está pulando, ativa a animação de andar
        if (inputHorizontal != 0 && !isJumping)
        {
            animacao.SetBool("andando", true);

            // Se o som não está tocando, toca o som de andar
            if (!audioSource.isPlaying)
            {
                audioSource.clip = somAndando;
                audioSource.Play();
            }
        }
        else if (!isJumping)
        {
            animacao.SetBool("andando", false);

            // Para o som de andar quando o jogador parar
            if (audioSource.clip == somAndando)
            {
                audioSource.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        // Movimenta o personagem com base no input horizontal
        rb.velocity = new Vector2(inputHorizontal * velocidade, rb.velocity.y);
    }

    // Função para inverter o sprite dependendo do movimento horizontal
    public void spriteFlip(float horizontal)
    {
        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se tocou o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            canDoubleJump = true;
        }
    }
}
