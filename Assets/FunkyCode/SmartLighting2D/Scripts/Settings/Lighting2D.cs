using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting2D {
	public const int VERSION = 111;

	// Main Preferences
	static public Lighting2D.RenderingPipeline renderingPipeline = Lighting2D.RenderingPipeline.Standard;
	static public Lighting2D.RenderingMode renderingMode = Lighting2D.RenderingMode.OnRender;
	static public SortingLayer sortingLayer = new SortingLayer();

	// Settings
	static public CommonSettings commonSettings = new CommonSettings();

	// Day Settings
	static public DayLightingSettings dayLightingSettings = new DayLightingSettings();

	// Atlas
	static public AtlasSettings atlasSettings = new AtlasSettings();

	// Light Source Buffers
	static public LightingSourceSettings lightingSourceSettings = new LightingSourceSettings();
	
	// Utilities
	static public PolygonTriangulator2D.Triangulation triangulation = PolygonTriangulator2D.Triangulation.Advanced;

	// Methods
	static public void UpdateByProfile(Lighting2DSettingsProfile profile) {
		// Rendering
		renderingPipeline = profile.renderingPipeline;
		renderingMode = profile.renderingMode;

		sortingLayer.ID = profile.sortingLayer.ID;
		sortingLayer.Name = profile.sortingLayer.Name;
		sortingLayer.Order = profile.sortingLayer.Order;

		// Settings
		commonSettings.lightingResolution = profile.commonSettings.lightingResolution;
		commonSettings.hdr = profile.commonSettings.hdr;
		commonSettings.darknessColor = profile.commonSettings.darknessColor;
		commonSettings.drawRooms = profile.commonSettings.drawRooms;
		commonSettings.drawOcclusion = profile.commonSettings.drawOcclusion;
		commonSettings.drawPenumbra = profile.commonSettings.drawPenumbra;
		commonSettings.drawSunPenumbra = profile.commonSettings.drawSunPenumbra;
		commonSettings.drawAdditiveLights = profile.commonSettings.drawAdditiveLights;
		commonSettings.drawHighQualityShadows = profile.commonSettings.drawHighQualityShadows;
		commonSettings.darknessBuffer = profile.commonSettings.darknessBuffer;

		// Day Lighting
		dayLightingSettings.drawDayShadows = profile.dayLightingSettings.drawDayShadows;
		dayLightingSettings.sunDirection = profile.dayLightingSettings.sunDirection; // Should't it represent degrees???
		dayLightingSettings.shadowDarkness = profile.dayLightingSettings.shadowDarkness;
		dayLightingSettings.sunPenumbra = profile.dayLightingSettings.sunPenumbra;

		// Atlas
		atlasSettings.lightingSpriteAtlas = profile.atlasSettings.lightingSpriteAtlas;
		atlasSettings.spriteAtlasScale = profile.atlasSettings.spriteAtlasScale;
		atlasSettings.spriteAtlasSize = profile.atlasSettings.spriteAtlasSize;

		atlasSettings.spriteAtlasPreloadFoldersCount = profile.atlasSettings.spriteAtlasPreloadFoldersCount;
		atlasSettings.spriteAtlasPreloadFolders = profile.atlasSettings.spriteAtlasPreloadFolders;

		// Lighting Source Buffers
		lightingSourceSettings.fixedLightBufferSize = profile.lightingSourceSettings.fixedLightBufferSize;
		lightingSourceSettings.fixedLightTextureSize = profile.lightingSourceSettings.fixedLightTextureSize;
		lightingSourceSettings.textureFormat = profile.lightingSourceSettings.textureFormat;
		lightingSourceSettings.lightingBufferPreload = profile.lightingSourceSettings.lightingBufferPreload;
		lightingSourceSettings.lightingBufferPreloadCount = profile.lightingSourceSettings.lightingBufferPreloadCount;

		// Misc
		triangulation = profile.triangulation;
	}

	public enum RenderingPipeline {
		Standard = 0,
		Scriptable = 1
	}

	// Settings Enumerators
	public enum RenderingMode {
		OnPreRender = 0, 
		OnPostRender = 1,
		OnRender = 2
	}

	public enum SpriteAtlasScale {
		None,
		X2
	}

	public enum SpriteAtlasSize {
		px2048,
		px1024,
		px512,
		px256
	}

	[System.Serializable]
	public class CommonSettings {
		public Color darknessColor = Color.black;
	
		public float lightingResolution = 1f;

		public bool hdr = false;
		public bool drawRooms = true;
		public bool drawOcclusion = true;
		public bool drawPenumbra = true;
		public bool drawSunPenumbra = true;
		public bool drawAdditiveLights = true;
		public bool drawHighQualityShadows = true;
		public bool darknessBuffer = true; // Draw Main Buffer
	}

	[System.Serializable]
	public class DayLightingSettings {
		public bool drawDayShadows = true;
		public float sunDirection = - 90;
		public float shadowDarkness = 1;
		public float sunPenumbra = 0.5f;
	}

	[System.Serializable]
	public class SortingLayer {
		public string Name;
		public int ID;
		public int Order;
	}

	[System.Serializable]
	public class AtlasSettings {
		public bool lightingSpriteAtlas = false;
		public SpriteAtlasScale spriteAtlasScale = SpriteAtlasScale.None;
		public SpriteAtlasSize spriteAtlasSize = SpriteAtlasSize.px256;

		public int spriteAtlasPreloadFoldersCount = 0;
		public string[] spriteAtlasPreloadFolders = new string[1];
	}

	[System.Serializable]
	public class LightingSourceSettings {
		public bool fixedLightBufferSize = true;
		public LightingSourceTextureSize fixedLightTextureSize = LightingSourceTextureSize.px512;
		public RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32;
		public bool lightingBufferPreload = false;
		public int lightingBufferPreloadCount = 1;
	}

	// Profile
	static public Lighting2DSettingsProfile profile = null;

	static public Lighting2DSettingsProfile GetProfile() {
		if (profile == null) {
			profile = Resources.Load("Profiles/Lighting Default") as Lighting2DSettingsProfile;
		}

		return(profile);
	}
}
