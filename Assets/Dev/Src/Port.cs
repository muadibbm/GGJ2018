using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Port : Unit {

    public Unit connectedTo;
    public Plug plug;

    void Start() {
        if (this.plug) {
            this.ConnectTo(this.plug);
        }
    }

    public override void Select() {
        if(this.plug) {
            this.Disconnect();
        }
    }

    public override void Release(Unit unit, Plug pPlug) {
        if (unit) unit.Release();
        if(pPlug) pPlug.Release();
        if ((unit == null && pPlug != null) || unit == this || pPlug == null) return;
        this.ConnectTo(pPlug);
    }

    private void ConnectTo(Plug plug) {
        this.plug = plug;
        this.plug.Release();
        this.plug.ConnectTo(this);
    }

    private void Disconnect() {
        this.plug.Select();
        this.plug.Disconnect();
        this.plug = null;
    }
}
