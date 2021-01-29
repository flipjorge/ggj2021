using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FSMStateWizard : ScriptableWizard
{
    public string ownerType = "";
    public string stateName = "";
    private Object _selectionObject;

    [MenuItem("Assets/Create/FSM/State")]
    static void CreateWizard()
    {
        FSMStateWizard wizard = ScriptableWizard.DisplayWizard<FSMStateWizard>("Create State", "Create");
        wizard._selectionObject = Selection.activeObject;
        wizard.ownerType = FSMStateWizard.getOwnerType(wizard._selectionObject);
        //
        wizard.OnWizardUpdate();
        wizard.Focus();
    }

    void OnWizardCreate()
    {
        string folderPath = FSMStateWizard.getFolderPath(this._selectionObject);

        if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string stateFilename = ownerType + "State" + stateName + ".cs";
        string stateFilePath = Path.Combine(folderPath, stateFilename);
        string fileText = FSMStateWizard.getText(this.ownerType, this.stateName);

        File.WriteAllText(stateFilePath, fileText);

        AssetDatabase.Refresh();
    }

    void OnWizardUpdate()
    {
        this.isValid = false;
        this.helpString = "";
        this.errorString = "";

        if(this.ownerType.Length == 0 && this.stateName.Length == 0) this.helpString = "Please write the owner type and state name";
        else if(this.ownerType.Length == 0) this.helpString = "Please write the owner type";
        else if(this.stateName.Length == 0) this.helpString = "Please write the state name";
        else
        {
            string filePath = getFilePreviewPath(Selection.activeObject, this.ownerType, this.stateName);
            if(File.Exists(filePath)) this.errorString = "That state already exists";
            else
            {
                this.helpString = "Will be generated at \"" + filePath.Replace("/", "\\") + "\"";
                this.isValid = true;
            }
        }
    }

    #region Utils
    private static string getOwnerType(Object activeObject)
    {
        string selectionPath = AssetDatabase.GetAssetPath(activeObject);
        if(!AssetDatabase.IsValidFolder(selectionPath))
        {
            return Path.GetFileNameWithoutExtension(selectionPath);
        }

        return "";
    }

    private static string getFolderPath(Object activeObject)
    {
        string selectionPath = AssetDatabase.GetAssetPath(activeObject);

        if(AssetDatabase.IsValidFolder(selectionPath)) return selectionPath;
        else
        {
            string assetFolder = Path.GetDirectoryName(selectionPath);
            string assetStateFolder = Path.Combine(assetFolder, "States");

            return assetStateFolder;
        }
    }

    private static string getFilePreviewPath(Object activeObject, string ownerType, string stateName)
    {
        string folderPath = FSMStateWizard.getFolderPath(activeObject);
        string stateFilename = ownerType + "State" + stateName + ".cs";
        return Path.Combine(folderPath, stateFilename);
    }

    private static string getText(string ownerType, string stateName)
    {
        StringBuilder template = new StringBuilder();

        template.AppendLine("public class " + ownerType + "State" + stateName + " : FSMState<" + ownerType + ">");
        template.AppendLine("{");
        template.AppendLine("    #region Constructors");
        template.AppendLine("    public " + ownerType + "State" + stateName + "(FSM<" + ownerType + "> fsm) : base(fsm) {}");
        template.AppendLine("    #endregion");
        template.Append("}");

        return template.ToString();
    }
    #endregion
}