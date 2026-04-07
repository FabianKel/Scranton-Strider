using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultEntry : MonoBehaviour
{
    [Header("General UI")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI reasonText;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject checkIcon;

    [Header("Gear Icons")]
    [SerializeField] Image shoesIcon;
    [SerializeField] Image kitIcon;
    [SerializeField] Image visorIcon;

    [Header("Stats")]
    [SerializeField] TextMeshProUGUI statsText;

    public void Setup(Result data, bool isHistoryMode)
    {
        if (isHistoryMode)
        {
            titleText.text = data.Title;
            reasonText.text = $"Result: {data.Reason}";

            shoesIcon.gameObject.SetActive(true);
            kitIcon.gameObject.SetActive(true);
            visorIcon.gameObject.SetActive(true);

            SetIconState(shoesIcon, data.hasShoes);
            SetIconState(kitIcon, data.hasKit);
            SetIconState(visorIcon, data.hasVisor);

            if (statsText != null)
            {
                statsText.gameObject.SetActive(true);
                statsText.text = $"C: {data.totalCarbs} | P: {data.totalProtein} | H: {data.totalHydration}";
            }

            if (lockIcon) lockIcon.SetActive(false);
            if (checkIcon) checkIcon.SetActive(false);
        }
        else
        {

            shoesIcon.gameObject.SetActive(false);
            kitIcon.gameObject.SetActive(false);
            visorIcon.gameObject.SetActive(false);

            if (data.Locked)
            {
                titleText.text = "???";
                reasonText.text = "Locked: " + data.Description;
            }
            else
            {
                titleText.text = data.Title;
                reasonText.text = data.Description;
            }

            if (statsText != null) statsText.gameObject.SetActive(false);
            if (lockIcon) lockIcon.SetActive(data.Locked);
            if (checkIcon) checkIcon.SetActive(!data.Locked);
        }
    }

    private void SetIconState(Image icon, bool hasItem)
    {
        if (icon == null) return;
        // Color.white (1,1,1,1) mantiene el color original del sprite
        // Color.gray (0.5, 0.5, 0.5, 1) lo oscurece
        icon.color = hasItem ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1f);
    }
}