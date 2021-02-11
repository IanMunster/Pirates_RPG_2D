using UnityEngine;
using System.Collections;

public class Backfollow : MonoBehaviour
{
    public float parralax = 2f;

	// Update is called once per frame
	void Update ()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.y = transform.position.y / transform.localScale.y / parralax;
        offset.x = transform.position.x / transform.localScale.x / parralax;
        mat.mainTextureOffset = offset;
    }
}
