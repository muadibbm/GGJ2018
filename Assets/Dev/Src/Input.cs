using UnityEngine;

public class Input : Unit {

    public enum Type { Yaw, Pitch, Shoot, Reload, Shield, NULL }
    public Type type;
    public Port port;

    private void Awake() {
        this.port.connectedTo = this;
    }

    public void Activate(Module module) {
        if(module.type == Module.Type.Button) {
            switch (this.type) {
                case Input.Type.Shoot:
                    break;
                case Input.Type.Reload:
                    break;
                case Input.Type.Shield:
                    break;
                default:
                    this.Diactivate(module);
                    break;
            }
        } else {
            switch (this.type) {
                case Input.Type.Yaw:
                    Bootstrap.instance.cg.mech.SetHorizontalVelocity(module.GetSliderValue() / 0.1f);
                    break;
                case Input.Type.Pitch:
                    Bootstrap.instance.cg.mech.SetVerticalVelocity(module.GetSliderValue() / 0.1f);
                    break;
                default:
                    this.Diactivate(module);
                    break;
            }
        }
    }

    public void Diactivate(Module module) {
        switch (this.type) {
            case Input.Type.Yaw:
                Bootstrap.instance.cg.mech.SetHorizontalVelocity(0f);
                break;
            case Input.Type.Pitch:
                Bootstrap.instance.cg.mech.SetVerticalVelocity(0f);
                break;
            case Input.Type.Shoot:
                break;
            case Input.Type.Reload:
                break;
            case Input.Type.Shield:
                break;
            case Input.Type.NULL:
                break;
        }
    }

    public override void Select() {
        // do nothing;
    }

    public override void Release(Unit unit, Plug plug) {
        // do nothing;
    }
}
