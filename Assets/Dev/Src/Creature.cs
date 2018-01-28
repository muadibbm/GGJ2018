using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public AkAmbient idle;
    public AkAmbient angry;
    public AkAmbient hurt;
    public AkAmbient painHit;

    public float hitpoint = 100f;

	void Start () {
        AkSoundEngine.PostEvent((uint)(int)this.idle.eventID, this.idle.gameObject);
    }
	
	public void TakeDamage(float damage) {
        this.hitpoint -= damage;
        AkSoundEngine.PostEvent((uint)(int)this.painHit.eventID, this.painHit.gameObject);
        if(this.hitpoint <= 0f) {
            AkSoundEngine.PostEvent((uint)(int)this.hurt.eventID, this.hurt.gameObject);
        }
    }
}
