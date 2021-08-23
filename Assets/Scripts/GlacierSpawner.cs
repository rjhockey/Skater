using UnityEngine;

public class GlacierSpawner : MonoBehaviour
{
    private const float DISTANCE_TO_RESPAWN = 10f;

    public float scrollSpeed = -2f;
    public float totalLength;
    public bool IsScrolling { set; get; }

    private float scrollLocation;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!IsScrolling)
            return; //do nothing

        scrollLocation += scrollSpeed * Time.deltaTime;
        //where the glacier will be
        Vector3 newLocation = (playerTransform.position.z + scrollLocation) *Vector3.forward;
        transform.position = newLocation;
        if (transform.GetChild(0).transform.position.z < playerTransform.position.z - DISTANCE_TO_RESPAWN)
        {
            transform.GetChild(0).localPosition += Vector3.forward * totalLength; //set first object
            transform.GetChild(0).SetSiblingIndex(transform.childCount); //move it to end

            transform.GetChild(0).localPosition += Vector3.forward * totalLength; //set first object
            transform.GetChild(0).SetSiblingIndex(transform.childCount); //move it to end
        }
    }
}
