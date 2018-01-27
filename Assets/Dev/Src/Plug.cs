using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Plug : MonoBehaviour {

    public Plug connectedTo;
    public LayerMask PlugPlatform;

    private bool selected;

    void Awake() {
        Interactable interactable = this.GetComponent<Interactable>();
        interactable.OnBegin = SelectPlug;
        interactable.OnEnd = ReleasePlug;
    }

    private void SelectPlug() {
        Debug.Log("selecting plug " + name);
        this.selected = true;
    }

    private void ReleasePlug() {
        Debug.Log("releaseing plug " + name);
        this.selected = false;
    }

    private void Update() {
        if(this.selected) {
            this.transform.position = GetPlugPositionOnPlane();
        }
    }

    private Vector3 GetPlugPositionOnPlane() {
        Vector3 pos = this.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, this.PlugPlatform)) {
            pos = hit.point;
        }
        return pos;
    }
}
