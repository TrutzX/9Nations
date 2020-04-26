using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class Lighting2DSettings : MonoBehaviour {
    public bool initializeCopy = false;

    public Lighting2DSettingsProfile setProfile;
    public Lighting2DSettingsProfile profile;

    static Lighting2DSettings instance;

    public void SetProfile(Lighting2DSettingsProfile profile) {
        setProfile = profile;
    }

    public Lighting2DSettingsProfile GetProfile() {
        return(setProfile);
    }

    void Awake() {
        instance = this;

        SetupProfile();
    }

    void SetupProfile() {
         // Set Up Main Profile as Default
        if (setProfile == null) {
            setProfile = Lighting2D.GetProfile();
        } 

        if (initializeCopy) {
            profile = Object.Instantiate(setProfile);
        } else {
            profile = setProfile;
        }
    }

    void Update() {
        if (profile != null) {
            if (Application.IsPlaying(gameObject)) {
                Lighting2D.UpdateByProfile(profile);
            } else {
                Lighting2D.UpdateByProfile(setProfile);
            }
			
		} else {
            SetupProfile();
        }
    }

    static public Lighting2DSettings Get() {
		if (instance != null) {
			return(instance);
		}

		foreach(Lighting2DSettings manager in Object.FindObjectsOfType(typeof(Lighting2DSettings))) {
			instance = manager;
			return(instance);
		}

		GameObject gameObject = LightingManager2D.Get().gameObject;

		instance = gameObject.AddComponent<Lighting2DSettings>();
		return(instance);
	}
}
