using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_FontAsset initialAsset;
    [SerializeField] private TMP_FontAsset hoverAsset;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().font = hoverAsset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().font = initialAsset;
    }
}
