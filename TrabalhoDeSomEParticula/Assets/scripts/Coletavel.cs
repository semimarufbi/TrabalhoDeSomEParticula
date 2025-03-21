using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private SpawnerColetavel spawner;  // Referência ao Spawner
    private ParticleSystem particulaColeta;  // Partícula de coleta
    private float tempoParticula;  // Tempo que a partícula ficará ativa
    public AudioSource somColetado;  // O som que será tocado

    private void Start()
    {
        somColetado = GetComponent<AudioSource>();
    }

    // Método para configurar o coletável
    public void Configurar(SpawnerColetavel spawner, ParticleSystem particulaColeta, float tempoParticula)
    {
        this.spawner = spawner;
        this.particulaColeta = particulaColeta;
        this.tempoParticula = tempoParticula;
    }

    // Método de colisão com o jogador ou outro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu com o coletável é o jogador ou outro objeto relevante
        if (other.CompareTag("Player"))  // Supondo que o jogador tenha a tag "Player"
        {
            ColetavelColetado();
            other.gameObject.GetComponent<AudioSource>().Play(); // Chama a função de coleta
        }
    }

    // Método chamado quando o coletável é coletado
    public void ColetavelColetado()
    {
        // Verifica se a partícula de coleta foi configurada
        if (particulaColeta != null)
        {
            // Instancia a partícula e a destrói depois de um tempo
            ParticleSystem particula = Instantiate(particulaColeta, transform.position, Quaternion.identity);

            // Aqui, forçamos a rotação da partícula para garantir que não fique virada para trás
            particula.transform.rotation = Quaternion.Euler(-112f, 0f, 0f);  // Z = 0 (para garantir que não tenha rotação)

            particula.Play();
            Destroy(particula.gameObject, tempoParticula); // Destroi a partícula após o tempo configurado
        }

        // Destroi o coletável com um pequeno delay para permitir o som e as partículas
        Destroy(gameObject, 5f); // Ajuste o tempo de destruição se necessário

        // Respawna um novo coletável através do Spawner
        if (spawner != null)
        {
            spawner.ColetavelColetado();
        }
        else
        {
            Debug.LogWarning("Spawner não configurado para coletável!");
        }
    }
}
