using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	internal class ContextMenuFunction
	{

		//Rename Function
		public static void Rename(TreeView tvItems, string fileName)
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

		//Market Value
		public static void MarketValue(TreeView tvItems)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{
				MessageBox.Show($"Item: {selectedNode.Text}\nPrice:$ {selectedNode.marketPrice}\n", "Current Price");
			}
		}

		//Change Market Value
		public static void ChangeMarketValue(TreeView tvItems, string fileName)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;

			if (selectedNode != null)
			{

				string newPrice = Interaction.InputBox("Enter new price:", "Edit Price", selectedNode.marketPrice.ToString());
				if (!string.IsNullOrEmpty(newPrice))
				{
					int adjustedAmount = int.Parse(newPrice) - selectedNode.marketPrice;
					selectedNode.updateMarketPrice(int.Parse(newPrice));
					changeParentMarketValue(selectedNode, adjustedAmount);

					FileOperations.SaveDataToFile(tvItems, fileName);
					// Show updated information
				}
			}
		}

		//Change price for all parents
		private static void changeParentMarketValue(CustomTreeNode node, int adjustedAmount)
		{
			CustomTreeNode parentNode = node.Parent as CustomTreeNode;

			while (parentNode != null)
			{
				parentNode.updateMarketPrice(parentNode.marketPrice + adjustedAmount);
				parentNode = parentNode.Parent as CustomTreeNode;
			}
		}

		//Show Purchassed Price
		public static void PurchasedPrice(TreeView tvItems)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{
				MessageBox.Show($"Item: {selectedNode.Text}\nAmount: {selectedNode.Item_Container_Amount}\nPurchased Price: {selectedNode.purchasePrice}$\n", "Puchased Price: ");
			}
		}
		//Delete Item
		public static void DeleteItem(TreeView tvItems, string fileName)
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
					else
					{
						MessageBox.Show("Error: Amount invalid");
					}

				}
			}

		}
		//Add Item
		public static void AddItem(TreeView tvItems, string fileName)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{

				string newAmount = Interaction.InputBox("Current amount of " + selectedNode.Text + ": " + selectedNode.Item_Container_Amount + "\nEnter amount want to add:", "Edit Amount");
				string newPurchase = Interaction.InputBox("Enter the purchased price: ", "Purchased Amount");
				if (!string.IsNullOrEmpty(newAmount)&& !string.IsNullOrEmpty(newPurchase))
				{
					DialogResult dr = MessageBox.Show("Are you sure you want to add " + newAmount  + selectedNode.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dr == DialogResult.Yes)
					{
						
						selectedNode.updateAmount(selectedNode.Item_Container_Amount + int.Parse(newAmount));
						selectedNode.updatePurchasePrice(selectedNode.purchasePrice+ int.Parse(newPurchase));
						
						FileOperations.SaveDataToFile(tvItems, fileName);
					}

				}
				
			}
		}
		//Current Amount of Items
		public static void CurrentAmount(TreeView tvItems)
		{
			CustomTreeNode selectedNode = tvItems.SelectedNode as CustomTreeNode;
			if (selectedNode != null)
			{
				MessageBox.Show($"Item: {selectedNode.Text}\nAmount: {selectedNode.Item_Container_Amount}\n", "Current Price");
			}
		}

	}

}
