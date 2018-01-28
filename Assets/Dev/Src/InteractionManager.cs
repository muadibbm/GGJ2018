using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public Vector3 mouseDelta;
    public LayerMask interactionLayer;

    private Vector3 prevMousePos;
    private Unit unit;
    private Plug plug;

    public delegate void OnInteraction();
    public OnInteraction onInteraction;

    public Module CurrentlyInteractingModule() {
        try {
            return ((Module)(this.unit));
        } catch (System.Exception) {
            return null;
        }
    }

    void Update () {
        this.mouseDelta = UnityEngine.Input.mousePosition - this.prevMousePos;
        if (UnityEngine.Input.GetMouseButtonUp(0)) {
            this.Interact(false);
        }
        if (UnityEngine.Input.GetMouseButtonDown(0)) {
            this.Interact(true);
        }
        this.prevMousePos = UnityEngine.Input.mousePosition;
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
                this.plug = this.unit.GetPlug();
                this.unit.Select();
                Cursor.visible = false;
            } else {
                hit.collider.GetComponent<Unit>().Release(this.unit, this.plug);
                this.unit = this.plug = null;
                Cursor.visible = true;
            }
        } else {
            this.Deinteract();
        }
        this.onInteraction();
    }

    private void Deinteract() {
        if (this.unit) {
            this.unit.Release(null, this.plug);
        }
        this.unit = this.plug = null;
        Cursor.visible = true;
    }
}
