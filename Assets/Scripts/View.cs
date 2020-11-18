using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisCity.Nicolas 
{
	public class View : MonoBehaviour
	{
        [SerializeField] private Material gridMaterial;
        [SerializeField] private HexaGrid hexaGrid;

        private Layout layout;
        private Camera mainCam;

        private int textureWidth, textureHeight;

        private void Start()
        {
            mainCam = Camera.main;
            Texture2D texture = gridMaterial.mainTexture as Texture2D;

            textureHeight = texture.height;
            textureWidth = texture.width;

            float hexaWidth = (float)textureWidth / hexaGrid.NbCellsbyWidth;
            float hexaRadius = hexaWidth / 2;

            layout = Layout.Pointy(new Vector2(textureWidth / 2, textureHeight / 2), hexaRadius);



            /*
            Color[] colors = new Color[3];
            colors[0] = Color.red;
            colors[1] = Color.green;
            colors[2] = Color.blue;
            int mipCount = Mathf.Min(3, texture.mipmapCount);

            // tint each mip level
            for (int mip = 0; mip < mipCount; ++mip)
            {
                Color[] cols = texture.GetPixels(mip);
                for (int i = 0; i < cols.Length; ++i)
                {
                    cols[i] = new Color(Random.value, Random.value, Random.value);
                }
                texture.SetPixels(cols, mip);
            }
            // actually apply all SetPixels, don't recalculate mip levels
            texture.Apply(false);
        }
            */
        }
    }