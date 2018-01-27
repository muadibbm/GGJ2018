using UnityEngine;

public class Module : Unit {

    public Unit connectedUnitA;
    public Unit connectedUnitB;

    public Interactable module;
    public Interactable portA;
    public Interactable portB;

    void Awake() {
        //portA.OnBegin += test;
        //portA.OnEnd += test2;
    }
}
