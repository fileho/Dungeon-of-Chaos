using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] protected int index;
    public abstract void OnDrop(PointerEventData eventData);
}
