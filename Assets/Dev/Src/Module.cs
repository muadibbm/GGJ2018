using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Module : Unit {

    public enum Type { ButtonClick, ButtonHold, Handlebar }
    public Type type;

    public Port portA;
    public Port portB;

    public override void Select() {

    }

    public override void Release(Unit unit) {

    }
}
