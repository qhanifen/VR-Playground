using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VertexShader : MonoBehaviour {

    public Transform proj;
    public float radius;
    Renderer render;	

    void Start()
    {
        render = GetComponent<Renderer>();
    }

	// Update is called once per frame
	void Update ()
    {
        SetMatrix();
	}

    [ContextMenu("Set Matrix")]
    void SetMatrix()
    {
        Matrix4x4 mat = proj.worldToLocalMatrix * transform.localToWorldMatrix;
        render.sharedMaterial.SetMatrix("_VolumeMatrix", mat);
        render.sharedMaterial.SetVector("_VolumePosition", proj.position);

        radius = proj.GetComponent<SphereCollider>().radius * proj.transform.localScale.x;
        render.sharedMaterial.SetFloat("_VolumeRadius", radius);
    }
}
