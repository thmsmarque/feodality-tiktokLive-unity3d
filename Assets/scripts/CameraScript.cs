using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float panSpeed = 2f; // Vitesse de déplacement de la caméra
    public float zoomSpeed = 10f; // Vitesse de zoom de la caméra
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
            // Enregistrer la position de la souris lorsque le clic droit est enfoncé
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1)) // Si le clic droit est maintenu
        {
            // Calculer le déplacement de la souris
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            // Déplacer la caméra en fonction du déplacement de la souris
            Vector3 move = new Vector3(-deltaMousePosition.x, 0, -deltaMousePosition.y) * panSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);

            // Mettre à jour la position de la souris pour le prochain déplacement
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Récupérer l'entrée de la molette de défilement

        if (scroll != 0.0f)
        {
            // Ajuster le champ de vision de la caméra pour zoomer
            Camera.main.fieldOfView -= scroll * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
        }
    }
}
