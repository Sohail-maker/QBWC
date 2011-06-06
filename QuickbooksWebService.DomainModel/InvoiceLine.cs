using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickbooksWebService.DomainModel
{
	public class InvoiceLine
	{
		/// <summary>
		/// ItemRef
		/// </summary>
		public string Desc{get;set;}
		public string InventoryName{get;set;}
		/// <summary>
		/// Quickbooks takes Quantity and Amount and Calculate total for order.
		/// </summary>
		public int? Quantity{get;set;}
		public decimal? Amount{get {return Quantity * Rate;}}
		public decimal? Rate{get;set;}
		
	}
}
