using UnityEngine;

public class Input : Unit {

    public enum Type { Yaw, Pitch, Shoot, Reload, Shield }
    public Type type;
    public Port port;

    public override void Select() {

    }

    public override void Release(Unit unit) {

    }
}
