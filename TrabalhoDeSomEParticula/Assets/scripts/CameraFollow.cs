using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Refer�ncia ao jogador
    public Vector3 offset = new Vector3(0, 2, -10);  // Ajuste de posi��o da c�mera
    public float smoothSpeed = 5f;  // Velocidade de suaviza��o
    public bool lockY = false;  // Bloquear movimento vertical

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;

            if (lockY)
            {
                targetPosition.y = transform.position.y;  // Mant�m a altura fixa
            }

            // Suaviza o movimento da c�mera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
