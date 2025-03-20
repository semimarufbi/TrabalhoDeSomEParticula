using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private SpawnerColetavel spawner;  // Refer�ncia ao Spawner
    private ParticleSystem particulaColeta;  // Part�cula de coleta
    private float tempoParticula;  // Tempo que a part�cula ficar� ativa

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
            ColetavelColetado();  // Chama a fun��o de coleta
        }
    }

    // M�todo chamado quando o colet�vel � coletado
    public void ColetavelColetado()
    {
        if (particulaColeta != null)
        {
            // Instancia a part�cula e a destr�i depois de um tempo
            ParticleSystem particula = Instantiate(particulaColeta, transform.position, Quaternion.identity);
            particula.Play();
            Destroy(particula.gameObject, tempoParticula);
        }

        // Destroi o colet�vel
        Destroy(gameObject);

        // Respawna um novo colet�vel atrav�s do Spawner
        spawner.ColetavelColetado();
    }
}
