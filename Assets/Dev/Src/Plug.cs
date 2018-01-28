﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Plug : Unit {

    public Plug connectedTo;
    public Port port;
    public LayerMask cableWall;

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

    public override void Release(Unit unit) {
        this.selected = false;
    }

    private void Update() {
        this._collider.isTrigger = (this.port != null) && this.selected;
        this._rigidbody.useGravity = (this.port == null) && !this.selected;
        if (this.selected) {
            this.transform.position = GetPlugPositionOnPlane();
        }
    }

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
