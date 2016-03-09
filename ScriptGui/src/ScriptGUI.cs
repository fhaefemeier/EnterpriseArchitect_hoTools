﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using hoTools.ActiveX;

using System.Collections.Generic;
using EAAddinFramework.Utils;



namespace hoTools.Scripts
{
    /// <summary>
    /// ActiveX COM Component 'hoTools.ScriptGUI' to show as tab in the EA Addin window
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("23722D72-7C6A-4246-B8B8-8D421CEBCD65")]
    [ProgId(PROGID)]
    [ComDefaultInterface(typeof(IScriptGUI))]
    public partial class ScriptGUI : AddinGUI, IScriptGUI
    {
        public const string PROGID = "hoTools.ScriptGUI";
        public const string TABULATOR = "Scripts";

        List<Script> _lscripts = null;  // list off all scripts
        DataTable _tableFunctions = null; // Scripts and Functions


        #region Constructor
        public ScriptGUI()
        {
            InitializeComponent();

            initDataGrid();
            initDataTable();

        }
        #endregion
        #region initDataGrid
        private void initDataGrid()
        {
            dataGridViewScripts.AutoGenerateColumns = false;

            dataGridViewScripts.DataSource = null;

            DataGridViewTextBoxColumn col;

            /*
            // to harbour the Script object
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ScriptObj";
            col.Visible = false;
            dataGridViewScripts.Columns.Add(col);

            // to harbour the ScriptFunction object
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "FunctionObj";
            col.Visible = false;
            dataGridViewScripts.Columns.Add(col);
            */


            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Script";
            col.Name = "Script";
            col.HeaderText = "Script";
            dataGridViewScripts.Columns.Add(col);


            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Language";
            col.Name = "Language";
            col.HeaderText = "Language";
            dataGridViewScripts.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Group";
            col.Name = "Group";
            col.HeaderText = "Group";
            dataGridViewScripts.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Err";
            col.Name = "Err";
            col.HeaderText = "Err";
            dataGridViewScripts.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Function";
            col.Name = "Function";
            col.HeaderText = "Function";
            dataGridViewScripts.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ParCount";
            col.Name = "ParCount";
            col.HeaderText = "Par count";
            dataGridViewScripts.Columns.Add(col);
        }
        #endregion
        #region initDataTable
        private void initDataTable()
        {
            dataGridViewScripts.DataSource = null;
            _tableFunctions = new DataTable();
            DataColumn functionScriptObj = new DataColumn("ScriptObj", typeof(Script));
            DataColumn functionFunctionObj = new DataColumn("FunctionObj", typeof(ScriptFunction));
            DataColumn functionScriptColumn = new DataColumn("Script", typeof(string));
            DataColumn functionLanguageColumn = new DataColumn("Language", typeof(string));
            DataColumn functionGroupColumn = new DataColumn("Group", typeof(string));
            DataColumn functionErrColumn = new DataColumn("Err", typeof(string));
            DataColumn functionFunctionColumn = new DataColumn("Function", typeof(string));
            DataColumn functionParCountColumn = new DataColumn("ParCount", typeof(int));
            // add columns
            _tableFunctions.Columns.AddRange(new DataColumn[]
                {
                    functionScriptObj,
                    functionFunctionObj,
                    functionScriptColumn,
                    functionLanguageColumn,
                    functionGroupColumn,
                    functionErrColumn,
                    functionFunctionColumn,
                    functionParCountColumn
                }
                );
        }
        #endregion


        // Interface IScriptGUI implementation
        public string getName() => "hoTools.ScriptGUI";

        public override EA.Repository Repository
        {
            set
            {
                base.Repository = value;
                // only if there is a repository available
                if (value.ProjectGUID != "")
                {
                    _lscripts = Script.getEAMaticScripts(Model);
                    updateTableFunctions();
                }
            }
        }


        private void ScriptGUI_Load(object sender, EventArgs e)
        {
         
        }

       
        /// <summary>
        /// Load all usable script
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadScripts_Click(object sender, EventArgs e)
        {
            _lscripts = Script.getEAMaticScripts(Model);
            updateTableFunctions();
        }

        void updateTableFunctions()
        {
            _tableFunctions.Rows.Clear();
            // fill list
            foreach (Script script in _lscripts)
            {
                DataRow newRow = _tableFunctions.NewRow();
                newRow["ScriptObj"] = script;
                newRow["Script"] = script.name;
                newRow["Language"] = script.languageName;
                newRow["Group"] = script.groupName;
                newRow["Err"] = script.errorMessage;
                foreach (ScriptFunction function in script.functions)
                {
                    newRow["FunctionObj"] = function;
                    newRow["Function"] = function.name;
                    newRow["ParCount"] = function.numberOfParameters;
                }
                _tableFunctions.Rows.Add(newRow);
            }
            // bind to grid
            // Select column to view
            dataGridViewScripts.DataSource = _tableFunctions;

        }

        /// <summary>
        /// Run the script under the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunScript_Click(object sender, EventArgs e)
        {
            //DataRow[] rowToRun = _tableFunctions.Select();
            foreach (DataGridViewRow row in this.dataGridViewScripts.SelectedRows)
            {
                DataRowView rowToRunView = row.DataBoundItem as DataRowView;
                DataRow rowToRun = rowToRunView.Row;
                var scriptName = rowToRun["Script"] as string;
                var language = rowToRun["Language"] as string;
                var functionName = rowToRun["Function"] as string;
                var parCount = (int)rowToRun["ParCount"];
                var script = rowToRun["ScriptObj"] as Script;
                var scriptFunction = rowToRun["FunctionObj"] as ScriptFunction;

                string s = $"Fullname '{scriptName}' Function:'{ functionName} NumberOfParameters:{parCount}";
                MessageBox.Show(s, "Script:" + script.displayName);
                object[] par = { script, "2222" };
                var par1 = par[0] as Script;
                string name = par1.name;
                string par2 = par[1] as string;
                try
                {
                    // run Script
                    
                    scriptFunction.execute(par);
                    
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString(), $"Error run Script:{scriptName}:{functionName}()") ;
                }

            }
        }
    }
}