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
	public static class ProcessAddRs
	{
		private static Repository rep{get{return new Repository();}}
		public static void ProcessInvoiceAddRs(string response, string hResult, string message)
		{
			var doc = XDocument.Parse(response);
			if (response.IndexOf("statusSeverity=\"Error\"") > 0)
			{
				var itemRs = doc.Document.Root.Descendants("QBXMLMsgsRs").FirstOrDefault().Descendants().FirstOrDefault();
				var msg = itemRs.Attribute("statusMessage").Value;
				var rquestID = Convert.ToInt32(itemRs.Attribute("requestID").Value);
				Error.ProcessFailedOrder(rquestID, msg);
				//throw new ApplicationException(msg);
			}
			else{
				var invoiceAddRs = doc.Document.Root.Descendants("QBXMLMsgsRs").FirstOrDefault().Descendants("InvoiceAddRs").FirstOrDefault();
				var requestID = Convert.ToInt32(invoiceAddRs.Attribute("requestID").Value);
				var order = rep.GetQuickBooksOrderByRequestID(requestID);
				order.ResponseXML = response;
				order.Reported = true;
				order.Message = message;
			}
		}

		public static void ProcessSalesTaxAdd(string response)
		{
			var doc = XDocument.Parse(response);
			var invoiceAddRs = doc.Document.Root.Descendants("QBXMLMsgsRs").FirstOrDefault().Descendants("ItemSalesTaxAddRs").FirstOrDefault();
			var ListID = invoiceAddRs.Descendants("ItemSalesTaxRet").FirstOrDefault().Descendants("ListID").FirstOrDefault().Value;
			var db = Database.Open("Quickbooks");
			db.Execute("Update Clients set IWebSalesTaxID = @0 where contentEditsClientID = @1",ListID,rep.GetUser(WebSecurity.CurrentUserId).ClientID);
		}

		public static void ProcessShippingAdd(string response)
		{
			var doc = XDocument.Parse(response);
			var invoiceAddRs = doc.Document.Root.Descendants("QBXMLMsgsRs").FirstOrDefault().Descendants("ItemOtherChargeAddRs").FirstOrDefault();
			var ListID = invoiceAddRs.Descendants("ItemSalesTaxRet").FirstOrDefault().Descendants("ListID").FirstOrDefault().Value;
			var db = Database.Open("Quickbooks");
			db.Execute("Update Clients set IWebSalesTaxID = @0 where contentEditsClientID = @1", ListID, rep.GetUser(WebSecurity.CurrentUserId).ClientID);
		}

		public static void ProcessCustomerAdd(XDocument doc,string ticket)
		{
			try{
				var customer = rep.GetCustomersByTransaction(rep.GetTransaction(ticket).TransactionID).Where(t => t.CurrentRequest == true).FirstOrDefault();
			
				var success = doc.Descendants("CustomerAddRs").FirstOrDefault().Attribute("statusSeverity").Value != "Error";
				if (!success)
				{
					Error.ProcessFailedOrder(customer.Orders.LastOrDefault());
				}
			}
			catch(Exception e)
			{
				Error.CatchApplicationException(e);
			}
			finally
			{
				var customer = rep.GetCustomersByTransaction(rep.GetTransaction(ticket).TransactionID).Where(t => t.CurrentRequest == true).FirstOrDefault();
				if(customer != null)
				{
					customer.CurrentRequest = false;
					rep.Save();
				}
			}

			
		}

	}
}