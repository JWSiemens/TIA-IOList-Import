using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Siemens.Engineering;
using Siemens.Engineering.Compiler;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Tags;

namespace TIA_Addin_IO_List_Import
{
    public class Util
    {
        public TiaPortal MyTiaPortal
        {
            get; set;
        }
        public Project MyProject
        {
            get; set;
        }

        public Util(TiaPortal tiaPortal)
        {
            

            //Move project info to the form
            MyTiaPortal = tiaPortal;
            MyProject = MyTiaPortal.Projects.First();
        }

        public void CreateHW(DataTable dt)
        {
            int numOfRows = dt.Rows.Count;
            int numOfColumns = dt.Columns.Count;
            List<string> ioNodes = new List<string>();
            List<string> ioNodeMasters = new List<string>();
            List<string> ioControllers_str = new List<string>();          


            //Device creation loop
            for (int i = 0; i < numOfRows - 1; i++)
            {
                //Collect part data, for it to work the MLFB takes this format: OrderNumber:MLFB#/FirmwareVersion
                string MLFB = "OrderNumber:" + (string)dt.Rows[i][7] + "/" + (string)dt.Rows[i][8];
                //Read io Type per io list
                string ioType = (string)dt.Rows[i][1];
                //create rack name
                string rackName = "";
                if (ioType != "PLC")
                    rackName = (string)dt.Columns[2].ColumnName + (string)dt.Rows[i][2];
                //Acquire IP, subnet mask, and default gateway
                string[] networkAddress = { "", "", "" }; //order of this array is: IPAddress, SubnetMask, Gateway(gateway only used for controllers)
                networkAddress[0] = (string)dt.Rows[i][9];   //IPAddress
                networkAddress[1] = (string)dt.Rows[i][10];  //Subnet Mask
                networkAddress[2] = (string)dt.Rows[i][11];  //Default Gateway


                switch (ioType)
                {
                    case "IM":

                        //add interface module
                        addDevice(MLFB, rackName, networkAddress, ioType);
                        //add rack name to list for network connections
                        ioNodes.Add(rackName);
                        //add the IO controller name to list of network connection lists
                        ioNodeMasters.Add((string)dt.Rows[i][5]);
                        break;

                    case "PLC":
                        //add interface module
                        string plcName = (string)dt.Rows[i][5];
                        addDevice(MLFB, plcName, networkAddress, ioType);
                        //add PLC to network connection lists
                        ioControllers_str.Add(plcName);
                        break;

                    default:
                        //Assuming IO card needs added
                        int slot = Convert.ToInt32(dt.Rows[i][3]);
                        string cardName = (string)dt.Rows[i][1] + slot;
                        //insert card into rack
                        addCard(MLFB, cardName, rackName, slot);
                        break;
                }

                //Increment progress bar, will be at 50 percent when this section is complete
                //if (i % 2 == 0)
                    //pb_HWProgress.Value = pb_HWProgress.Value + 1;

            }

            #region Connect to Network and create tag table
            //Create then connect new devices to network
            //Access project subnets
            SubnetComposition subnets = MyProject.Subnets;

            //Check to see if the subnet already exists, if not create it
            Subnet subnet1 = subnets.Find("subnet1");
            if (subnet1 == null)
                subnet1 = subnets.Create("System:Subnet.Ethernet", "subnet1");

            //Connect all created PLCs to subnet1, then create an IO System for each to allow connection to io
            foreach (string controller in ioControllers_str)
            {
                //define access PLC and network interface
                Device plc = MyProject.Devices.Find("station" + controller);
                var cpu = plc.DeviceItems.First(de => de.Name == controller);
                var profiCpuModul = cpu.DeviceItems.First(de => de.Name == "PROFINET interface_1");
                var profiCpuInterface = profiCpuModul.GetService<NetworkInterface>();
                var profiCpuNode = profiCpuInterface.Nodes.First(n => n.Name == "X1");

                //Connect to subnet
                profiCpuNode.ConnectToSubnet(subnet1);

                //Create IO system for controller
                IoSystem ioSystem = null;
                if ((profiCpuInterface.InterfaceOperatingMode & InterfaceOperatingModes.IoController) != 0)
                {
                    IoControllerComposition ioControllers = profiCpuInterface.IoControllers;
                    IoController ioController = ioControllers.First();

                    //debug code---------------------------------------------------
                    Console.WriteLine("The io controller (" + controller + ") has been selected");
                    //-------------------------------------------------------------

                    if (ioController != null)
                    {
                        ioSystem = ioController.CreateIoSystem("io system " + controller);


                        //Debug code ---------------------------------------------------
                        Console.WriteLine("The io system (" + controller + ") has been created");
                    }
                    else
                        Console.WriteLine("The io system was not created, check device name: " + controller);

                }


                //Create tag table for auto gen tags, to populate tags after nodes are connected to the IO system
                //Acquire access to software of PLC
                SoftwareContainer plcSoftwareContainer = cpu.GetService<SoftwareContainer>();
                PlcSoftware plcSoftware = plcSoftwareContainer.Software as PlcSoftware;
                //Verify that the plc software has been selected
                if (plcSoftware is PlcSoftware)
                    Console.WriteLine("The plc software has been selected of PLC Main");
                else
                    Console.WriteLine("Get fucked!!!, the selected object is not PLC software");

                PlcTagTable myTable = plcSoftware.TagTableGroup.TagTables.Create("autoGenTags");

            }
            #endregion


            //Connect all created IO Nodes and IO masters to subnet1 and then assign IO Nodes to associated controller
            for (int i = 0; i < ioNodes.Count; i++)
            {

                //connect IO module then set to be controlled by assigned plc
                //Define access to rack interfaces
                Device ioRack = MyProject.Devices.Find("station" + ioNodes[i]);
                var ioInterface = ioRack.DeviceItems.First(de => de.Name == ioNodes[i]);
                var profiModul = ioInterface.DeviceItems.First(de => de.Name == "PROFINET interface");
                var profiInterface = profiModul.GetService<NetworkInterface>();
                var profiIMNode = profiInterface.Nodes.First();

                //Connect rack to subnet
                profiIMNode.ConnectToSubnet(subnet1);

                //create io system name to connect to
                string ioSystemName = "io system " + ioNodeMasters[i];

                //define access to IO system
                Device plc = MyProject.Devices.Find("station" + ioNodeMasters[i]);
                DeviceItem cpu = plc.DeviceItems.First(de => de.Name == ioNodeMasters[i]);
                DeviceItem profiCpuModul = cpu.DeviceItems.First(de => de.Name == "PROFINET interface_1");
                NetworkInterface profiCpuInterface = profiCpuModul.GetService<NetworkInterface>();

                IoControllerComposition ioControllers = profiCpuInterface.IoControllers;
                IoController ioController = ioControllers.First();

                IoSystem ioSystem = ioController.IoSystem;

                //assign to ioController(io system)
                profiInterface.IoConnectors[0].ConnectToIoSystem(ioSystem);

            }

            //Increment progress bar by 12.5%
            //pb_HWProgress.Value = pb_HWProgress.Value + numOfRows / 8;


            //Create tags for all IO points on the IO list
            for (int i = 0; i < numOfRows - 1; i++)
            {
                string ioTypeIoList = (string)dt.Rows[i][1];

                //Check to see if current line is an IO channel, if not skip this section
                if (ioTypeIoList == "DI" || ioTypeIoList == "DO" || ioTypeIoList == "AI" || ioTypeIoList == "AO")
                {
                    //Acquire current slot per the IO list
                    string slot_str = (string)dt.Rows[i][3];
                    int slot = Int32.Parse(slot_str);
                    //Acquire data and access to create tags 
                    //Rack access
                    string rackName = (string)dt.Columns[2].ColumnName + (string)dt.Rows[i][2];
                    Device ioRack = MyProject.Devices.Find("station" + rackName);
                    //io card access
                    DeviceItem ioCard = ioRack.DeviceItems[slot + 2];
                    DeviceItem ioCardInterface = ioCard.DeviceItems.First();
                    Address addressComp = ioCardInterface.Addresses.First();
                    String plcName = (string)dt.Rows[i][5];
                    //plc access
                    Device plc = MyProject.Devices.Find("station" + plcName);
                    DeviceItem cpu = plc.DeviceItems.First(de => de.Name == plcName);
                    //software access
                    SoftwareContainer plcSoftwareContainer = cpu.GetService<SoftwareContainer>();
                    PlcSoftware plcSoftware = plcSoftwareContainer.Software as PlcSoftware;
                    //Verify that the plc software has been selected
                    if (plcSoftware is PlcSoftware)
                        Console.WriteLine("The plc software has been selected of PLC Main");
                    else
                        Console.WriteLine("The object selected is not PLC software");

                    //read startAddress
                    int startAddress = (int)addressComp.StartAddress;
                    string channelAddress = "";
                    int channelSize = 0;

                    //Acquire tag info
                    int channel = Int32.Parse((string)dt.Rows[i][4]);
                    string tagname = (string)dt.Rows[i][0];
                    string typeName = (string)ioCard.GetAttribute("TypeName");
                    int cardBitLength = (int)addressComp.Length;
                    string ioType = typeName.Substring(0, 2);
                    int numChannels = Int32.Parse(typeName.Substring(2, 2));


                    //Find the created tag table
                    PlcTagTable autoGenTagTable = plcSoftware.TagTableGroup.TagTables.Find("autoGenTags");
                    //tagComposition composes tags...
                    PlcTagComposition tagComposition = autoGenTagTable.Tags;
                    string dataType = "";
                    int channelStartAddress = channelSize * channel;
                    int numBytes = channelStartAddress / 8;
                    int bitAdjust = channelStartAddress - (numBytes * 8);

                    switch (ioType)
                    {
                        case "DI":
                            dataType = "Bool";
                            if (numChannels <= 8 || channel <= 8)
                                channelAddress = "%I" + startAddress + "." + channel;
                            else if (channel <= 16)
                            {
                                startAddress = startAddress + 1;
                                channel = channel - 8;
                                channelAddress = "%I" + startAddress + "." + channel;
                            }
                            break;

                        case "DQ":
                            dataType = "Bool";
                            if (numChannels <= 8 || channel <= 8)
                                channelAddress = "%Q" + startAddress + "." + channel;
                            else if (channel <= 16)
                            {
                                startAddress = startAddress + 1;
                                channel = channel - 8;
                                channelAddress = "%Q" + startAddress + "." + channel;
                            }
                            break;

                        case "AI":
                            dataType = "Word";
                            //calculate channel size (AI and AO are ususally 1 word but doing the calculation to make sure)
                            channelSize = cardBitLength / numChannels;
                            //Calculate the byte address of the channel
                            channelStartAddress = channelSize * channel;
                            //find number of bytes from start bit address
                            numBytes = channelStartAddress / 8;

                            //Offset start address for channel
                            startAddress = startAddress + numBytes;
                            //Combine address for tag command
                            channelAddress = "%IW" + startAddress;
                            break;

                        case "AQ":
                            dataType = "Word";
                            //calculate channel size (AI and AO are ususally 1 word but doing the calculation to make sure)
                            channelSize = cardBitLength / numChannels;
                            //Calculate the byte address of the channel
                            channelStartAddress = channelSize * channel;
                            //find number of bytes from start bit address
                            numBytes = channelStartAddress / 8;

                            //Offset start address for channel
                            startAddress = startAddress + numBytes;
                            //Combine address for tag command
                            channelAddress = "%QW" + startAddress;
                            break;

                    }

                    //Create tag
                    tagComposition.Create(tagname, dataType, channelAddress);
                    Console.WriteLine(tagname + " tag has been created and is assigned to: " + channelAddress);

                    //increm
                }
                //increment progress bar
                //if (i % 4 == 0)
                    //pb_HWProgress.Value = pb_HWProgress.Value + 1;

            }

            //Compile hardware for each controller
            foreach (string ioController in ioControllers_str)
            {
                //Define Access to ioCOntrollers
                Device plc = MyProject.Devices.Find("station" + ioController);
                DeviceItem cpu = plc.DeviceItems.First(de => de.Name == ioController);

                //Access compilable part of controller and compile hardware
                ICompilable compileService = plc.GetService<ICompilable>();
                CompilerResult result = compileService.Compile();
                //Capture compile data
                LogCompileData(result, ioController);

            }
        }


