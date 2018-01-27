using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Plug : Unit {

    public Plug connectedTo;
    public Port port;
    public LayerMask cableWall;

    private Collider _collider;
    private Rigidbody _rigidbody;

    public override void Select() {

    }

    public override void Release(Unit unit) {

    }

    //private void Awake() {
    //    this._collider = this.GetComponent<Collider>();
    //    this._rigidbody = this.GetComponent<Rigidbody>();
    //    Interactable interactable = this.GetComponent<Interactable>();
    //    interactable.OnBegin += SelectPlug;
    //}
    //
    //public void SelectPlug() {
    //    if (Bootstrap.instance.ps.currentInteractingPlug == null) {
    //        Bootstrap.instance.ps.currentInteractingPlug = this;
    //    }
    //    this._collider.isTrigger = true;
    //    this._rigidbody.useGravity = false;
    //}
    //
    //public void ReleasePlug() {
    //    if (Bootstrap.instance.ps.currentInteractingPlug == this) {
    //        Bootstrap.instance.ps.currentInteractingPlug = null;
    //    }
    //    this._collider.isTrigger = (this.port != null);
    //    this._rigidbody.useGravity = (this.port == null);
    //}
    //
    //private void Update() {
    //    if(Bootstrap.instance.ps.currentInteractingPlug == this) {
    //        this.transform.position = GetPlugPositionOnPlane();
    //    }
    //}

    private Vector3 GetPlugPositionOnPlane() {
        Vector3 pos = this.transform.position;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        ray.origin = new Vector3(0.05f, -0.05f, 0f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, this.cableWall)) {
            pos = hit.point;
        }
        return pos;
    }
}
