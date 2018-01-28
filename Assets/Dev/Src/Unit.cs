using UnityEngine;

public abstract class Unit : MonoBehaviour {

    public abstract void Select();
    public abstract void Release(Unit self);
}
