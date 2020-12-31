using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using System;
using System.Collections.Generic;
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
            Submenu settingsSubmenu = addInRootSubmenu.Items.AddSubmenu("Settings - Not used currently");
            settingsSubmenu.Items.AddActionItemWithCheckBox<IEngineeringObject>("Check Box", _settings.CheckBoxOnClick, _settings.CheckBoxDisplayStatus);
            settingsSubmenu.Items.AddActionItemWithRadioButton<IEngineeringObject>("Radio Button 1", _settings.RadioButton1OnClick, _settings.RadioButton1DisplayStatus);
            settingsSubmenu.Items.AddActionItemWithRadioButton<IEngineeringObject>("Radio Button 2", _settings.RadioButton2OnClick, _settings.RadioButton2DisplayStatus);
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
            //Application.Run(new IOListInterface(_tiaPortal));

                   
            IOListInterface form1 = new IOListInterface(_tiaPortal);
            //Update title of form
            form1.Text = "IO Import Tool";
            //Place form on top of Portal Window
            form1.TopMost = true;
            form1.ShowDialog();
            
         
            

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
