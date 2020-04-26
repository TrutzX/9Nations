using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighting2DUtility {
    public class Max2D {

        // Used By Day Lighting
        static public void DrawMesh(Material material, Mesh mesh, Transform transform, Vector2D offset, float z = 0f) {
            if (mesh == null) {
                return;
            }
            
            GL.PushMatrix ();
            material.SetPass (0); 
            GL.Begin(GL.TRIANGLES);

            for (int i = 0; i <  mesh.triangles.GetLength (0); i = i + 3) {
                Vector3 a = transform.TransformPoint(mesh.vertices [mesh.triangles [i]]);
                Vector3 b = transform.TransformPoint(mesh.vertices [mesh.triangles [i + 1]]);
                Vector3 c = transform.TransformPoint(mesh.vertices [mesh.triangles [i + 2]]);
                Max2DMatrix.DrawTriangle(a.x, a.y, b.x, b.y, c.x, c.y, offset, z);
            }

            GL.End ();
            GL.PopMatrix ();
        }



        
        // Used By Lighting Source Sprite
        static public void DrawImage2D(Material material, Vector2 pos, Vector2D size, float rot, float z, bool flipX, bool flipY){
            rot = rot * Mathf.Deg2Rad + Mathf.PI;

            float rectAngle = Mathf.Atan2((float)size.y, (float)size.x);
            float dist = Mathf.Sqrt((float)(size.x * size.x + size.y * size.y));

            Vector2 v1 = new Vector2(pos.x + Mathf.Cos(rectAngle + rot) * dist, pos.y + Mathf.Sin(rectAngle + rot) * dist);
            Vector2 v2 = new Vector2(pos.x + Mathf.Cos(-rectAngle + rot) * dist, pos.y + Mathf.Sin(-rectAngle + rot) * dist);
            Vector2 v3 = new Vector2(pos.x + Mathf.Cos(rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(rectAngle + Mathf.PI + rot) * dist);
            Vector2 v4 = new Vector2(pos.x + Mathf.Cos(-rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(-rectAngle + Mathf.PI + rot) * dist);
            
            Vector2 uv1 = new Vector2(0, 0);
            Vector2 uv2 = new Vector2(0, 1);
            Vector2 uv3 = new Vector2(1, 1);
            Vector2 uv4 = new Vector2(1, 0);

            if (flipY) {
                float tmpY = uv1.y;
                uv1.y = uv2.y;
                uv2.y = tmpY;

                tmpY = uv3.y;
                uv3.y = uv4.y;
                uv4.y = tmpY;
            }

            if (flipX) {
                float tmpX = uv1.x;
                uv1.x = uv3.x;
                uv3.x = tmpX;

                tmpX = uv2.x;
                uv2.x = uv4.x;
                uv4.x = tmpX;
            }

            material.SetPass (0); 
            GL.Begin (GL.QUADS);
            GL.TexCoord2 (uv1.x, uv1.y);
            GL.Vertex3 (v1.x, v1.y, z);
            GL.TexCoord2 (uv2.x, uv2.y);
            GL.Vertex3 (v2.x, v2.y, z);
            GL.TexCoord2 (uv3.x, uv3.y);
            GL.Vertex3 (v3.x, v3.y, z);
            GL.TexCoord2 (uv4.x, uv4.y);
            GL.Vertex3 (v4.x, v4.y, z);
            GL.End ();
        }

        // Used By Lighting Source Sprite
        static public void DrawImage2DBatched(Vector2 pos, Vector2D size, float rot, float z, bool flipX, bool flipY){
            rot = rot * Mathf.Deg2Rad + Mathf.PI;

            float rectAngle = Mathf.Atan2((float)size.y, (float)size.x);
            float dist = Mathf.Sqrt((float)(size.x * size.x + size.y * size.y));

            Vector2 v1 = new Vector2(pos.x + Mathf.Cos(rectAngle + rot) * dist, pos.y + Mathf.Sin(rectAngle + rot) * dist);
            Vector2 v2 = new Vector2(pos.x + Mathf.Cos(-rectAngle + rot) * dist, pos.y + Mathf.Sin(-rectAngle + rot) * dist);
            Vector2 v3 = new Vector2(pos.x + Mathf.Cos(rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(rectAngle + Mathf.PI + rot) * dist);
            Vector2 v4 = new Vector2(pos.x + Mathf.Cos(-rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(-rectAngle + Mathf.PI + rot) * dist);
            
            Vector2 uv1 = new Vector2(0, 0);
            Vector2 uv2 = new Vector2(0, 1);
            Vector2 uv3 = new Vector2(1, 1);
            Vector2 uv4 = new Vector2(1, 0);

            if (flipY) {
                float tmpY = uv1.y;
                uv1.y = uv2.y;
                uv2.y = tmpY;

                tmpY = uv3.y;
                uv3.y = uv4.y;
                uv4.y = tmpY;
            }

            if (flipX) {
                float tmpX = uv1.x;
                uv1.x = uv3.x;
                uv3.x = tmpX;

                tmpX = uv2.x;
                uv2.x = uv4.x;
                uv4.x = tmpX;
            }
        
            GL.TexCoord2 (uv1.x, uv1.y);
            GL.Vertex3 (v1.x, v1.y, z);
            GL.TexCoord2 (uv2.x, uv2.y);
            GL.Vertex3 (v2.x, v2.y, z);
            GL.TexCoord2 (uv3.x, uv3.y);
            GL.Vertex3 (v3.x, v3.y, z);
            GL.TexCoord2 (uv4.x, uv4.y);
            GL.Vertex3 (v4.x, v4.y, z);
        }

        // Used By Lighting Main Buffer
        static public void DrawImage(Material material, Vector2D pos, Vector2D size, float z = 0f) {
            GL.PushMatrix ();
            material.SetPass (0); 
            GL.Begin (GL.QUADS);

            GL.TexCoord2 (0, 0);
            GL.Vertex3 ((float)pos.x - (float)size.x, (float)pos.y - (float)size.y, z);
            GL.TexCoord2 (0, 1);
            GL.Vertex3 ((float)pos.x - (float)size.x, (float)pos.y + (float)size.y, z);
            GL.TexCoord2 (1, 1);
            GL.Vertex3 ((float)pos.x + (float)size.x, (float)pos.y + (float)size.y, z);
            GL.TexCoord2 (1, 0);
            GL.Vertex3 ((float)pos.x + (float)size.x, (float)pos.y - (float)size.y, z);

            GL.End ();
            GL.PopMatrix ();
        }

        // Used By Day Lighting And Lighting Manager 2D
        static public void DrawImage(Material material, Vector2 pos, Vector2 size, float rot, float z = 0f){
            GL.PushMatrix ();
            material.SetPass (0); 
            
            rot = rot * Mathf.Deg2Rad + Mathf.PI;

            float rectAngle = Mathf.Atan2(size.y, size.x);
            float dist = Mathf.Sqrt(size.x * size.x + size.y * size.y);

            Vector2 v1 = new Vector2(pos.x + Mathf.Cos(rectAngle + rot) * dist, pos.y + Mathf.Sin(rectAngle + rot) * dist);
            Vector2 v2 = new Vector2(pos.x + Mathf.Cos(-rectAngle + rot) * dist, pos.y + Mathf.Sin(-rectAngle + rot) * dist);
            Vector2 v3 = new Vector2(pos.x + Mathf.Cos(rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(rectAngle + Mathf.PI + rot) * dist);
            Vector2 v4 = new Vector2(pos.x + Mathf.Cos(-rectAngle + Mathf.PI + rot) * dist, pos.y + Mathf.Sin(-rectAngle + Mathf.PI + rot) * dist);
            
            GL.Begin (GL.QUADS);
            GL.TexCoord2 (0, 0);
            GL.Vertex3 (v1.x, v1.y, z);
            GL.TexCoord2 (0, 1);
            GL.Vertex3 (v2.x, v2.y, z);
            GL.TexCoord2 (1, 1);
            GL.Vertex3 (v3.x, v3.y, z);
            GL.TexCoord2 (1, 0);
            GL.Vertex3 (v4.x, v4.y, z);
            GL.End ();

            GL.PopMatrix ();
        }
    }
}
