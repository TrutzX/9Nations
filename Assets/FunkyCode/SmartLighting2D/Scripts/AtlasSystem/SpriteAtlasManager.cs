using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAtlasRequest {
    static public List<SpriteAtlasRequest> requestList = new List<SpriteAtlasRequest>();

    // Normal = Black Alpha
    public enum Type {Normal, WhiteMask, BlackMask};
    public Sprite sprite;
    public Type type;

    public SpriteAtlasRequest (Sprite s, Type t) {
        sprite = s;
        type = t;
    }

    static public void Update() {
        foreach(SpriteAtlasRequest req in requestList) {
            float timer = Time.realtimeSinceStartup;

            SpriteAtlasManager.RequestAccess(req.sprite, req.type);

            LightingDebug.atlasTimer += (Time.realtimeSinceStartup - timer);
        }

        requestList.Clear();
    }
}

[System.Serializable]
public class SpriteAtlasManager {
    private static SpriteAtlasTexture atlasPage = null;
    public static SpriteAtlasTexture GetAtlasPage() {
        if (atlasPage == null) {
            atlasPage = new SpriteAtlasTexture();
        }
        return(atlasPage);
    }

    // Normal / White Mask / Black Mask
    public static List<Sprite> spriteListDefault = new List<Sprite>();
    public static List<Sprite> spriteListWhiteMask = new List<Sprite>();
    public static List<Sprite> spriteListBlackMask = new List<Sprite>();
    
    private static Dictionary<Sprite, Sprite> spriteDictionaryDefault = new Dictionary<Sprite, Sprite>();
    private static Dictionary<Sprite, Sprite> spriteDictionaryWhiteMask = new Dictionary<Sprite, Sprite>();
    private static Dictionary<Sprite, Sprite> spriteDictionaryBlackMask = new Dictionary<Sprite, Sprite>();

    static void PreloadSprites() {
        for(int i = 1; i <= Lighting2D.atlasSettings.spriteAtlasPreloadFoldersCount; i++) {
            string folder = Lighting2D.atlasSettings.spriteAtlasPreloadFolders[i - 1];

            object[] sprites = Resources.LoadAll(folder, typeof(Sprite));
            foreach(object obj in sprites) {
                Sprite sprite = (Sprite)obj;
                RequestAccess(sprite, SpriteAtlasRequest.Type.WhiteMask);
            }
        }
    }
  
    public static void Initialize() {
        atlasPage = new SpriteAtlasTexture();

        spriteDictionaryDefault.Clear();
        spriteDictionaryWhiteMask.Clear();
        spriteDictionaryBlackMask.Clear();

        spriteListDefault.Clear();
        spriteListWhiteMask.Clear();
        spriteListBlackMask.Clear();

        SpriteAtlasRequest.requestList.Clear();

        SpriteAtlasTexture atlasTexture = GetAtlasPage();
        atlasTexture.Initialize();
       
        PreloadSprites();
    }

    static public void Update() {
        SpriteAtlasRequest.Update();
    }

    static public Sprite RequestAccess(Sprite originalSprite, SpriteAtlasRequest.Type type) {
		Sprite spriteObject = null;

        Dictionary<Sprite, Sprite> dictionary = null;

        switch(type) {
            case SpriteAtlasRequest.Type.Normal:
                dictionary = spriteDictionaryDefault;
                break;

            case SpriteAtlasRequest.Type.WhiteMask:
                dictionary = spriteDictionaryWhiteMask;
                break;

            case SpriteAtlasRequest.Type.BlackMask:
                dictionary = spriteDictionaryBlackMask; 
                break;
        }

		bool exist = dictionary.TryGetValue(originalSprite, out spriteObject);

		if (exist) {
			if (spriteObject == null || spriteObject.texture == null) {
				dictionary.Remove(originalSprite);

				spriteObject = AddSprite(originalSprite, type);

				dictionary.Add(originalSprite, spriteObject);
			} 
			return(spriteObject);
		} else {		
			spriteObject = AddSprite(originalSprite, type);

			dictionary.Add(originalSprite, spriteObject);

			return(spriteObject);
		}
    }

