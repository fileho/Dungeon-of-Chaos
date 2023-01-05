using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Checkpoint : MonoBehaviour, IMapSavable
{
    [SerializeField]
    private float range = 2.5f;

    private GameObject tooltipCanvas;
    private CharacterSheet characterSheet;
    private SaveSystem saveSystem;
    private GameObject gameUI;

    [Header("SFX")]
    [SerializeField]
    private SoundSettings checkpointSFX;
    private const float sfxRange = 50f;
    private SoundData sfx = null;

    private const float enemyRadius = 20f;

    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private int id;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");
        tooltipCanvas = GetComponentInChildren<Canvas>().gameObject;
        characterSheet = FindObjectOfType<CharacterSheet>();
        saveSystem = FindObjectOfType<SaveSystem>();
        gameUI = FindObjectOfType<InGameUIManager>().transform.parent.gameObject;
        tooltipCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Vector2.Distance(Character.instance.transform.position, transform.position) <= sfxRange)
        {
            if (sfx == null)
                PlaySound();
            else
                UpdateSound();
        }
        else
        {
            SoundManager.instance.StopLoopingSound(sfx);
            sfx = null;
        }

        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (!(((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range))
            return;
        if (!CheckEnemyNearby())
        {
            Interact();
        }
        else
        {
            TooltipSystem.instance.ShowMessage("Enemies are nearby!", 1.5f);
        }
    }

    private bool CheckEnemyNearby()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, enemyRadius, LayerMask.GetMask("Enemy"));
        return cols.Length > 0;
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

        Gizmos.DrawWireSphere(transform.position, enemyRadius);
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
        // Disable setting overlay
        gameUI.SetActive(false);
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
        var l = LayerMask.NameToLayer("BothMaps");
        gameObject.layer = l;
        gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = l;
    }

    public Object GetAttachedComponent()
    {
        return this;
    }

    private void PlaySound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, transform.position);
        checkpointSFX.SetVolumeFromDistance(distance, sfxRange);

        sfx = SoundManager.instance.PlaySoundLooping(checkpointSFX);
    }

    private void UpdateSound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, transform.position);

        float volume = checkpointSFX.GetVolumeFromDistance(distance, sfxRange);
        SoundManager.instance.UpdateLoopingSound(sfx, volume);
    }
}
