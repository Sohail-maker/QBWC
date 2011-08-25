using System;
using System.Web;
using System.Web.Helpers;
using QuickbooksWebService.DomainModel;
using WebMatrix.WebData;

namespace QuickbooksWebService.ResponseProcess
{
	public static class Error
	{
		public static void ProcessFailedOrder(int requestID, string xmlMessage)
		{
			var rep = new Repository();
			var failure = new FailedOrder()
			{
				Imported = false,
				RequestID = requestID,
				OrderID = rep.GetQuickBooksOrderByRequestID(requestID).OrderID,
				DateAdded = DateTime.Now,
				ClientID = rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0
			};

			var order = rep.GetQuickBooksOrderByRequestID(requestID);
			order.ResponseXML = xmlMessage;
			rep.Add(failure);
			rep.Save();

			var hostUrl = HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
			hostUrl = "http://quickbooks.infomedia.com";
			var importUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Unprocessed-Invoices");
			var modifyUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Inventory");
			var body = new System.Text.StringBuilder();
			body.AppendLine("There was an error processing a website sale with Quickbooks");
			body.AppendLine("The most likely cause was that an item on the order was not found in your quickbooks inventory.");
			body.AppendLine("Please view the order that failed and create the invoice manually here: " + importUrl);
			body.AppendLine("You can modify your inventory naming schema here: " + modifyUrl);
			body.AppendLine(order.Receipt);
			body.AppendLine(xmlMessage);
			var user = rep.GetUser(WebSecurity.CurrentUserId);
			var email = user.Client.EmailRecipient != null ? user.Client.EmailRecipient : WebSecurity.CurrentUserName;
			WebMail.Send(email, "Quickbooks Web Connector Failed Invoice Attempt", body.ToString(), cc: "krisburtoft@infomedia.com", isBodyHtml: false);
		}

		public static void ProcessFailedOrder(Order order)
		{
			var rep = new Repository();
			var failure = new FailedOrder()
			{
				Imported = false,
				OrderID = order.OrderID,
				DateAdded = DateTime.Now,
				ClientID = rep.GetUser(WebSecurity.CurrentUserId).ClientID ?? 0
			};
			order.Reported = true;
			rep.Add(failure);
			rep.Save();

			var hostUrl = HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
			hostUrl = "http://quickbooks.infomedia.com";
			var importUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Unprocessed-Invoices");
			var modifyUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Inventory");
			var body = new System.Text.StringBuilder();
			body.AppendLine("Someone has made a purchase on your website which was not possible to import through the Web Connector and is ready for manual import.");
			body.AppendLine("Please view the order that failed and create the invoice manually here: " + importUrl);
			body.AppendLine(order.Receipt);
			var user = rep.GetUser(WebSecurity.CurrentUserId);
			var email = user.Client.EmailRecipient != null ? user.Client.EmailRecipient : WebSecurity.CurrentUserName;
			WebMail.Send(email, "Quickbooks Web Connector Failed Invoice Attempt", body.ToString(), cc: "kburtoft@infomedia.com", isBodyHtml: false);
		}

		public static void CatchApplicationException(Exception e)
		{
			var body = new System.Text.StringBuilder();
			body.AppendLine("Quickbooks application Exception");
			body.AppendLine("Message: " + e.Message);
			var theEnd = false;
			var innerException = e.InnerException;
			while(!theEnd)
			{
				if(innerException != null && innerException.InnerException != null)
				{
					body.AppendLine("\t Message: " +  innerException.Message);
					body.AppendLine("\t StackTrace: " + innerException.StackTrace);
					innerException = innerException.InnerException;
				}
				else{
					theEnd = true;
				}
			}
			body.AppendLine("StackTrace: " + e.StackTrace);
			WebMail.Send("kburtoft@infomedia.com", "Infomedia Quickbooks Application Error", body.ToString(), isBodyHtml: false);
		}
	}
}