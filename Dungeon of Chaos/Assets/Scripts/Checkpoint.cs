using UnityEngine;
using UnityEngine.Assertions;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float range = 2.5f;

    private GameObject tooltipCanvas;
    private CharacterSheet characterSheet;
    [SerializeField]
    [ReadOnly]
    private int id;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");
        tooltipCanvas = GetComponentInChildren<Canvas>().gameObject;
        characterSheet = FindObjectOfType<CharacterSheet>();

        tooltipCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range)
            Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(false);
    }

    private void Interact()
    {
        tooltipCanvas.SetActive(false);
        characterSheet.Open();
        Time.timeScale = 0f;
    }

    public void SetUniqueId(int uid)
    {
        id = uid;
    }

    public bool IsSame(int uid)
    {
        return id == uid;
    }
}
