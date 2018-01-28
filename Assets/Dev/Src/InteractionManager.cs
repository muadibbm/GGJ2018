using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public LayerMask interactionLayer;

    private Unit unit; // currently interacting unit

    void Update () {
        if (UnityEngine.Input.GetMouseButtonUp(0)) {
            this.Interact(false);
        }
        if (UnityEngine.Input.GetMouseButtonDown(0)) {
            this.Interact(true);
        }
    }

    private void Interact(bool begin) {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit,
                            Mathf.Infinity, this.interactionLayer, QueryTriggerInteraction.Ignore)) {
            if(hit.collider == null) {
                this.Deinteract();
                return;
            }
            if (begin) {
                this.unit = hit.collider.GetComponent<Unit>();
                this.unit.Select();
            } else {
                this.unit.Release(hit.collider.GetComponent<Unit>());
            }
        } else {
            this.Deinteract();
        }
    }

    private void Deinteract() {
        if (this.unit) {
            this.unit.Release(null);
            this.unit = null;
        }
    }
}