        public void addDevice(string MLFB, string Name, string[] NetworkAddress, string IoType)
        {
            bool found = false;
            string devName = "station" + Name;


            //the for loop below checks to see if there is another PLC, HMI, or rack in the project with the same name that is trying to be added, 

            foreach (Device device in MyProject.Devices)
            {
                DeviceItemComposition deviceItemAggregation = device.DeviceItems;
                foreach (DeviceItem deviceItem in deviceItemAggregation)
                {
                    if (deviceItem.Name == devName || device.Name == devName || deviceItem.Name == Name || device.Name == Name)
                        found = true;

                }
            }
            if (found == true)
            {
                Console.WriteLine("Device already exists");
            }
            else
            {
                //Create device
                Device device = MyProject.Devices.CreateWithItem(MLFB, Name, devName);
                //Assign IP, subnet mask, and default gateway if applicable
                //Access network interface
                DeviceItem cpu = device.DeviceItems.First(de => de.Name == Name);
                String profinetInterfaceName = "";
                //The default profinet name changes depending on the devicetype, so check the type then access the interface with the correct name
                //This part of the code will most likely need updated as this assumption of rack vs PLC is weak, it has more to do with 
                //how many interface the device at hand has. So to improve this I believe I need to list the deviceItems so that I can select from them
                if (IoType == "PLC")
                    profinetInterfaceName = "PROFINET interface_1";
                else
                    profinetInterfaceName = "PROFINET interface";
                DeviceItem profiModul = cpu.DeviceItems.First(de => de.Name == profinetInterfaceName);
                NetworkInterface profiInterface = profiModul.GetService<NetworkInterface>();
                //Assign IP
                profiInterface.Nodes.First().SetAttribute("Address", NetworkAddress[0]);
                //Assign subnet mask
                profiInterface.Nodes.First().SetAttribute("SubnetMask", NetworkAddress[1]);
                //Assign gateway if needed
                if (NetworkAddress[2] != "" && NetworkAddress[2] != "N/A")
                {
                    //Set "Use router" checkbox to allow gateway to be used
                    profiInterface.Nodes.First().SetAttribute("UseRouter", true);
                    //Set gateway address
                    profiInterface.Nodes.First().SetAttribute("SubnetMask", NetworkAddress[2]);
                }


                Console.WriteLine("Add Device Name: " + Name + " with " + MLFB.Substring(0, MLFB.Length - 5) + " and Firmware Version: " + MLFB.Substring(MLFB.Length - 5));
                Console.WriteLine(Name + " was created");
            }
        }

