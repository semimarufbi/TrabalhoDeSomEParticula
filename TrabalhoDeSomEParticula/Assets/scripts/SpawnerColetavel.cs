using UnityEngine;
using System.Collections.Generic;

public class SpawnerColetavel : MonoBehaviour
{
    public Collider2D areaSpawn;  // Área de spawn (GameObject vazio com um Collider2D)
    public List<GameObject> prefabsColetaveis;  // Lista de prefabs dos coletáveis
    public ParticleSystem particulaColeta;  // Partícula ao coletar
    public float tempoRespawn = 3f;  // Tempo para reaparecer
    public float tempoParticula = 0.1f;  // Tempo que a partícula ficará ativa
    AudioSource som;

    private GameObject coletavelAtual;  // Referência ao coletável ativo

    private void Start()
    {
        SpawnarNovoColetavel();
        som = GetComponent<AudioSource>();
    }

    private void SpawnarNovoColetavel()
    {
        if (areaSpawn == null || prefabsColetaveis.Count == 0)
        {
            Debug.LogError("Área de spawn ou lista de prefabs não atribuída!");
            return;
        }

        // Define uma posição aleatória dentro do Collider2D
        Bounds bounds = areaSpawn.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 spawnPos = new Vector2(x, y);

        // Escolhe um prefab aleatório da lista e instancia
        GameObject prefabEscolhido = prefabsColetaveis[Random.Range(0, prefabsColetaveis.Count)];
        coletavelAtual = Instantiate(prefabEscolhido, spawnPos, Quaternion.identity);

        // Configura o script de coleta no objeto instanciado
        Coletavel coletavelScript = coletavelAtual.GetComponent<Coletavel>();
        if (coletavelScript != null)
        {
            coletavelScript.Configurar(this, particulaColeta, tempoParticula);
        }
    }

    public void ColetavelColetado()
    {
        if (coletavelAtual != null)
        {
            // Salva a posição antes de destruir
            Vector3 posicaoColetavel = coletavelAtual.transform.position;

            // Toca o som de coleta
            som.Play();

            // Instancia a partícula e a destrói depois de um tempo
            if (particulaColeta != null)
            {
                // Instancia a partícula e corrige a rotação
                ParticleSystem particula = Instantiate(particulaColeta, posicaoColetavel, Quaternion.identity);

                // Garantir que a partícula não tenha rotação incorreta
                particula.transform.rotation = Quaternion.identity;

                particula.Play();
                Destroy(particula.gameObject, tempoParticula); // Destroi a partícula após X segundos
            }

            // Destroi o coletável
            Destroy(coletavelAtual);
        }

        // Respawna um novo coletável após um tempo
        Invoke(nameof(SpawnarNovoColetavel), tempoRespawn);
    }
}
