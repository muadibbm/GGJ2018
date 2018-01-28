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
            if (module != Bootstrap.instance.im.CurrentlyInteractingModule()) return;
            switch (this.type) {
                case Input.Type.Shoot:
                    Bootstrap.instance.cg.mech.FireGun();
                    break;
                case Input.Type.Reload:
                    Bootstrap.instance.cg.mech.Reload();
                    break;
                case Input.Type.Shield:
                    break;
                default:
                    this.Diactivate(module);
                    break;
            }
        } else {
            float val;
            switch (this.type) {
                case Input.Type.Yaw:
                    val = module.GetSliderValue();
                    if (Mathf.Abs(val) < 0.03f) val = 0f;
                    Bootstrap.instance.cg.mech.SetHorizontalVelocity(val / 0.1f);
                    break;
                case Input.Type.Pitch:
                    val = module.GetSliderValue();
                    if (Mathf.Abs(val) < 0.03f) val = 0f;
                    Bootstrap.instance.cg.mech.SetVerticalVelocity(val / 0.1f);
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
                Bootstrap.instance.cg.mech.DisableWeapon();
                break;
            case Input.Type.Reload:
                Bootstrap.instance.cg.mech.DisableWeapon();
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

    public override Plug GetPlug() {
        return null;
    }
}
