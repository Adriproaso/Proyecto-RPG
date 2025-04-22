using RPG.Stats;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class StatSheetUI : MonoBehaviour
    {
        StatSheet stats;
        [SerializeField]
        TextMeshProUGUI title;
        [SerializeField]
        TextMeshProUGUI mainStat;
        [SerializeField]
        TextMeshProUGUI stat1;
        [SerializeField]
        TextMeshProUGUI stat1Lvl;
        [SerializeField]
        TextMeshProUGUI stat2;
        [SerializeField]
        TextMeshProUGUI stat2Lvl;
        [SerializeField]
        TextMeshProUGUI stat3;
        [SerializeField]
        TextMeshProUGUI stat3Lvl;

        private void Awake()
        {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>();
            stats.statsChanged += Paint;
        }

        private void Paint()
        {
            StatSheet statSheet = GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>();
            mainStat.text = statSheet.GetValue(title.text).ToString();
            stat1Lvl.text = statSheet.GetValue(stat1.text).ToString();
            stat2Lvl.text = statSheet.GetValue(stat2.text).ToString();
            stat3Lvl.text = statSheet.GetValue(stat3.text).ToString();
        }
    }
}
