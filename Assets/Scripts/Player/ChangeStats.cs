using UnityEngine;

namespace RPG.Stats 
{
    public class ChangeStats : MonoBehaviour
    {
        [SerializeField]
        StatSheet.Stats statToChange;
        [SerializeField]
        int changeAmount;

        /// <summary>
        /// Cambia el stat pedido por el numero introducido
        /// </summary>
        public void ChangeStat()
        {
            StatSheet statSheet = GameObject.FindGameObjectWithTag("Player").GetComponent<StatSheet>();
            statSheet.SetStat(changeAmount, statToChange);
        }

    }
}