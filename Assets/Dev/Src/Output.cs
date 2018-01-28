using UnityEngine;

public class Output : Unit {

    public enum Type { Power, NULL }
    public Type type;
    public Port port;

    private void Awake() {
        this.port.connectedTo = this;
    }

    public override void Select() {
        // do nothing;
    }

    public override void Release(Unit unit, Plug plug) {
        // do nothing;
    }
}
