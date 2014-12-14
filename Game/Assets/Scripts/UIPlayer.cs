using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlayer : MonoBehaviour {
	public Text energyText;
	public Image energyImage;
	public Image engineImage;
	public Image shieldImage;
	public Image weaponImage;

	// Use this for initialization
	void Start () {
		energyImage.material = new Material(Shader.Find("Custom/SpriteCutOut"));
		engineImage.material = new Material(Shader.Find("Custom/SpriteCutOut"));
		shieldImage.material = new Material(Shader.Find("Custom/SpriteCutOut"));
		weaponImage.material = new Material(Shader.Find("Custom/SpriteCutOut"));

		//SetEnergy(Random.Range(0, 100));
		//SetEngine(Random.Range(0, 100));
		//SetShield(Random.Range(0, 100));
		//SetWeapon(Random.Range(0, 100));

		SetEnergy(0f);
		SetEngine(0f);
		SetShield(0f);
		SetWeapon(0f);
	}
	

	public void SetEnergy(float pourcent){
		pourcent = pourcent % 101f;
		energyImage.material.SetFloat("_Cutoff", pourcent/100f);
		energyText.text = pourcent+"%";
	}

	public void SetEngine(float pourcent){
		pourcent = pourcent % 1001;
		engineImage.material.SetFloat("_Cutoff", pourcent/100f);
	}

	public void SetShield(float pourcent){
		pourcent = pourcent % 101f;
		shieldImage.material.SetFloat("_Cutoff", pourcent/100f);
	}

	public void SetWeapon(float pourcent){
		pourcent = pourcent % 101f;
		weaponImage.material.SetFloat("_Cutoff", pourcent/100f);
	}

}