    static public Sprite RequestSprite(Sprite originalSprite, SpriteAtlasRequest.Type type) {
        if (originalSprite == null) {
            return(null);
        }
        
		Sprite spriteObject = null;
        Dictionary<Sprite, Sprite> dictionary = null;

        switch(type) {
            case SpriteAtlasRequest.Type.Normal:
                dictionary = spriteDictionaryDefault;
                break;

            case SpriteAtlasRequest.Type.WhiteMask:
                dictionary = spriteDictionaryWhiteMask;
                break;

            case SpriteAtlasRequest.Type.BlackMask:
                dictionary = spriteDictionaryBlackMask;
                break;
        }

		bool exist = dictionary.TryGetValue(originalSprite, out spriteObject);

		if (exist) {
			if (spriteObject == null || spriteObject.texture == null) {
                SpriteAtlasRequest.requestList.Add(new SpriteAtlasRequest(originalSprite, type));
				return(null);
			} 
			return(spriteObject);
		} else {
            SpriteAtlasRequest.requestList.Add(new SpriteAtlasRequest(originalSprite, type));
			return(null);
		}
    }

    static private Sprite AddSprite(Sprite sprite, SpriteAtlasRequest.Type type) {
        if (sprite == null || sprite.texture == null) {
            return(null);
        }

        switch(Lighting2D.atlasSettings.spriteAtlasScale) {
            case Lighting2D.SpriteAtlasScale.None:
                return(DefaultScale.GenerateSprite(sprite, type));

            case Lighting2D.SpriteAtlasScale.X2:
                return(SmallScale.GenerateSprite(sprite, type));
        }

        return(null);
    }

    static public Texture2D GetTextureFromSprite(Sprite sprite) {
        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

         // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(sprite.texture.width, sprite.texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(sprite.texture, tmp);

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
       // Texture2D myTexture2D = new Texture2D(sprite.texture.width, sprite.texture.height);
        Texture2D myTexture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        Rect tempRect = sprite.rect;
        tempRect.y = sprite.texture.height - tempRect.y - sprite.rect.height;

        myTexture2D.ReadPixels(tempRect, 0, 0);

        // Copy the pixels from the RenderTexture to the new Texture
        //myTexture2D.ReadPixels(sprite.rect, 0, 0);
        myTexture2D.Apply();

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

       // RenderTexture.active = null;
        RenderTexture.active = previous;
        
        return(myTexture2D);
    }

    public class DefaultScale {
            
        static public Sprite GenerateSprite(Sprite sprite, SpriteAtlasRequest.Type type) {
            SpriteAtlasTexture atlasTexture = GetAtlasPage();
            Texture2D texture = atlasTexture.GetTexture();

            if (texture == null) {
                return(null);
            }

            if (atlasTexture.currentX + sprite.rect.width >= atlasTexture.atlasSize) {
                atlasTexture.currentX = 1;
                atlasTexture.currentY += atlasTexture.currentHeight;
                atlasTexture.currentHeight = 0;
            }

        if (atlasTexture.currentY + sprite.rect.height >= atlasTexture.atlasSize) {
            Debug.Log("Error: Lighting Atlas Overhead (" + atlasTexture.atlasSize + ") (" + sprite + ")");
            LightingManager2D.Get().disableEngine = true;
            return(null);
        }

        Texture2D myTexture2D = GetTextureFromSprite(sprite);

        Color color;
        
        switch(type) {
            case SpriteAtlasRequest.Type.Normal:
                for(int x = 0; x < (int)sprite.rect.width; x++) {
                    for(int y = 0; y < (int)sprite.rect.height; y++) {
                        color = myTexture2D.GetPixel(x + (int)sprite.rect.x, y + (int)sprite.rect.y);
                        
                        color.a = 1;

                        texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                    }
                }
                break;

            case SpriteAtlasRequest.Type.WhiteMask:
                    for(int x = 0; x < (int)sprite.rect.width; x++) {
                    for(int y = 0; y < (int)sprite.rect.height; y++) {
                        color = myTexture2D.GetPixel(x + (int)sprite.rect.x, y + (int)sprite.rect.y);

                        color.r = 1;
                        color.g = 1;
                        color.b = 1;

                        texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                    }
                }
                break;
                
                case SpriteAtlasRequest.Type.BlackMask:
                    for(int x = 0; x < (int)sprite.rect.width; x++) {
                        for(int y = 0; y < (int)sprite.rect.height; y++) {
                            color = myTexture2D.GetPixel(x + (int)sprite.rect.x, y + (int)sprite.rect.y);
                        
                            color.a = ((1 - color.r) + (1 - color.g) + (1 - color.b)) / 3;
                            color.r = 0;
                            color.g = 0;
                            color.b = 0;

                            texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                        }
                    }
                    break;
            }
        
            texture.Apply();

            atlasTexture.spriteCount ++;

            Vector2 pivot = new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height);
            
