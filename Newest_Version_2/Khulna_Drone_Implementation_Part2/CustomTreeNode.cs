using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace Project
{
	internal class CustomTreeNode:TreeNode
	{
		public int marketPrice { get;private set; }
		public string Location { get;private set; }
		public int Depth { get;private set; }
		public int Item_Container_Amount { get;private set; }
		public int purchasePrice { get; private set; }
		public CustomTreeNode(string text, int marketPrice, string location, int depth, int amount,int purchasePrice) : base(text)
		{
			this.marketPrice = marketPrice;
			Location = location;
			Depth = depth;
			Item_Container_Amount = amount;
			this.purchasePrice = purchasePrice;
		}
		public void updateMarketPrice(int newPrice) { 
			this.marketPrice = newPrice;
		}
		public void updateDepth(int newDepth)
		{
			Depth = newDepth;
		}
		public void updateAmount(int newAmount) { 
			Item_Container_Amount=newAmount;
		}
		public void updatePurchasePrice(int newPrice)
		{
			this.purchasePrice = newPrice;
			
		}

	}
}
