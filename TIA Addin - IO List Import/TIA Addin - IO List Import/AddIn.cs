using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
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

            //Define access to utility functions
            Util utility = new Util(_tiaPortal);

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
            compareDT.Columns.Add("Tagname");   //during the transfer the text changes to all lower case            
            compareDT.Columns.Add("IO Type");
            compareDT.Columns.Add("Rack");
            compareDT.Columns.Add("Slot");
            compareDT.Columns.Add("Channel");
            compareDT.Columns.Add("PLC");
            compareDT.Columns.Add("Signal Type");
            compareDT.Columns.Add("Part Number");
            compareDT.Columns.Add("Firmware Version");
            compareDT.Columns.Add("IP Address");
            compareDT.Columns.Add("Subnet Mask");
            compareDT.Columns.Add("Default Gateway");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= 11; i++)
                {
                    if (compareDT.Columns[i].ColumnName != dt.Columns[i].ColumnName)
                    {
                        MessageBox.Show("Column headers do not match, check headers on io list\n" +
                            "the column name on the import is: " + dt.Columns[i].ColumnName +
                            "It should be: " + compareDT.Columns[i].ColumnName);
                        return;
                    }
                }


                //Create variables for creating and interconnecting devices
                int numOfRows = dt.Rows.Count;
                int numOfColumns = dt.Columns.Count;
                List<string>[] ioNodes = new List<string>[2];                
                List<string> ioControllers_str = new List<string>();

                //Create PLCs and retain list of them
                ioControllers_str = utility.CreatePLCs(dt);
                //Create Profinet IM nodes and retain a list of nodes and node masters
                ioNodes = utility.CreateIMs(dt);                //ioNodes consists of IMs in [0] and IO node masters in [1]
                //Add IO Cards
                utility.SortAndAddCards(dt);
                //Create and interconnect networks/IO Systems
                utility.CreateNetworks(ioControllers_str, ioNodes);

                //Create IO tags
                utility.CreateIOTags(dt);
                //Compile each controller
                foreach (string controller in ioControllers_str)
                {
                    utility.CompilePLCs(controller);
                }
                
            }
            else
            {
                MessageBox.Show("No data available");
            }                       
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

        private void callLoadingBar()
        {
            LoadingBar loadingBarForm = new LoadingBar();
            loadingBarForm.Text = "IO Import Progress";
            loadingBarForm.TopMost = true;
            loadingBarForm.ShowDialog();
        }

    }
}