            Sprite output = Sprite.Create(texture, new Rect(atlasTexture.currentX, atlasTexture.currentY, myTexture2D.width, myTexture2D.height), pivot, sprite.pixelsPerUnit);
        
            switch(type) {
                case SpriteAtlasRequest.Type.BlackMask:
                    spriteListBlackMask.Add(output);
                    break;

                case SpriteAtlasRequest.Type.WhiteMask:
                    spriteListWhiteMask.Add(output);
                    break;

                case SpriteAtlasRequest.Type.Normal:
                    spriteListDefault.Add(output);
                    break;
            }

            atlasTexture.currentX += (int)sprite.rect.width;
            atlasTexture.currentHeight = Mathf.Max(atlasTexture.currentHeight, (int)sprite.rect.height);
            return(output);
        }
    }

    public class SmallScale {
        
        static public Sprite GenerateSprite(Sprite sprite, SpriteAtlasRequest.Type type) {
            SpriteAtlasTexture atlasTexture = GetAtlasPage();
            Texture2D texture = atlasTexture.GetTexture();

            Texture2D myTexture2D = GetTextureFromSprite(sprite);

            int image_x = (int)(sprite.rect.x * 0.5f);
            int image_y = (int)(sprite.rect.y * 0.5f);
            int image_width = (int)(sprite.rect.width * 0.5f);
            int image_height = (int)(sprite.rect.height * 0.5f);

            if (atlasTexture.currentX + image_width >= atlasTexture.atlasSize) {
                atlasTexture.currentX = 1;
                atlasTexture.currentY += atlasTexture.currentHeight;
                atlasTexture.currentHeight = 0;
            }

            Color color;

            switch(type) {
                case SpriteAtlasRequest.Type.BlackMask:
                for(int x = 0; x < image_width; x++) {
                        for(int y = 0; y < image_height; y++) {
                            color = myTexture2D.GetPixel(x * 2 , y * 2);
                            texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                        }
                    }
                    break;

                case SpriteAtlasRequest.Type.WhiteMask:
                    for(int x = 0; x < image_width; x++) {
                        for(int y = 0; y < image_height; y++) {
                            color = myTexture2D.GetPixel(x * 2 , y * 2);
                            color.r = 1;
                            color.g = 1;
                            color.b = 1;
                            texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                        }
                    }
                    break;

                case SpriteAtlasRequest.Type.Normal:
                    for(int x = 0; x < image_width; x++) {
                        for(int y = 0; y < image_height; y++) {
                            color = myTexture2D.GetPixel(x * 2 , y * 2); 
                            color.r = 0;
                            color.g = 0;
                            color.b = 0;
                            texture.SetPixel(atlasTexture.currentX + x, atlasTexture.currentY + y, color);
                        }
                    }
                    break;
            }

            texture.Apply();
            atlasTexture.spriteCount ++;

            Vector2 pivot = new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height);
            Sprite output = Sprite.Create(texture, new Rect(atlasTexture.currentX, atlasTexture.currentY, myTexture2D.width / 2, myTexture2D.height / 2), pivot, sprite.pixelsPerUnit * 0.5f);
        
            switch(type) {
                case SpriteAtlasRequest.Type.BlackMask:
                    spriteListBlackMask.Add(output);
                    break;

                case SpriteAtlasRequest.Type.WhiteMask:
                    spriteListWhiteMask.Add(output);
                    break;

                case SpriteAtlasRequest.Type.Normal:
                    spriteListDefault.Add(output);
                    break;
            }

            atlasTexture.currentX += (int)image_width;
            atlasTexture.currentHeight = Mathf.Max(atlasTexture.currentHeight, image_height);

            return(output);
        }
    }
}