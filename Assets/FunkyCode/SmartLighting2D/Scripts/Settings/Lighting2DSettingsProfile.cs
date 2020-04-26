using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Lighting2D/Settings Profile", order = 1)]

public class Lighting2DSettingsProfile : ScriptableObject {
	// Main Preferences
	public Lighting2D.RenderingPipeline renderingPipeline = Lighting2D.RenderingPipeline.Standard;
	public Lighting2D.RenderingMode renderingMode = Lighting2D.RenderingMode.OnRender;

	// Sorting
	public Lighting2D.SortingLayer sortingLayer = new Lighting2D.SortingLayer();

	// Settings
	public Lighting2D.CommonSettings commonSettings = new Lighting2D.CommonSettings();

	// Day Settings
	public Lighting2D.DayLightingSettings dayLightingSettings = new Lighting2D.DayLightingSettings();

	// Atlas
	public Lighting2D.AtlasSettings atlasSettings = new Lighting2D.AtlasSettings();

	// Light Source Buffers
	public Lighting2D.LightingSourceSettings lightingSourceSettings = new Lighting2D.LightingSourceSettings();

	// Utilities
	public PolygonTriangulator2D.Triangulation triangulation = PolygonTriangulator2D.Triangulation.Advanced;
}
