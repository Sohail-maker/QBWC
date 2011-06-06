using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using QuickbooksWebService.DomainModel;
using WebMatrix.Data;
using System.Web.WebPages;
using System.Web.Helpers;
using WebMatrix.WebData;

namespace QuickbooksWebService.ResponseProcess
{
	public static class ProcessQueryRs
	{
		public static void InventoryQuery(string response)
		{
			var rep = new Repository();
			var doc = XDocument.Parse(response);
			var itemInventoryQueryRs = doc.Document.Root.Descendants("QBXMLMsgsRs").FirstOrDefault().Descendants("ItemInventoryQueryRs").FirstOrDefault();
			var msg = itemInventoryQueryRs.Attribute("statusMessage").Value;
			var requestID = Convert.ToInt32(itemInventoryQueryRs.Attribute("requestID").Value);
			if (msg == "A query request did not find a matching object in QuickBooks")
			{
				Error.ProcessFailedOrder(requestID,response);
			}
			else
			{
				var order = rep.GetQuickBooksOrderByRequestID(requestID);
				var quickbooksItem = new QuickbooksInventory(){
					ClientID = order.ClientID,
					FullName = itemInventoryQueryRs.Element("ItemInventoryRet").Element("FullName").Value,
					ListID = itemInventoryQueryRs.Element("ItemInventoryRet").Element("ListID").Value,
				};
				rep.Add(quickbooksItem);
				rep.Save();
				var shopInventory = rep.GetShopInventoryItems(order.ClientID).Where(i => i.Name == itemInventoryQueryRs.Element("ItemInventoryRet").Element("FullName").Value).SingleOrDefault();
				if (shopInventory == null)
				{
					rep.Add(new ShopInventory(){
						 ClientID = order.ClientID,
						 Name = itemInventoryQueryRs.Element("ItemInventoryRet").Element("FullName").Value,
						 QuickbooksInventoryID = quickbooksItem.QuickbooksInventoryID
					});
				}
				else
				{
					shopInventory.QuickbooksInventoryID = quickbooksItem.QuickbooksInventoryID;
				}
				order.Reported = false;
				rep.Save();
			}
		}

		public static void ItemSalesTaxQueryRs(string response)
		{
			var rep = new Repository();
			var client = rep.GetClient(rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0);
			var db = Database.Open("Quickbooks");
			var taxCodes = XDocument.Parse(response).Document.Root.Descendants("QBXMLMsgsRs").Descendants("ItemSalesTaxQueryRs").FirstOrDefault().Descendants("ItemSalesTaxRet");
			var clientID = rep.GetUser(WebSecurity.CurrentUserId).ClientID;
			foreach (var item in taxCodes)
			{
				var taxCode = rep.GetSalesTaxCode(rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0, item.Descendants("Name").FirstOrDefault().Value);
				var taxCodeExists = taxCode != null;
				var name = item.Descendants("Name").FirstOrDefault().Value;
				var listID = item.Descendants("ListID").FirstOrDefault().Value;
				var taxRate = Convert.ToDecimal(item.Descendants("TaxRate").FirstOrDefault().Value);
				if (!taxCodeExists)
				{
					var taxcode = new SalesTaxCode()
					{
						ClientID = clientID ?? 0,
						QuickbooksFullName = name,
						Rate = taxRate
					};
					rep.Add(taxcode);
				}
				
				rep.Save();
			}
			
		}

		public static void ItemSalesTaxGroupQueryRs(string response)
		{
			var rep = new Repository();
			var client = rep.GetClient(rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0);
			var db = Database.Open("Quickbooks");
			var taxCodes = XDocument.Parse(response).Document.Root.Descendants("QBXMLMsgsRs").Descendants("ItemSalesTaxGroupQueryRs").FirstOrDefault().Descendants("ItemSalesTaxGroupRet");
			var clientID = rep.GetUser(WebSecurity.CurrentUserId).ClientID;
			foreach (var item in taxCodes)
			{
				var taxCode = rep.GetSalesTaxCode(rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0, item.Descendants("Name").FirstOrDefault().Value);
				var taxCodeExists = taxCode != null;
				var name = item.Descendants("Name").FirstOrDefault().Value;
				var listID = item.Descendants("ListID").FirstOrDefault().Value;
				decimal rate = 0;

				foreach(var rat in item.Descendants("ItemSalesTaxRef"))
				{
					rate += rep.GetSalesTaxCode(clientID ?? 0,rat.Descendants("FullName").FirstOrDefault().Value).Rate;
				}
				var taxRate = rate;
				if (!taxCodeExists)
				{
					var taxcode = new SalesTaxCode()
					{
						ClientID = clientID ?? 0,
						QuickbooksFullName = name,
						Rate = taxRate
					};
					rep.Add(taxcode);
				}

				rep.Save();
			}
			
		}

		public static void ItemInventoryQueryRs(string response)
		{
			ParseItemQuery("ItemInventoryQueryRs","ItemInventoryRet",response);
		}

		public static void ItemNonInventoryQueryRs(string response)
		{
			ParseItemQuery("ItemNonInventoryQueryRs", "ItemNonInventoryRet", response);
		}

		private static void ParseItemQuery(string retType,string itemNode,string response)
		{
			var rep = new Repository();
			var clientID = rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0;
			var items = XDocument.Parse(response).Document.Root.Descendants("QBXMLMsgsRs").Descendants(retType).FirstOrDefault().Descendants(itemNode);
			List<QuickbooksInventory> inventoryList = new List<QuickbooksInventory>();
			foreach(var item in items)
			{
				var DatabaseItem = rep.GetQuickbooksInventoryItem(clientID,item.Descendants("ListID").FirstOrDefault().Value);
				if (DatabaseItem != null)
				{
					DatabaseItem.FullName = item.Descendants("FullName").FirstOrDefault().Value;
				}
				else
				{
					inventoryList.Add(new QuickbooksInventory(){
						ClientID = clientID,
						ListID = item.Descendants("ListID").FirstOrDefault().Value,
						FullName = item.Descendants("FullName").FirstOrDefault().Value
					});
				}

			}
			rep.Add(inventoryList);
			rep.Save();
		}
	}
}