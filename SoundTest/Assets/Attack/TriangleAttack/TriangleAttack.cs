using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAttack : MonoBehaviour
{
    public GameObject[] pointPos = new GameObject[4];
    public Material material;

    public GameObject beamObj;
    public float speed = 3;

    GameObject obj;

    float timer;

    class BeamData
    {
        public GameObject beamObjList;
        public float dis;
    }
    List<BeamData> beam = new List<BeamData>();

    public bool[] onStop;
    bool onInstantObj;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3[] posArray = new Vector3[pointPos.Length];
        //for (int i = 0; i < pointPos.Length; i++) posArray[i] = pointPos[i].transform.position;
        ////MeshCreate(posArray);
        obj = new GameObject("test");

        for (int i = 0; i < 3; i++)
        {
            beam.Add(new BeamData());
            beam[i].beamObjList = Instantiate(beamObj, pointPos[0].transform);
            beam[i].beamObjList.transform.LookAt(pointPos[i + 1].transform);
            beam[i].dis = Vector3.Distance(pointPos[0].transform.position, pointPos[i + 1].transform.position);
        }
        for (int i = 0; i < 2; i++)
        {
            beam.Add(new BeamData());
            beam[beam.Count-1].beamObjList = Instantiate(beamObj, pointPos[pointPos.Length - 1].transform);
            beam[beam.Count - 1].beamObjList.transform.LookAt(pointPos[i + 1].transform);
            beam[beam.Count - 1].dis = Vector3.Distance(pointPos[pointPos.Length - 1].transform.position, pointPos[i + 1].transform.position);
        }
        beam.Add(new BeamData());
        beam[beam.Count - 1].beamObjList=Instantiate(beamObj, pointPos[1].transform);
        beam[beam.Count - 1].beamObjList.transform.LookAt(pointPos[2].transform);
        beam[beam.Count - 1].dis = Vector3.Distance(pointPos[1].transform.position, pointPos[2].transform.position);
        onStop = new bool[beam.Count];
    }

    // Update is called once per frame
    void Update()
    {
        Beam();
    }

    void Beam()
    {
        for (int i = 0; i < beam.Count; i++)
        {
            if (beam[i].beamObjList.transform.localScale.z < beam[i].dis * 2)
            {
                beam[i].beamObjList.transform.localScale = Siz(i);
            }
            else onStop[i] = true;
        }

        if (onStop[onStop.Length - 1] && !onInstantObj)
        {
            Vector3[] posArray = new Vector3[pointPos.Length];
            for (int i = 0; i < pointPos.Length; i++) posArray[i] = pointPos[i].transform.position;
            MeshCreate(posArray);
        }
        if (onInstantObj) {
            timer += Time.deltaTime;
           // material.SetFloat("_Timer", timer);
        }

        Vector3 Siz(int count)
        {
            Vector3 siz = beam[count].beamObjList.transform.localScale;
            siz.z += speed * Time.deltaTime;
            return siz;
        }

    }

    void MeshCreate(Vector3[] pos)
    {
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<MeshFilter>();

        var mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            pos[0], pos[1], pos[2],
            pos[0], pos[1], pos[3],
            pos[0], pos[2], pos[3],
            pos[1], pos[2], pos[3]
        };
        mesh.triangles = new int[] 
        {
            0, 2, 1,
            3, 4, 5,
            6, 8, 7,
            9, 10, 11,
        };
        obj.GetComponent<MeshFilter>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        obj.GetComponent<Renderer>().material = material;

        onInstantObj = true;
    }
}
