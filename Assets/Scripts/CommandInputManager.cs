using UnityEditor.Scripting.Python;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using System.Diagnostics;

public class CommandInputManager : MonoBehaviour
{
    public GameObject commandPanel;      //  input panel
    public TMP_InputField inputField;    //  input field
    public TMP_Text texthere;        // assign Canvas to Text(TMP) here
    public PlayerMovement player;

    private bool isTyping = false;

    private bool isMatador = false;
    private bool isNapoleon = false;
    private bool isWoman = false;

    void Update()
    {
        if (!isTyping){
            isMatador = false;
            isNapoleon = false;
            isWoman = false;
            if(player.nearMatador)
            {
                EnterTypingMode();
                isMatador = true;
            }
            if(player.nearNapoleon)
            {
                EnterTypingMode();
                isNapoleon = true;
            }
            if(player.nearWoman)
            {
                EnterTypingMode();
                isWoman = true;
            }

        }
        else if (isTyping && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitCommand(inputField.text);
            ExitTypingMode();
        }
        else if(isTyping && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitTypingMode();
        }
    }

    void EnterTypingMode()
    {
        isTyping = true;
        commandPanel.SetActive(true);
        inputField.text = "";
        inputField.ActivateInputField();
        //player.canMove = false;
        player.isPanel = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ExitTypingMode()
    {
        isTyping = false;
        commandPanel.SetActive(false);
        //player.canMove = true;
        player.isPanel = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SubmitCommand(string command)
    {
        string scriptPath = "";

        UnityEngine.Debug.Log("Command entered: " + command);
        if(isMatador){
            scriptPath = Path.Combine(Application.dataPath,"Scripts/Matador/Matador.py");
        }
        if(isNapoleon)
        {
            scriptPath = Path.Combine(Application.dataPath,"Scripts/Napoleon/Napoleon.py");
        }
        if(isWoman)
        {
            scriptPath = Path.Combine(Application.dataPath,"Scripts/Woman/Woman.py");
        }
        PythonRunner.RunFile(scriptPath);


/*
        // Prepare and start the Python process
        //@"C:\Users\Landon\Downloads\Subot - Copy - Copy\main.py"
        string pythonScriptPath = "Assets/Scripts/Napoleon/Napoleon.py";
        var start = new ProcessStartInfo {
            FileName          = "python",
            Arguments         = $"\"{pythonScriptPath}\" \"{command}\"",
            UseShellExecute   = false,
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            CreateNoWindow    = true
        };

        try
        {
            using (var process = Process.Start(start))
            {
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();

                //  Log to console
                UnityEngine.Debug.Log("Python output: " + stdout);
                if (!string.IsNullOrEmpty(stderr))
                    UnityEngine.Debug.LogError("Python error: " + stderr);

                //  Display it on your TMP Text
                if (texthere != null)
                    texthere.text = stdout.Trim(); 
                else
                    UnityEngine.Debug.LogWarning("texthere not assigned!");
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Failed to run Python script: " + ex.Message);
        }
            */
    }

}
