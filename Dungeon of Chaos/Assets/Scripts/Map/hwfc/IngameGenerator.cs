using UnityEngine;
using hwfc;

public class IngameGenerator : MonoBehaviour
{
    private HierarchicalController hierarchicalController;

    // Start is called before the first frame update
    void Start()
    {
        hierarchicalController = FindObjectOfType<HierarchicalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            hierarchicalController.StartGenerating();
        }
    }
}
