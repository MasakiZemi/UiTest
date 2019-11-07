using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExportMesh : MonoBehaviour
{
    bool onStart;
    public string a= "Assets/OpMesh.asset";

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!onStart)
        {
            AssetDatabase.CreateAsset(GetComponent<MeshFilter>().sharedMesh, a);
            AssetDatabase.SaveAssets();
            onStart = true;
        }
    }
}
