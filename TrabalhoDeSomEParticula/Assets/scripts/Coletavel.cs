using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private SpawnerColetavel spawner;  // Refer�ncia ao Spawner
    private ParticleSystem particulaColeta;  // Part�cula de coleta
    private float tempoParticula;  // Tempo que a part�cula ficar� ativa
    public AudioSource somColetado;  // O som que ser� tocado

    private void Start()
    {
        somColetado = GetComponent<AudioSource>();
    }

    // M�todo para configurar o colet�vel
    public void Configurar(SpawnerColetavel spawner, ParticleSystem particulaColeta, float tempoParticula)
    {
        this.spawner = spawner;
        this.particulaColeta = particulaColeta;
        this.tempoParticula = tempoParticula;
    }

    // M�todo de colis�o com o jogador ou outro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu com o colet�vel � o jogador ou outro objeto relevante
        if (other.CompareTag("Player"))  // Supondo que o jogador tenha a tag "Player"
        {
            ColetavelColetado();
            other.gameObject.GetComponent<AudioSource>().Play(); // Chama a fun��o de coleta
        }
    }

    // M�todo chamado quando o colet�vel � coletado
    public void ColetavelColetado()
    {
        // Verifica se a part�cula de coleta foi configurada
        if (particulaColeta != null)
        {
            // Instancia a part�cula e a destr�i depois de um tempo
            ParticleSystem particula = Instantiate(particulaColeta, transform.position, Quaternion.identity);

            // Aqui, for�amos a rota��o da part�cula para garantir que n�o fique virada para tr�s
            particula.transform.rotation = Quaternion.Euler(-112f, 0f, 0f);  // Z = 0 (para garantir que n�o tenha rota��o)

            particula.Play();
            Destroy(particula.gameObject, tempoParticula); // Destroi a part�cula ap�s o tempo configurado
        }

        // Destroi o colet�vel com um pequeno delay para permitir o som e as part�culas
        Destroy(gameObject, 5f); // Ajuste o tempo de destrui��o se necess�rio

        // Respawna um novo colet�vel atrav�s do Spawner
        if (spawner != null)
        {
            spawner.ColetavelColetado();
        }
        else
        {
            Debug.LogWarning("Spawner n�o configurado para colet�vel!");
        }
    }
}
