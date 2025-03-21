using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Refer�ncia ao jogador
    public Vector3 offset = new Vector3(0, 2, -10);  // Ajuste de posi��o da c�mera
    public float smoothSpeed = 5f;  // Velocidade de suaviza��o
    public bool lockY = false;  // Bloquear movimento vertical

    public Transform sun;  // Refer�ncia ao objeto sol
    public Vector2 sunViewportPosition = new Vector2(0.95f, 0.95f);  // Posi��o fixa na tela para o sol (por exemplo, canto superior direito)

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;  // Refer�ncia � c�mera principal
    }

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

        // Manter o sol sempre vis�vel e fixo na posi��o desejada na tela
        KeepSunFixedOnScreen();
    }

    private void KeepSunFixedOnScreen()
    {
        if (sun != null)
        {
            // Converte a posi��o de viewport para a posi��o mundial do sol
            Vector3 sunWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(sunViewportPosition.x,sunViewportPosition.y, mainCamera.nearClipPlane));

            // Atualiza a posi��o do sol, mantendo a dist�ncia Z fixa
            sun.position = new Vector3(sunWorldPosition.x, sunWorldPosition.y, sun.position.z);
        }
    }
}
