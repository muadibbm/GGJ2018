using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Module : Unit {

    public enum Type { Button, Slider }
    public Type type;

    public Port portA;
    public Port portB;

    public override void Select() {
        switch(this.type) {
            case Type.Button:
                break;
            case Type.Slider:
                break;
        }
    }

    public override void Release(Unit unit) {
        switch (this.type) {
            case Type.Button:
                break;
            case Type.Slider:
                break;
        }
    }
}
