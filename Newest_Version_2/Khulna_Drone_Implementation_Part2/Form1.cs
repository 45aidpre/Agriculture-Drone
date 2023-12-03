using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;
using System.Threading.Tasks;




namespace Project
{
    
    public partial class MainForm : Form
    {
        //Initiallize
        const int MainDroneLocationX = 133;
        const int MainDroneLocationY = 108;
        //private string fileName = "D:\\C#\\Project\\another.txt"; // Replace with your actual file name and path
        private string fileName = Path.Combine(Directory.GetCurrentDirectory(), "treedata.txt");
        //Load Main Form
        public MainForm()
        {
            InitializeComponent();
            this.Drone.BringToFront();
            this.ptbBarn.SendToBack();
            //Load TreeView from file
            FileOperations.LoadDataFromFile(tvItems, fileName);

            // Show the form after loading data
            Show();
        }


        private static MainForm instance;



        public static MainForm GetInstance()
        {

            {
                return instance ?? (instance = new MainForm());
            }
        }



        //------------------------------------Read/Write Function--------------------------------------------------

        // Save the Data to File

        //--------------------------------------------Function in Context Menu Strip---------------------------------------------


        //Show CMS when right click to item in Tree View
        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tvItems.SelectedNode != null && tvItems.SelectedNode.Nodes.Count > 0)

                    cmsCommandContainer.Show(Cursor.Position);
                else
                    cmsCommandOnItems.Show(Cursor.Position);
            }
        }

        //Context menu strip func: Rename
        private void renameMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.Rename(tvItems, fileName);
        }
        private void renameMenuContainer_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.Rename(tvItems, fileName);
        }


        //Pricing Function, check price and change price for item
        //Check current market value for container and items
        private void currentMarketValueMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.MarketValue(tvItems);
        }
        private void currentMarketValueMenuContainer_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.MarketValue(tvItems);
        }




        //Change price item
        private void changeMarketValueMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.ChangeMarketValue(tvItems, fileName);
        }


        //Purchase Price:
        private void purchasedPriceMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.PurchasedPrice(tvItems);
        }
        private void purchasedValueMenuContainer_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.PurchasedPrice(tvItems);
        }


        //Delete amount of item
        private void deleteItemMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.DeleteItem(tvItems, fileName);
        }
        //Add more item
        private void addItemMenuItem_Click(object sender, EventArgs e)
        {

            ContextMenuFunction.AddItem(tvItems, fileName);
        }
        //Current amount of item
        private void currentAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuFunction.CurrentAmount(tvItems);
        }

        private void btnScanFarm_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            void completeRight() //Move to the very right of the area
            {
                while (!(Drone.Location.X >= 600))
                {
                    for (int i = 1; i <= 2; i++)
                    {

                        Drone.Left += 1;

                    }
                }
            }

            void completeLeft() //Move to the very left of the area
            {
                while (!(Drone.Location.X < 10))
                {
                    for (int i = 1; i <= 2; i++)
                    {

                        Drone.Left -= 1;
                    }
                }
            }

            //-------------------------------------------------------------------------

            //First Scan
            this.Drone.BringToFront();

            completeRight();

            while (!(Drone.Location.Y >= 300))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top += 1;

                }
            }

            completeLeft();

            while (!(Drone.Location.Y >= 400))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top += 1;

                }
            }

            completeRight();

            while (!(Drone.Location.Y >= 500))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top += 1;

                }
            }

            completeLeft();

            while (!(Drone.Location.Y >= 600))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top += 1;

                }
            }

            completeRight();

            //Reverse back to origin
            

            completeLeft();

            while (!(Drone.Location.Y == 500))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top -= 1;

                }
            }

            completeRight();

            while (!(Drone.Location.Y == 400))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top -= 1;

                }
            }

            completeLeft();

            while (!(Drone.Location.Y == 300))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top -= 1;

                }
            }

            completeRight();

            while (!(Drone.Location.Y == 108))
            {
                for (int i = 1; i <= 2; i++)
                {

                    Drone.Top -= 1;

                }
            }
            while (!(Drone.Location.X < 125))
            {

                Drone.Left -= 1;
            }

            timer1.Stop();
        }

		private void btnVisitItem_Click(object sender, EventArgs e)
		{
            // Depending on selected item or container travel to those coordinates

            //Drone.Location.X = object.Location.X;
            //Drone.Location.Y = object.Location.Y;

            // Running the coordinates throught a manual format.
            //Scan Cows
            if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Cows")
            {
                while (Drone.Location.X <=ptbCow.Location.X)
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;
                }

                while ((Drone.Location.Y <= ptbCow.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                
                while ((Drone.Location.Y >= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;
                }
                while ((Drone.Location.X >= MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
            }

            //Scan Chicken
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Chicken")
            {
                while (!(Drone.Location.X >= ptbChicken.Location.X))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;
                }
                while (!(Drone.Location.Y >= ptbChicken.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                
                while (!(Drone.Location.Y <= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;
                }
                while (!(Drone.Location.X <= MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
            }
            //Scan Crops
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Crops")
            {
                while (!(Drone.Location.X >= ptbCrops.Location.X))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;
                }
                while (!(Drone.Location.Y >= ptbCrops.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                while (!(Drone.Location.Y <= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;
                }
                
                while (!(Drone.Location.X <= MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
                
            }
            //Scan MilkStorage
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Milk Storage")
            {

                
                while (!(Drone.Location.X <= ptbMilkStorage.Location.X))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
                while (!(Drone.Location.Y >= ptbMilkStorage.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                while (!(Drone.Location.Y <= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;
                }
                while (!(Drone.Location.X >=MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;
                }

            }
            //Scan Barn
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Barn")
            {
                while (!(Drone.Location.X <= ptbBarn.Location.X))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
                while (!(Drone.Location.Y >= ptbBarn.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                while (!(Drone.Location.Y <= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;

                }
                while (!(Drone.Location.X >= MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;

                }
            }
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Livestock Area")
            {
                while ((Drone.Location.X >= ptbLiveStock.Location.X))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left -= 1;
                }
                while ((Drone.Location.Y <= ptbLiveStock.Location.Y))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top += 1;
                }
                Thread.Sleep(2000);
                while (!(Drone.Location.X >= MainDroneLocationX))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Left += 1;
                }
                while (!(Drone.Location.Y <= MainDroneLocationY))
                {
                    for (int i = 1; i <= 2; i++)
                        Drone.Top -= 1;
                }
            }
            //Command center
            else if (this.visitItemBox.GetItemText(this.visitItemBox.SelectedItem) == "Command Center")
            {
                if (Drone.Location.X >= MainDroneLocationX)
                {
                    while (Drone.Location.X >= MainDroneLocationX)
                    {
                        for (int i = 1; i <= 2; i++)
                            Drone.Left -= 1;
                    }
                }
                else {
                    while (Drone.Location.X <= MainDroneLocationX)
                    {
                        for (int i = 1; i <= 2; i++)
                            Drone.Left += 1;
                    }
                }
                if (Drone.Location.Y >= MainDroneLocationY)
                {
                    while (Drone.Location.X >= MainDroneLocationX)
                    {
                        for (int i = 1; i <= 2; i++)
                            Drone.Top -= 1;
                    }
                }
                else
                {
                    while (Drone.Location.Y <= MainDroneLocationY)
                    {
                        for (int i = 1; i <= 2; i++)
                            Drone.Top += 1;
                    }
                }
                
            }
        }
	}
}
