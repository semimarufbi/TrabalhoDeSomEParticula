using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private SpawnerColetavel spawner;  // Referência ao Spawner
    private ParticleSystem particulaColeta;  // Partícula de coleta
    private float tempoParticula;  // Tempo que a partícula ficará ativa

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
            ColetavelColetado();  // Chama a função de coleta
        }
    }

    // Método chamado quando o coletável é coletado
    public void ColetavelColetado()
    {
        if (particulaColeta != null)
        {
            // Instancia a partícula e a destrói depois de um tempo
            ParticleSystem particula = Instantiate(particulaColeta, transform.position, Quaternion.identity);
            particula.Play();
            Destroy(particula.gameObject, tempoParticula);
        }

        // Destroi o coletável
        Destroy(gameObject);

        // Respawna um novo coletável através do Spawner
        spawner.ColetavelColetado();
    }
}
