using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private float offset;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = target.position;
        pos.y += offset;

        transform.position = pos;
    }
}
