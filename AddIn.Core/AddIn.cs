using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.HW;
using Siemens.Engineering.MC.Drives;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static SDRhelper.StartdriveHelper;
using AddIn.UI;
using AddIn.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QPADrv_DriveSettings
{
    public class AddIn : ContextMenuAddIn
    {
        ///The global TIA Portal Object 
        TiaPortal _tiaportal;

        String _logText;
        private readonly AddInController _controller;

        /// The display name of the Add-In.
        private const string s_DisplayNameOfAddIn = "QPADrv_DriveSettings";

        /// Represents the actual used TIA Portal process.
        public AddIn(TiaPortal tiaportal) : base(s_DisplayNameOfAddIn)
        {
            /*
             * The acutal TIA Portal process is saved in the 
             * global TIA Portal variable _tiaportal
             * tiaportal comes as input Parameter from the
             * AddInProvider
            */
            _tiaportal = tiaportal;
            _controller = new AddInController();
        }


        /// The Add-In will be displayed in 
        /// the Context Menu of TIA Portal.
        protected override void BuildContextMenuItems(ContextMenuAddInRoot
            addInRootSubmenu)
        {
            /* Method addInRootSubmenu.Items.AddActionItem
             * Will Create a Pushbutton with the text 'Start Add-In Code'
             * 1st input parameter of AddActionItem is the text of the 
             *          button
             * 2nd input parameter of AddActionItem is the clickDelegate, 
             *          which will be executed in case the button 'Start 
             *          Add-In Code' will be clicked/pressed.
             * 3rd input parameter of AddActionItem is the 
             *          updateStatusDelegate, which will be executed in 
             *          case there is a mouseover the button 'Start 
             *          Add-In Code'.
             * in <placeholder> the type of AddActionItem will be 
             *          specified, because AddActionItem is generic 
             * AddActionItem<DeviceItem> will create a button that will be 
             *          displayed if a rightclick on a DeviceItem will be 
             *          performed in TIA Portal
             * AddActionItem<Project> will create a button that will be 
             *          displayed if a rightclick on the project name 
             *          will be performed in TIA Portal
            */
            addInRootSubmenu.Items.AddActionItem<DeviceItem>(
                ("Start Add-In"), OnDoSomething, OnCanSomething);
            addInRootSubmenu.Items.AddActionItem<Project>(
                "Not Available here", OnClickProject,
                OnStatusUpdateProject);
        }

        /// <summary>
        /// The method contains the program code of the TIA Add-In.
        /// Called when the button 'Start Add-In Code' will be pressed.
        /// <para>MenuSelectionProvider DeviceItem menuSelectionProvider
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is DeviceItem
        /// </para>
        /// </summary>
        private void OnDoSomething(MenuSelectionProvider<DeviceItem>
            menuSelectionProvider)
        {
            _controller.ShowLogForm();
            _controller.logForm.BringToFront();

            //Get the actual selected DeviceItem from TIA Portal
            IEnumerable<DeviceItem> selection =
                menuSelectionProvider.GetSelection<DeviceItem>();

            _controller.SelectedDeviceAddIn(selection);
            //_controller.ProjectAddIn(_tiaportal);


            MainWindow myWindow = new MainWindow(_controller);
            myWindow.ShowDialog();
        }


        /// <summary>
        /// The method contains the Drive Parameter Interconnections.
        /// <para>DriveObject actDriveObject:
        /// the driveobject, on which a rightclick was performed in TIA
        /// </para>
        /// </summary>
        private MenuStatus OnCanSomething(MenuSelectionProvider
            <DeviceItem> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Enabled;
        }

        /// <summary>
        /// Will be called when the Add-In is started on the project level
        /// <para>MenuSelectionProvider Project menuSelectionProvider
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is Project
        /// </para>
        /// </summary>
        private void OnClickProject(MenuSelectionProvider<Project>
            menuSelectionProvider)
        {
            //Do Nothing on Project level
        }

        /// <summary>
        /// Called when there is a mousover the button at the Project 
        /// Level. It will be used to disable the button because no 
        /// action should be performed on project level.
        /// <para>MenuSelectionProvider Project menuSelectionProvider
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is Project
        /// </para>
        /// </summary>
        private MenuStatus OnStatusUpdateProject(MenuSelectionProvider
            <Project> menuSelectionProvider)
        {
            //disable the button
            return MenuStatus.Disabled;
        }
    }
}