using UnityEngine;
using System.Collections.Generic;

public class SpawnerColetavel : MonoBehaviour
{
    public Collider2D areaSpawn;  // �rea de spawn (GameObject vazio com um Collider2D)
    public List<GameObject> prefabsColetaveis;  // Lista de prefabs dos colet�veis
    public ParticleSystem particulaColeta;  // Part�cula ao coletar
    public float tempoRespawn = 3f;  // Tempo para reaparecer
    public float tempoParticula = 0.1f;  // Tempo que a part�cula ficar� ativa
    AudioSource som;

    private GameObject coletavelAtual;  // Refer�ncia ao colet�vel ativo

    private void Start()
    {
        SpawnarNovoColetavel();
        som = GetComponent<AudioSource>();
    }

    private void SpawnarNovoColetavel()
    {
        if (areaSpawn == null || prefabsColetaveis.Count == 0)
        {
            Debug.LogError("�rea de spawn ou lista de prefabs n�o atribu�da!");
            return;
        }

        // Define uma posi��o aleat�ria dentro do Collider2D
        Bounds bounds = areaSpawn.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 spawnPos = new Vector2(x, y);

        // Escolhe um prefab aleat�rio da lista e instancia
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
            // Salva a posi��o antes de destruir
            Vector3 posicaoColetavel = coletavelAtual.transform.position;

            // Toca o som de coleta
            som.Play();

            // Instancia a part�cula e a destr�i depois de um tempo
            if (particulaColeta != null)
            {
                // Instancia a part�cula e corrige a rota��o
                ParticleSystem particula = Instantiate(particulaColeta, posicaoColetavel, Quaternion.identity);

                // Garantir que a part�cula n�o tenha rota��o incorreta
                particula.transform.rotation = Quaternion.identity;

                particula.Play();
                Destroy(particula.gameObject, tempoParticula); // Destroi a part�cula ap�s X segundos
            }

            // Destroi o colet�vel
            Destroy(coletavelAtual);
        }

        // Respawna um novo colet�vel ap�s um tempo
        Invoke(nameof(SpawnarNovoColetavel), tempoRespawn);
    }
}
