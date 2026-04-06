using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI reasonText;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject checkIcon;

    [SerializeField] TextMeshProUGUI statsText;

    public void Setup(Result data, bool isHistoryMode)
    {
        titleText.text = data.Title;
        reasonText.text = isHistoryMode ? data.Reason : data.Description;

        if (isHistoryMode)
        {
            lockIcon.SetActive(false);
            checkIcon.SetActive(false);
            if (statsText != null)
                statsText.text = $"C: {data.totalCarbs} P: {data.totalProtein} H: {data.totalHydration}";
        }
        else
        {
            lockIcon.SetActive(data.Locked);
            checkIcon.SetActive(!data.Locked);
            if (statsText != null) statsText.gameObject.SetActive(false);
        }
    }
}