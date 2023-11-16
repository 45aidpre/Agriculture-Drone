using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Project
{
	public partial class MainForm : Form
	{
		//Initiallize
		//private string fileName = "D:\\C#\\Project\\another.txt"; // Replace with your actual file name and path
		private string fileName = Path.Combine(Directory.GetCurrentDirectory(), "treedata.txt");
		//Load Main Form
		public MainForm()
		{
			InitializeComponent();

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
				cmsCommandOnItems.Show(Cursor.Position);
			}
		}

		//Context menu strip func: Rename
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{

			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;

			if (selectedNode != null)
			{
				string text = selectedNode.Text;
				string newName = Interaction.InputBox("Enter new name:", "Edit Name", text);
				if (!string.IsNullOrEmpty(newName))
				{
					selectedNode.Text = newName;
					FileOperations.SaveDataToFile(tvItems, fileName);
					// Show updated information
				}
			}

		}
		//Pricing Function, check price and change price
		private void currentPriceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{
				MessageBox.Show($"Item: {selectedNode.Text}\nPrice:$ {selectedNode.Price}\n", "Current Price");
			}
		}
		//Change price item
		private void changePriceToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;

			if (selectedNode != null)
			{

				string newPrice = Interaction.InputBox("Enter new price:", "Edit Price", selectedNode.Price.ToString());
				if (!string.IsNullOrEmpty(newPrice))
				{
					selectedNode.updatePrice(int.Parse(newPrice));
					FileOperations.SaveDataToFile(tvItems, fileName);
					// Show updated information
				}
			}
		}
		//Delete amount of item
		private void deleteLivestockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{

				string amountDelete = Interaction.InputBox("Current amount of " + selectedNode.Text + ": " + selectedNode.Item_Container_Amount + "\nEnter amount want to delete:", "Edit Name");
				if (!string.IsNullOrEmpty(amountDelete))
				{
					int temp = selectedNode.Item_Container_Amount - int.Parse(amountDelete);
					if (temp >= 0)
					{
						DialogResult dr = MessageBox.Show("Are you sure you want to delete " + amountDelete + selectedNode.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
						if (dr == DialogResult.Yes)
						{

							selectedNode.updateAmount(temp);
							FileOperations.SaveDataToFile(tvItems, fileName);
						}
					}
					else {
						MessageBox.Show("Error: Amount invalid");
					}
					
				}
			}
		}
		//Add more item
		private void addToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{

				string newAmount = Interaction.InputBox("Current amount of " + selectedNode.Text + ": " + selectedNode.Item_Container_Amount + "\nEnter amount want to add:", "Edit Name");
				if (!string.IsNullOrEmpty(newAmount))
				{
					DialogResult dr = MessageBox.Show("Are you sure you want to add " + newAmount + " more " + selectedNode.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dr == DialogResult.Yes)
					{
						int temp = selectedNode.Item_Container_Amount + int.Parse(newAmount);
						selectedNode.updateAmount(temp);
						FileOperations.SaveDataToFile(tvItems, fileName);
					}

					// Show updated information
				}
			}
		}
		//Current amount of item
		private void currentAmountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{
				MessageBox.Show($"Item: {selectedNode.Text}\nAmount: {selectedNode.Item_Container_Amount}\n", "Current Price");
			}
		}

		//------------------------------------------------Scan Farm-----------------------------------------





		private void btnScanFarm_Click(object sender, EventArgs e)
		{
			timer1.Start();
		}

		private void btnVisitItem_Click(object sender, EventArgs e)
		{
			// Depending on selected item or container travel to those coordinates
			// Drone.Location.X = object.Location.X;
			// Drone.Location.Y = object.Location.Y;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			ScanFarm();
			timer1.Stop();
		}

		private void MoveDrone(int targetX, int targetY)
		{
			while (ptbDrone.Location.X != targetX || ptbDrone.Location.Y != targetY)
			{
				int xDirection = targetX > ptbDrone.Location.X ? 1 : -1;
				int yDirection = targetY > ptbDrone.Location.Y ? 1 : -1;

				ptbDrone.Left += xDirection * 2;
				ptbDrone.Top += yDirection * 2;
			}
		}

		private void ScanFarm()
		{
			MoveDrone(550, 200);
			MoveDrone(10, 300);
			MoveDrone(550, 400);
			MoveDrone(10, 500);
			MoveDrone(550, 50);

			// Reverse back to origin
			MoveDrone(10, 500);
			MoveDrone(550, 400);
			MoveDrone(10, 300);
			MoveDrone(550, 200);
			MoveDrone(50, 50);
		}

	}
}

