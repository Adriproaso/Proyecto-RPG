using RPG.Core;
using RPG.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{ 
    public class StatSheet : MonoBehaviour, IPredicateEvaluate, ISaveable
    {
        [Serializable]
        public class StatSheetSaveData
        {
            public int health;
            public int morale;

            public int fortaleza;
            public int fuerza;
            public int fuerzaXP;
            public int aguante;
            public int aguanteXP;
            public int constitucion;
            public int constitucionXP;

            public int destreza;
            public int acrobacias;
            public int acrobaciasXP;
            public int reaccion;
            public int reaccionXP;
            public int concentracion;
            public int concentracionXP;

            public int inteligencia;
            public int investigacion;
            public int investigacionXP;
            public int historia;
            public int historiaXP;
            public int biologia;
            public int biologiaXP;

            public int compenetracion;
            public int drama;
            public int dramaXP;
            public int autoridad;
            public int autoridadXP;
            public int subconsciente;
            public int subconscienteXP;
        }

        [System.Serializable]
        public enum Stats
        {
            Health,
            Morale,

            Fortaleza,
            Fuerza,
            Aguante,
            Constitucion,

            Destreza,
            Acrobacias,
            Reaccion,
            Concentracion,

            Inteligencia,
            Investigacion,
            Historia,
            Biologia,

            Compenetracion,
            Drama,
            Autoridad,
            Subconsciente
    }

        public event Action statsChanged;

        #region stats
        [SerializeField]
        int xpToLevel;

        [Range(1, 9)]
        [SerializeField]
        int health;
        [Range(1, 9)]
        [SerializeField]
        int morale;

        [Header("Fortaleza")]
        [SerializeField]
        int fortaleza;
        [Range(1, 9)]
        [SerializeField]
        int fuerza;
        int fuerzaXP;
        [Range(1, 9)]
        [SerializeField]
        int aguante;
        int aguanteXP;
        [Range(1, 9)]
        [SerializeField]
        int constitucion;
        int constitucionXP;

        [Header("Destreza")]
        [SerializeField]
        int destreza;
        [Range(1, 9)]
        [SerializeField]
        int acrobacias;
        int acrobaciasXP;
        [Range(1, 9)]
        [SerializeField]
        int reaccion;
        int reaccionXP;
        [Range(1, 9)]
        [SerializeField]
        int concentracion;
        int concentracionXP;

        [Header("Inteligencia")]
        [SerializeField]
        int inteligencia;
        [Range(1, 9)]
        [SerializeField]
        int investigacion;
        int investigacionXP;
        [Range(1, 9)]
        [SerializeField]
        int historia;
        int historiaXP;
        [Range(1, 9)]
        [SerializeField]
        int biologia;
        int biologiaXP;

        [Header("Compenetracion")]
        [SerializeField]
        int compenetracion;
        [Range(1, 9)]
        [SerializeField]
        int drama;
        int dramaXP;
        [Range(1, 9)]
        [SerializeField]
        int autoridad;
        int autoridadXP;
        [Range(1, 9)]
        [SerializeField]
        int subconsciente;
        int subconscienteXP;
        #endregion

        [SerializeField]
        List<string> tags = new List<string>();

        private void Start()
        {
            UpdateUI();
        }

        /// <summary>
        /// Pone los stats base a la suma de los substats
        /// </summary>
        private void SetBaseStats()
        {
            fortaleza = fuerza + aguante + constitucion;

            destreza = acrobacias + reaccion + concentracion;

            inteligencia = investigacion + historia + biologia;

            compenetracion = drama + autoridad + subconsciente;
        }

        /// <summary>
        /// Devuelve el stat pedido
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetValue(string stat)
        {
            switch (stat)
            {
                case "Health":
                    return health;
                case "Morale":
                    return morale;

                case "Fortaleza":
                    return fortaleza;
                case "Fuerza Bruta":
                    return fuerza;
                case "Aguante":
                    return aguante;
                case "Constitucion":
                    return constitucion;

                case "Destreza":
                    return destreza;
                case "Acrobacias":
                    return acrobacias;
                case "Reaccion":
                    return reaccion;
                case "Concentracion":
                    return concentracion;

                case "Inteligencia":
                    return inteligencia;
                case "Investigacion":
                    return investigacion;
                case "Historia":
                    return historia;
                case "Biologia":
                    return biologia;

                case "Compenetracion":
                    return compenetracion;
                case "Drama":
                    return drama;
                case "Autoridad":
                    return autoridad;
                case "Subconsciente":
                    return subconsciente;
            }
            return 0;
        }

        /// <summary>
        /// Busca el tag pedido entre los tags del jugador
        /// </summary>
        /// <param name="tagCheck"></param>
        /// <returns></returns>
        public bool LookForTag(string tagCheck)
        {
            foreach (string tag in tags)
            {
                if (tag == tagCheck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Modifica el nivel del stat
        /// </summary>
        /// <param name="statChange"></param>
        /// <param name="statToChange"></param>
        public void SetStat(int statChange, Stats statToChange)
        {
            switch (statToChange)
            {
                case Stats.Health:
                    health += statChange;
                    break;
                case Stats.Morale:
                    morale += statChange;
                    break;

                case Stats.Fortaleza:
                    fortaleza += statChange;
                    break;
                case Stats.Fuerza:
                    fuerza += statChange;
                    break;
                case Stats.Aguante:
                    aguante += statChange;
                    break;
                case Stats.Constitucion:
                    constitucion += statChange;
                    break;

                case Stats.Destreza:
                    destreza += statChange;
                    break;
                case Stats.Acrobacias:
                    acrobacias += statChange;
                    break;
                case Stats.Reaccion:
                    reaccion += statChange;
                    break;
                case Stats.Concentracion:
                    concentracion += statChange;
                    break;

                case Stats.Inteligencia:
                    inteligencia += statChange;
                    break;
                case Stats.Investigacion:
                    investigacion += statChange;
                    break;
                case Stats.Historia:
                    historia += statChange;
                    break;
                case Stats.Biologia:
                    biologia += statChange;
                    break;

                case Stats.Compenetracion:
                    compenetracion += statChange;
                    break;
                case Stats.Drama:
                    drama += statChange;
                    break;
                case Stats.Autoridad:
                    autoridad += statChange;
                    break;
                case Stats.Subconsciente:
                    subconsciente += statChange;
                    break;
            }
            UpdateUI();
        }

        /// <summary>
        /// Añade experiencia al stat y checkea si sube de nivel
        /// </summary>
        /// <param name="xp"></param>
        /// <param name="statToChange"></param>
        public void AddXP(int xp, Stats statToChange)
        {
            switch (statToChange)
            {
                case Stats.Fuerza:
                    fuerzaXP += xp;
                    if (fuerzaXP >= xpToLevel)
                    {
                        fuerza++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Aguante:
                    aguanteXP += xp;
                    if (aguanteXP >= xpToLevel)
                    {
                        aguante++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Constitucion:
                    constitucionXP += xp;
                    if (constitucionXP >= xpToLevel)
                    {
                        constitucion++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;

                case Stats.Acrobacias:
                    acrobaciasXP += xp;
                    if (acrobaciasXP >= xpToLevel)
                    {
                        acrobacias++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Reaccion:
                    reaccionXP += xp;
                    if (reaccionXP >= xpToLevel)
                    {
                        reaccion++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Concentracion:
                    concentracionXP += xp;
                    if (concentracionXP >= xpToLevel)
                    {
                        concentracion++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;

                case Stats.Investigacion:
                    investigacionXP += xp;
                    if (investigacionXP >= xpToLevel)
                    {
                        investigacion++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Historia:
                    historiaXP += xp;
                    if (historiaXP >= xpToLevel)
                    {
                        historia++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Biologia:
                    biologiaXP += xp;
                    if (biologiaXP >= xpToLevel)
                    {
                        biologia++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;

                case Stats.Drama:
                    dramaXP += xp;
                    if (dramaXP >= xpToLevel)
                    {
                        drama++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Autoridad:
                    autoridadXP += xp;
                    if (autoridadXP >= xpToLevel)
                    {
                        autoridad++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;
                case Stats.Subconsciente:
                    subconscienteXP += xp;
                    if (subconscienteXP >= xpToLevel)
                    {
                        subconsciente++;
                        AudioManager.instance.PlaySFX("LevelUp");
                        Debug.Log("Level Up: " + statToChange);
                    }
                    break;

                default:
                    Debug.Log("No hay xp para ese stat");
                    break;
            }
            UpdateUI();
        }

        /// <summary>
        /// Añade el tag pasado si no lo tiene ya
        /// </summary>
        /// <param name="tagToAdd"></param>
        public void AddTag(string tagToAdd)
        {
            if (tagToAdd != null && !LookForTag(tagToAdd))
            {
                tags.Add(tagToAdd);
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasStat":
                    //Devuelve true si parametro 1 (Stat que se pide) es mayor o igual al numero del parametro 2
                    return (GetValue(parameters[0]) >= int.Parse(parameters[1]));
                case "HasTag":
                    //Devuelve true si el Jugador tiene el Tag pedido
                    return LookForTag(parameters[0]);
            }
            return null;
        }

        public void UpdateUI()
        {
            SetBaseStats();
            statsChanged();
        }

        public object CaptureState()
        {
            return new StatSheetSaveData
            {
                health = health,
                morale = morale,
                fortaleza = fortaleza,
                fuerza = fuerza,
                fuerzaXP = fuerzaXP,
                aguante = aguante,
                aguanteXP = aguanteXP,
                constitucion = constitucion,
                constitucionXP = constitucionXP,
                destreza = destreza,
                acrobacias = acrobacias,
                acrobaciasXP = acrobaciasXP,
                reaccion = reaccion,
                reaccionXP = reaccionXP,
                concentracion = concentracion,
                concentracionXP = concentracionXP,
                inteligencia = inteligencia,
                investigacion = investigacion,
                investigacionXP = investigacionXP,
                historia = historia,
                historiaXP = historiaXP,
                biologia = biologia,
                biologiaXP = biologiaXP,
                compenetracion = compenetracion,
                drama = drama,
                dramaXP = dramaXP,
                autoridad = autoridad,
                autoridadXP = autoridadXP,
                subconsciente = subconsciente,
                subconscienteXP = subconscienteXP
            };
        }

        public void RestoreState(object state)
        {
            StatSheetSaveData data = state as StatSheetSaveData;
            if (data == null) return;

            health = data.health;
            morale = data.morale;

            fortaleza = data.fortaleza;
            fuerza = data.fuerza;
            fuerzaXP = data.fuerzaXP;
            aguante = data.aguante;
            aguanteXP = data.aguanteXP;
            constitucion = data.constitucion;
            constitucionXP = data.constitucionXP;

            destreza = data.destreza;
            acrobacias = data.acrobacias;
            acrobaciasXP = data.acrobaciasXP;
            reaccion = data.reaccion;
            reaccionXP = data.reaccionXP;
            concentracion = data.concentracion;
            concentracionXP = data.concentracionXP;

            inteligencia = data.inteligencia;
            investigacion = data.investigacion;
            investigacionXP = data.investigacionXP;
            historia = data.historia;
            historiaXP = data.historiaXP;
            biologia = data.biologia;
            biologiaXP = data.biologiaXP;

            compenetracion = data.compenetracion;
            drama = data.drama;
            dramaXP = data.dramaXP;
            autoridad = data.autoridad;
            autoridadXP = data.autoridadXP;
            subconsciente = data.subconsciente;
            subconscienteXP = data.subconscienteXP;

            UpdateUI();
        }
    }
}
