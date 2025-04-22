using System;
using TMPro;
using UnityEngine;

namespace RPG.Quests.UI
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI title;
        [SerializeField]
        Transform objectiveContainer;
        [SerializeField]
        GameObject objectivePrefab;
        [SerializeField]
        GameObject objectiveIncompletePrefab;
        [SerializeField]
        TextMeshProUGUI rewardTitle;
        [SerializeField]
        TextMeshProUGUI rewardText;

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();

            //Limpia todos los quests
            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }

            foreach (Quest.Objective objective in quest.GetObjectives())
            {
                GameObject prefab = objectiveIncompletePrefab;
                if (status.IsObjectiveComplete(objective.reference))
                {
                    prefab = objectivePrefab;
                }
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
            }

            rewardText.text = GetRewardText(quest);

            if (rewardText.text == ".")
            {
                rewardText.gameObject.SetActive(false);
                rewardTitle.gameObject.SetActive(false);
            }
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())
            {
                if (rewardText != "")
                {
                    rewardText += ", ";

                }
                if (reward.number > 1)
                {
                    rewardText += reward.number + reward.item.name;
                }
                rewardText += reward.item.name + "\n";

                //rewardText += reward.xp + " xp";
            }
            rewardText += ".";
            return rewardText;
        }
    }
}
