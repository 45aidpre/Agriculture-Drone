using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	internal class FileOperations
	{
		//Functon Load Data From File. Load TreeView From File
		public static void LoadDataFromFile(TreeView tvItems,string fileName)
		{
			List<string> lines = new List<string>();
			try
			{
				// Clear existing nodes
				tvItems.Nodes.Clear();

				// Read lines from the file
				if (File.Exists(fileName))
				{
					//Try to read the line from file
					try
					{
						// Read the file and add each line to the list
						using (StreamReader reader = new StreamReader(fileName))
						{
							string line;
							while ((line = reader.ReadLine()) != null)
							{
								lines.Add(line);
							}
						}
					}
					//If can not read throw exception error.
					catch (Exception ex)
					{
						MessageBox.Show($"An error occurred: {ex.Message}");
					}

					// Maintain a stack to keep track of parent nodes
					Stack<CustomTreeNode> nodeStack = new Stack<CustomTreeNode>();

					// Populate TreeView with data from the file
					foreach (string line in lines)
					{
						string[] parts = line.Split(',');
						if (parts.Length == 6)
						{
							//Data read
							int depth = int.Parse(parts[0].Trim());
							string nodeText = parts[1].Trim();
							int marketPrice = int.Parse(parts[2].Trim());
							string location = parts[3].Trim();
							int itemAmount = int.Parse(parts[4].Trim());
							int purchasePrice = int.Parse(parts[5].Trim());
							CustomTreeNode newNode = new CustomTreeNode(nodeText, marketPrice, location, depth, itemAmount,purchasePrice);

							//Root will be at depth 0
							if (depth == 0)
							{
								tvItems.Nodes.Add(newNode);
								nodeStack.Push(newNode);

							}
							else
							{
								//Find node at peek, if depth is same level with peek, pop it out.
								CustomTreeNode parentNode = nodeStack.Peek();

								while (depth <= parentNode.Depth && depth != 0)
								{
									nodeStack.Pop();
									parentNode = nodeStack.Peek();
								}

								//When finish, add node to current parent node then push it to stack.
								parentNode.Nodes.Add(newNode);
								nodeStack.Push(newNode);
							}

						}

					}
				}
			}
			//Catch error
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}", "Error");
			}
		}

		public static void SaveDataToFile(TreeView tvItems,string fileName)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(fileName))
				{
					// Iterate through the nodes and save data to the file
					foreach (CustomTreeNode node in tvItems.Nodes)
					{
						SaveNodeToFile(node, sw);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error saving data to file: {ex.Message}", "Error");
			}
		}
		//Save Node in recursively
		public static void SaveNodeToFile(CustomTreeNode node, StreamWriter sw)
		{
			// Save the data for the current node
			string line = $"{node.Depth},{node.Text}, {node.marketPrice}, {node.Location},{node.Item_Container_Amount},{node.purchasePrice}";
			sw.WriteLine(line);

			// Recursively save child nodes
			foreach (CustomTreeNode childNode in node.Nodes)
			{
				SaveNodeToFile(childNode, sw);
			}



		}
	}
}
