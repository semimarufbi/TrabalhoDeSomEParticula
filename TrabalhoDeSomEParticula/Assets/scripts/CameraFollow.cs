using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public Vector3 offset = new Vector3(0, 2, -10);  // Ajuste de posição da câmera
    public float smoothSpeed = 5f;  // Velocidade de suavização
    public bool lockY = false;  // Bloquear movimento vertical

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;

            if (lockY)
            {
                targetPosition.y = transform.position.y;  // Mantém a altura fixa
            }

            // Suaviza o movimento da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
