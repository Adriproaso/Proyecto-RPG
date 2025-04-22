using RPG.Quests;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;

    QuestStatus status;


    public void Setup(QuestStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();
    }

    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}
