using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlurManager {
	static public Dictionary<Sprite, BlurObject> dictionary = new Dictionary<Sprite, BlurObject>();

	static public Sprite RequestSprite(Sprite originalSprite, int blurSize, int blurIterations) {
		if (originalSprite == null) {
			Debug.LogError("Blur Manager: Requesting Null Sprite");

			return(null);
		}
		
		BlurObject blurObject = null;

		bool exist = dictionary.TryGetValue(originalSprite, out blurObject);

		if (exist) {

			if (blurObject.sprite == null || blurObject.sprite.texture == null) {
				
				dictionary.Remove(originalSprite);

				blurObject.sprite = LinearBlur.Blur(originalSprite, blurSize, blurIterations, Color.white);
				blurObject.blurSize = blurSize;
				blurObject.blurIterations = blurIterations;

				dictionary.Add(originalSprite, blurObject);

			} else if (blurObject.blurSize != blurSize || blurObject.blurIterations != blurIterations){

				blurObject.sprite = LinearBlur.Blur(originalSprite, blurSize, blurIterations, Color.white);
				blurObject.blurSize = blurSize;
				blurObject.blurIterations = blurIterations;

			}
			
			return(blurObject.sprite);
		} else {		
			Sprite sprite = LinearBlur.Blur(originalSprite, blurSize, blurIterations, Color.white);

			blurObject = new BlurObject(sprite, blurSize, blurIterations);

			dictionary.Add(originalSprite, blurObject);

			//return(null);  
	
			return(blurObject.sprite);
		}
	}
}
