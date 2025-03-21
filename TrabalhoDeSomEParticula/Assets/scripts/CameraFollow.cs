using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public Vector3 offset = new Vector3(0, 2, -10);  // Ajuste de posição da câmera
    public float smoothSpeed = 5f;  // Velocidade de suavização
    public bool lockY = false;  // Bloquear movimento vertical

    public Transform sun;  // Referência ao objeto sol
    public Vector2 sunViewportPosition = new Vector2(0.95f, 0.95f);  // Posição fixa na tela para o sol (por exemplo, canto superior direito)

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;  // Referência à câmera principal
    }

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

        // Manter o sol sempre visível e fixo na posição desejada na tela
        KeepSunFixedOnScreen();
    }

    private void KeepSunFixedOnScreen()
    {
        if (sun != null)
        {
            // Converte a posição de viewport para a posição mundial do sol
            Vector3 sunWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(sunViewportPosition.x,sunViewportPosition.y, mainCamera.nearClipPlane));

            // Atualiza a posição do sol, mantendo a distância Z fixa
            sun.position = new Vector3(sunWorldPosition.x, sunWorldPosition.y, sun.position.z);
        }
    }
}
