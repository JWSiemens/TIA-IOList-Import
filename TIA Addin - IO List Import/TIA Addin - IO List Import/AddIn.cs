using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace TIA_Addin_IO_List_Import
{
    public class AddIn : ContextMenuAddIn
    {
        private readonly TiaPortal _tiaPortal;
        private readonly Settings _settings;

        public AddIn(TiaPortal tiaPortal) : base("IO Import")
        {
            _tiaPortal = tiaPortal;
            _settings = Settings.Load();
        }

        protected override void BuildContextMenuItems(ContextMenuAddInRoot addInRootSubmenu)
        {
            addInRootSubmenu.Items.AddActionItem<IEngineeringObject>("Run IO Import", OnClick, DisplayStatus);            
        }

        private void OnClick(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            // TODO: Replace this with your own on click logic

            //Bring in project data and selected items to work with
            string projectName = _tiaPortal.Projects.First(project => project.IsPrimary).Name;
            List<IEngineeringObject> selectedObjects = menuSelectionProvider.GetSelection<IEngineeringObject>().ToList();
            string selectedObjectNames = string.Join(Environment.NewLine, selectedObjects.Select(selection => (string)selection.GetAttribute("Name")));


            
            //Open form and pass current TIA project to app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Creat var for datatable
            DataTable dt = new DataTable();

            using (IOListInterface form1 = new IOListInterface(_tiaPortal))
            {
                //Update title of form
                form1.Text = "IO Import Tool";
                //Place form on top of Portal Window
                form1.TopMost = true;
                var result = form1.ShowDialog();
                if (result == DialogResult.OK)
                {                   
                    dt = form1.dataTable;
                    MessageBox.Show("The datatable transfer just happened the first header is: " + dt.Columns[0].ColumnName);
                }
            }

            //Create datatable to compare to the import to make sure the correct headers are used
            DataTable compareDT = new DataTable();
            compareDT.Columns.Add("tagname");   //during the transfer the text changes to all lower case
            
            compareDT.Columns.Add("io type");
            compareDT.Columns.Add("rack");
            compareDT.Columns.Add("slot");
            compareDT.Columns.Add("channel");
            compareDT.Columns.Add("plc");
            compareDT.Columns.Add("signal type");
            compareDT.Columns.Add("part number");
            compareDT.Columns.Add("firmware version");
            compareDT.Columns.Add("ip address");
            compareDT.Columns.Add("subnet mask");
            compareDT.Columns.Add("default gateway");

            for (int i = 0; i<=11; i++)
            {
                if (compareDT.Columns[i].ColumnName != dt.Columns[i].ColumnName)
                {
                    MessageBox.Show("Column headers do not match, check headers on io list\n" +
                        "the column name on the import is: " + dt.Columns[i].ColumnName +
                        "It should be: " +  compareDT.Columns[i].ColumnName);
                    return;
                }
            }


            if (dt.Rows.Count > 0)
            {
                Util utility = new Util(_tiaPortal);
                utility.CreateHW(dt);
            }
            else
                MessageBox.Show("No data available");
                        
        }

        private MenuStatus DisplayStatus(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            return MenuStatus.Enabled;
        }

        //Give access to add-in tester
        public IEnumerable<IEngineeringObject> GetSelection(string label)
        {
            var selection = new List<IEngineeringObject>();
            var myProject = _tiaPortal.Projects.First();

            if (myProject != null)
                selection.Add(myProject);
            // Add items to selection

            return selection;
        }

    }
}
