using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Plug : Unit {

    public Plug connectedTo;
    public Port port;
    public LayerMask cableWall;

    public AkAmbient jackIn;
    public AkAmbient jackOut;
    public AkAmbient jackDrop;

    private bool selected = false;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Awake() {
        this._collider = this.GetComponent<Collider>();
        this._rigidbody = this.GetComponent<Rigidbody>();
    }

    public override void Select() {
        this.selected = true;
    }

    public override void Release(Unit unit = null, Plug plug = null) {
        if (unit) { unit.Release(); }
        if (plug) { plug.Release(); }
        this.selected = false;
        this.port = null;
    }

    public override Plug GetPlug() {
        return this;
    }

    public void ConnectTo(Port port) {
        this.port = port;
        this.transform.position = port.transform.position;
        AkSoundEngine.PostEvent((uint)(int)this.jackIn.eventID, this.jackIn.gameObject);
    }

    public void Disconnect() {
        this.port = null;
        AkSoundEngine.PostEvent((uint)(int)this.jackOut.eventID, this.jackOut.gameObject);
    }

    private void Update() {
        this._collider.isTrigger = (this.port != null) || this.selected;
        this._rigidbody.useGravity = (this.port == null) && !this.selected;
        if (this.selected) {
            this.transform.position = GetPlugPositionOnPlane(1.7f);
        }
    }

    private Vector3 GetPlugPositionOnPlane(float frontOfCamera) {
        var mousePos = UnityEngine.Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z + frontOfCamera;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
        return newPos;
    }

    private void OnCollisionEnter(Collision collision) {
        AkSoundEngine.PostEvent((uint)(int)this.jackDrop.eventID, this.jackDrop.gameObject);
    }
}
