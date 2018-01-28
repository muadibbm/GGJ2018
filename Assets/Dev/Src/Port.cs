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

    public override void Release(Unit unit, Plug plug) {
        if (unit) unit.Release();
        if (plug) plug.Release();
        if ((unit == null && plug != null) || plug == null || this.plug != null) return;
        this.ConnectTo(plug);
    }

    public override Plug GetPlug() {
        return this.plug;
    }

    private void ConnectTo(Plug plug) {
        this.plug = plug;
        this.plug.Release(null, null);
        this.plug.ConnectTo(this);
    }

    private void Disconnect() {
        this.plug.Select();
        this.plug.Disconnect();
        this.plug = null;
    }
}
