using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public LayerMask interactionLayer;

    private Interactable currentInteracting;

	void Update () {
        if (UnityEngine.Input.GetMouseButtonUp(0)) {
            if(this.currentInteracting != null) {
                this.currentInteracting.OnInteractionEnd();
                this.currentInteracting = null;
            }
        }
        if (UnityEngine.Input.GetMouseButtonDown(0)) {
            if (this.currentInteracting != null) return;
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, this.interactionLayer)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null) {
                    this.currentInteracting = interactable;
                    this.currentInteracting.OnInteractionBegin();
                }
            }
        }
    }
}
