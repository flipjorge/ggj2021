using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FSMWizard : ScriptableWizard
{
    public string ownerType = "";
    private Object _selectionObject;

    [MenuItem("Assets/Create/FSM/FSM")]
    static void CreateWizard()
    {
        FSMWizard wizard = ScriptableWizard.DisplayWizard<FSMWizard>("Create FSM", "Create");
        wizard._selectionObject = Selection.activeObject;
        wizard.ownerType = FSMWizard.getOwnerType(wizard._selectionObject);
        //
        wizard.OnWizardUpdate();
        wizard.Focus();
    }

    void OnWizardCreate()
    {
        string folderPath = FSMWizard.getFolderPath(this._selectionObject);

        if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string fsmFilename = ownerType + "FSM.cs";
        string fsmFilePath = Path.Combine(folderPath, fsmFilename);
        string fileText = FSMWizard.getText(this.ownerType);

        File.WriteAllText(fsmFilePath, fileText);

        AssetDatabase.Refresh();
    }

    void OnWizardUpdate()
    {
        this.isValid = false;
        this.helpString = "";
        this.errorString = "";

        if(this.ownerType.Length == 0) this.helpString = "Please write the owner type";
        else if(this.ownerType.Length == 0) this.helpString = "Please write the owner type";
        else
        {
            string filePath = getFilePreviewPath(Selection.activeObject, this.ownerType);
            if(File.Exists(filePath)) this.errorString = "That FSM already exists";
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
        else return Path.GetDirectoryName(selectionPath);
    }

    private static string getFilePreviewPath(Object activeObject, string ownerType)
    {
        string folderPath = FSMWizard.getFolderPath(activeObject);
        string stateFilename = ownerType + "FSM.cs";
        return Path.Combine(folderPath, stateFilename);
    }

    private static string getText(string ownerType)
    {
        StringBuilder template = new StringBuilder();

        template.AppendLine("public class " + ownerType + "FSM : FSM<" + ownerType + ">");
        template.AppendLine("{");
        template.AppendLine("    #region Constructor");
        template.AppendLine("    public " + ownerType + "FSM(" + ownerType + " owner) : base(owner)");
        template.AppendLine("    {");
        template.AppendLine("       this._createStates();");
        template.AppendLine("       this._connectStates();");
        template.AppendLine("    }");
        template.AppendLine("    #endregion");
        template.AppendLine("    ");
        template.AppendLine("    #region States");
        template.AppendLine("    //e.g. public StateType nameState;");
        template.AppendLine("    #endregion");
        template.AppendLine("    ");
        template.AppendLine("    #region Create States");
        template.AppendLine("    private void _createStates()");
        template.AppendLine("    {");
        template.AppendLine("       //e.g. nameState = new StateType(this)");
        template.AppendLine("    }");
        template.AppendLine("    #endregion");
        template.AppendLine("    ");
        template.AppendLine("    #region Connect States");
        template.AppendLine("    private void _connectStates()");
        template.AppendLine("    {");
        template.AppendLine("       //e.g. nameState.onEventName = this.otherState");
        template.AppendLine("    }");
        template.AppendLine("    #endregion");
        template.Append("}");

        return template.ToString();
    }
    #endregion
}