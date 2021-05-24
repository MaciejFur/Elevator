using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScreen : MonoBehaviour
{
    [SerializeField] private Font mFont;
    [SerializeField] private List<MeshFilter> mPlaneFilters;
    
    [SerializeField] private int mAmmoCount = 60;

    void Awake()
    {
        foreach (MeshFilter meshFilter in mPlaneFilters)
            meshFilter.GetComponent<MeshRenderer>().material.mainTexture = mFont.material.mainTexture;
    }

    void Start()
    {
        StartCoroutine(AmmoCountDown());
    }

    private void CreateFont(string output)
    {
        // Get the texture based on the font, and characters needed
        mFont.RequestCharactersInTexture(output);

        // For each character in the string
        for (int i = 0; i < output.Length; i++)
        {
            // Character Data
            CharacterInfo character;
            mFont.GetCharacterInfo(output[i], out character);
            
            // Set Uvs
            Vector2[] uvs = new Vector2[4];
            uvs[0] = character.uvBottomLeft;
            uvs[1] = character.uvTopRight;
            uvs[2] = character.uvBottomRight;
            uvs[3] = character.uvTopLeft;
            
            // Apply UVs
            mPlaneFilters[i].mesh.uv = uvs;
            
            // Get basic scale
            Vector3 newScale = mPlaneFilters[i].transform.localScale;
            newScale.x = character.glyphWidth * 0.02f;
            
            // Set
            mPlaneFilters[i].transform.localScale = newScale;
        }
    }

    private IEnumerator AmmoCountDown()
    {
        while (mAmmoCount > 0)
        {
            DisplayAmmo(mAmmoCount);
            mAmmoCount--;

            yield return new WaitForSeconds(1f);
        }
    }

    private void DisplayAmmo(int ammoCount)
    {
        string output = ammoCount.ToString();

        if (ammoCount < 10)
            output = "0" + output;
        
        CreateFont(output);
    }
}
