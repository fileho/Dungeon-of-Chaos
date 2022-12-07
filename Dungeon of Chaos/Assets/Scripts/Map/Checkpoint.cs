using UnityEngine;
using UnityEngine.Assertions;

public class Checkpoint : MonoBehaviour, IMapSavable
{
    [SerializeField]
    private float range = 2.5f;

    private GameObject tooltipCanvas;
    private CharacterSheet characterSheet;
    private SaveSystem saveSystem;

    [SerializeField] private SoundSettings checkpointSFX;

    [SerializeField]
    [ReadOnly]
    private int id;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");
        tooltipCanvas = GetComponentInChildren<Canvas>().gameObject;
        characterSheet = FindObjectOfType<CharacterSheet>();
        saveSystem = FindObjectOfType<SaveSystem>();

        tooltipCanvas.SetActive(false);
        if (SoundManager.instance)
            SoundManager.instance.PlaySoundLooping(checkpointSFX);
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
        saveSystem.DungeonData.AddSavedUid(id);
        tooltipCanvas.SetActive(false);
        characterSheet.Open();
        Time.timeScale = 0f;
    }

    public void SetUniqueId(int uid)
    {
        id = uid;
    }

    public int GetUniqueId()
    {
        return id;
    }

    public void Load()
    {
        var l = LayerMask.NameToLayer("Walls");
        gameObject.layer = l;
        gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = l;
    }

    public Object GetAttachedComponent()
    {
        return this;
    }
}
