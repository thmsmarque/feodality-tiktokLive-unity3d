using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float panSpeed = 2f; // Vitesse de d�placement de la cam�ra
    public float zoomSpeed = 10f; // Vitesse de zoom de la cam�ra
    public float minZoom = 20f; // Zoom minimum
    public float maxZoom = 100f; // Zoom maximum

    private Vector3 lastMousePosition;

    void Update()
    {
        HandleMousePan();
        HandleMouseZoom();
    }

    void HandleMousePan()
    {
        if (Input.GetMouseButtonDown(1)) // Si on clique droit
        {
            // Enregistrer la position de la souris lorsque le clic droit est enfonc�
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1)) // Si le clic droit est maintenu
        {
            // Calculer le d�placement de la souris
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            // D�placer la cam�ra en fonction du d�placement de la souris
            Vector3 move = new Vector3(-deltaMousePosition.x, 0, -deltaMousePosition.y) * panSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);

            // Mettre � jour la position de la souris pour le prochain d�placement
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // R�cup�rer l'entr�e de la molette de d�filement

        if (scroll != 0.0f)
        {
            // Ajuster le champ de vision de la cam�ra pour zoomer
            Camera.main.fieldOfView -= scroll * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
        }
    }
}
