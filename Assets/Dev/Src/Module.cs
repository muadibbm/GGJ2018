using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Module : Unit {

    public enum Type { Button, VerticalSlider, HorizontalSlider }
    public Type type;

    public Port portA;
    public Port portB;

    public AkAmbient moduleSelected;
    public AkAmbient moduleReleased;
    public AkAmbient moduleLocked;

    private bool selected = false;
    private Transform slider;

    private Input connectedInput;

    private void Awake() {
        if (this.type == Type.VerticalSlider || this.type == Type.HorizontalSlider)
            this.slider = this.GetComponentInChildren<Transform>();
        this.portA.connectedTo = this;
        this.portB.connectedTo = this;
    }

    private void Start() {
        Bootstrap.instance.im.onInteraction += this.UpdateMech;
    }

    public override void Select() {
        this.selected = true;
        this.PlayModuleSelected();
    }

    public override void Release(Unit unit, Plug plug) {
        if (unit) {
            unit.Release();
            if (plug) plug.Release();
            return;
        } else {
            if (plug) {
                plug.Release();
                return;
            }
        }
        this.selected = false;
        this.PlayerModuleReleased();
    }

    public override Plug GetPlug() {
        return null;
    }

    public float GetSliderValue() {
        return this.slider.transform.localPosition.z;
    }

    private void Update() {
        if (this.selected) {
            switch (this.type) {
                case Type.Button:
                    break;
                case Type.VerticalSlider:
                    this.EngageSlider(Bootstrap.instance.im.mouseDelta.y);
                    break;
                case Type.HorizontalSlider:
                    this.EngageSlider(-Bootstrap.instance.im.mouseDelta.x);
                    break;
            }
        } else {
            switch (this.type) {
                case Type.Button:
                    break;
                case Type.VerticalSlider:
                    this.DisengageSlider();
                    break;
                case Type.HorizontalSlider:
                    this.DisengageSlider();
                    break;
            }
        }
    }

    private void EngageSlider(float value) {
        Vector3 pos = this.slider.transform.localPosition;
        pos.z = Mathf.Clamp(pos.z + Time.deltaTime * Time.deltaTime * value, -0.1f, 0.1f);
        this.slider.transform.localPosition = pos;
    }

    private void DisengageSlider() {
        Vector3 pos = this.slider.transform.localPosition;
        if (Mathf.Abs(pos.z) <= 0.03f) {
            if (pos.z != 0f) {
                AkSoundEngine.PostEvent((uint)(int)this.moduleLocked.eventID, this.moduleLocked.gameObject);
            }
            pos.z = 0f;
        }
        this.slider.transform.localPosition = pos;
    }

    private Output.Type GetOutput(Port port) {
        try {
            return ((Output)(port.plug.connectedTo.port.connectedTo)).type;
        } catch (System.Exception) {
            return Output.Type.NULL;
        }
    }

    private Input.Type GetInput(Port port) {
        try {
            return ((Input)(port.plug.connectedTo.port.connectedTo)).type;
        } catch (System.Exception) {
            return Input.Type.NULL;
        }
    }

    private void UpdateMech() {
        if (this.GetOutput(this.portA) == Output.Type.Power) {
            this.Activate(this.portB);
        } else if (this.GetOutput(this.portB) == Output.Type.Power) {
            this.Activate(this.portA);
        } else {
            this.Diactivate();
        }
    }

    private void Activate(Port port) {
        Input.Type input = this.GetInput(port);
        if (input == Input.Type.NULL) {
            this.Diactivate();
            return;
        }
        this.connectedInput = (Input)(port.plug.connectedTo.port.connectedTo);
        this.connectedInput.Activate(this);
    }

    private void Diactivate() {
        if (this.connectedInput) {
            this.connectedInput.Diactivate(this);
            this.connectedInput = null;
        }
    }

    private void PlayModuleSelected() {
        if(this.moduleSelected) {
            AkSoundEngine.PostEvent((uint)(int)this.moduleSelected.eventID, this.moduleSelected.gameObject);
        }
    }

    private void PlayerModuleReleased() {
        if (this.moduleReleased) {
            AkSoundEngine.PostEvent((uint)(int)this.moduleReleased.eventID, this.moduleReleased.gameObject);
        }
    }
}
