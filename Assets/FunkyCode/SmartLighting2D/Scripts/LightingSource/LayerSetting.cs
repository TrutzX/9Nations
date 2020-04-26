public enum LightingLayer {Layer1, Layer2, Layer3, Layer4, Layer5, Layer6};
public enum LightingLayerType {Default, ShadowOnly, MaskOnly}
public enum LightingLayerOrder {Default, DistanceToLight, YAxis};
public enum LightingLayerEffect {Default, InvisibleBellow};

[System.Serializable]
public class LayerSetting {
	public LightingLayer layerID = LightingLayer.Layer1;
	public LightingLayerType type = LightingLayerType.Default;
	public LightingLayerOrder renderingOrder = LightingLayerOrder.Default;
	public LightingLayerEffect effect = LightingLayerEffect.Default;
	public float maskEffectDistance = 1;

	public int GetLayerID() {
		int layer = (int)layerID;
		if (layer < 0) {
			return(-1);
		}
		return(layer);
	}
}
