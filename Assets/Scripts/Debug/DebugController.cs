using RPG.Stats;
using System.Collections.Generic;
using UnityEngine;
using static RPG.Stats.StatSheet;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;
    Vector2 scroll;
    string input;

    public static DebugCommand HELP;
    public static DebugCommand RETARDED;
    public static DebugCommand LEVEL_UP;
    public List<object> commandList;

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    public void OnReturn()
    {
        if (showConsole)
        {
            HandleInputs();
            input = "";
        }
    }

    private void Awake()
    {
        HELP = new DebugCommand("help", "Muestra todos los comandos", "help", () => { showHelp = true; });

        RETARDED = new DebugCommand("retarded", "bla bla bla, ble ble ble", "retarded", () =>
        {
            Debug.Log("Retarded CALLED");
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Fuerza);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Aguante);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Constitucion);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Acrobacias);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Reaccion);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Concentracion);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Investigacion);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Historia);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Biologia);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Drama);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Autoridad);
            GetComponent<StatSheet>().SetStat(-1, StatSheet.Stats.Subconsciente);
            GetComponent<StatSheet>().UpdateUI();
        });

        LEVEL_UP = new DebugCommand("level_up", "Sube todas las habilidades 1 nivel", "level_up", () =>
        {
            Debug.Log("LEVELUP CALLED");
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Fuerza);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Aguante);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Constitucion);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Acrobacias);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Reaccion);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Concentracion);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Investigacion);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Historia);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Biologia);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Drama);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Autoridad);
            GetComponent<StatSheet>().SetStat(1, StatSheet.Stats.Subconsciente);
            GetComponent<StatSheet>().UpdateUI();
        });

        commandList = new List<object>
        {
            HELP,
            RETARDED,
            LEVEL_UP
        };
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = Color.magenta;
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width-20f, 20f), input);
    }

    private void HandleInputs()
    {
        string[] properties = input.Split(" ");

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandID))
            {
                if (commandList[i] as DebugCommand != null) 
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[i]));
                }
            }           
        }
    }
}
