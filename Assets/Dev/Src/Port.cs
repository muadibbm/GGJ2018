using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Port : Unit {

    public Plug plug;

    void Start() {
        if(this.plug) {
            this.Connect(this.plug);
        }
    }

    public override void Select() {

    }

    public override void Release(Unit unit) {

    }

    private void Connect(Plug plug) {
        this.plug = plug;
        this.plug.port = this;
    }

    private void Disconnect() {
        //this.plug.port = null;
        this.plug = null;
    }
}