        //JW created function
        public void addCard(string MLFB, string CardName, string RackName, int Slot)
        {
            try
            {
                //find rack to add card to
                //Create container name
                string devName = "station" + RackName;
                //set var to be the device
                Device distIO = MyProject.Devices.Find(devName);
                //pull container location from device and set to var
                DeviceItem rack = distIO.DeviceItems.First(de => de.Name == "Rack_0");

                // check if card can be added, if so add it
                if (rack.CanPlugNew(MLFB, CardName, Slot))
                    rack.PlugNew(MLFB, CardName, Slot);
                else
                    Console.WriteLine("Module will not allow card to be plugged into slot " + Slot +
                        ". This is either another channel of an existing card or there is an incompatibility with the part");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The program has stopped, an error occured: " + ex.ToString());
                throw;
            }
        }

        private void LogCompileData(CompilerResult result, string IoController)
        {
            string path = "H:\\HWCompile " + IoController;

            using (StreamWriter log = File.CreateText(path))
            {
                log.WriteLine("State:" + result.State);
                log.WriteLine("Warning Count:" + result.WarningCount);
                log.WriteLine("Error Count:" + result.ErrorCount);
                RecursivelyWriteMessages(result.Messages, log);
            }

            Console.WriteLine("State:" + result.State);
            Console.WriteLine("Warning Count:" + result.WarningCount);
            Console.WriteLine("Error Count:" + result.ErrorCount);

        }
        private void RecursivelyWriteMessages(CompilerResultMessageComposition messages, StreamWriter Log, string
        indent = "")
        {
            indent += "\t";
            foreach (CompilerResultMessage message in messages)
            {
                Log.WriteLine(indent + "Path: " + message.Path);
                Log.WriteLine(indent + "DateTime: " + message.DateTime);
                Log.WriteLine(indent + "State: " + message.State);
                Log.WriteLine(indent + "Description: " + message.Description);
                Log.WriteLine(indent + "Warning Count: " + message.WarningCount);
                Log.WriteLine(indent + "Error Count: " + message.ErrorCount);
                RecursivelyWriteMessages(message.Messages, Log, indent);
            }
        }
    }

}
