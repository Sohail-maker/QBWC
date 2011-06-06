using System;
using System.Linq;
using System.Xml.Linq;
using QuickbooksWebService.DomainModel;

namespace QuickbooksWebService.QuickbooksObjects
{
	public class FullInvoice
	{
		private Repository rep;
		private Customer customer;
		private Order order;
		private XDocument doc;
		private bool itemsExist = true;
		private string missingInventoryItem;
		private int requestID;
		private XNode lastLineItemNode { get { return doc.LastNode.Document.Descendants().Where(n => n.Name == "InvoiceLineAdd").LastOrDefault(); } }

		public FullInvoice()
		{
			rep = new Repository();
		}
		public XDocument GetInvoice(int customerID, int orderID)
		{

			customer = rep.GetCustomer(customerID);
			order =		rep.GetQuickBooksOrder(false, orderID);
			requestID = order.RequestID;

			var lineItems = rep.GetOrderLineItems(orderID);
			foreach (var item in lineItems)
			{
				var shopItem = rep.GetShopInventoryItems(order.ClientID).Where(i => i.Name == item.InventoryName).FirstOrDefault();
				var exists = shopItem != null && shopItem.QuickbooksInventoryID != null;
				if (!exists)
				{
					var qbItem = rep.GetQuickbooksInventoryItem(item.InventoryName,order.ClientID);
					if (qbItem != null)
					{
						shopItem.QuickbooksInventoryID = qbItem.QuickbooksInventoryID;
						rep.Save();
					}
					else
					{
						itemsExist = false;
						missingInventoryItem = item.InventoryName;
					}
				}
				else
					item.InventoryName = shopItem.QuickbooksInventory.FullName;
			}
			if (itemsExist)
			{
				doc = new XDocument(
					new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"6.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
							new XElement("InvoiceAddRq",
								new XAttribute("requestID", requestID),
								new XElement("InvoiceAdd",
									new XElement("CustomerRef",
										new XElement("FullName", Name)),
									new XElement("TxnDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
									new XElement("BillAddress",
										new XElement("Addr1", FirstName + " " + LastName),
										new XElement("Addr2", BillAddress1),
										new XElement("Addr3", BillAddress2),
										new XElement("City", BillCity),
										new XElement("State", BillState),
										new XElement("PostalCode", BillPostalCode),
										new XElement("Country", BillCountry)),
									new XElement("ShipAddress",
										new XElement("Addr1", S_firstname + " " + S_lastname),
										new XElement("Addr2", ShipAddress1),
										new XElement("Addr3", ShipAddress2),
										new XElement("City", ShipCity),
										new XElement("State", ShipState),
										new XElement("PostalCode", ShipPostalCode),
										new XElement("Country", ShipCountry)),
									new XElement("ItemSalesTaxRef",
										new XElement("FullName", TaxFullName)
									)
								)
							)
						)
					)
				);
				var customerNode = doc.LastNode.Document.Descendants().Where(n => n.Name == "CustomerRef").SingleOrDefault();
				var salesTaxNode = doc.LastNode.Document.Descendants().Where(n => n.Name == "ItemSalesTaxRef").SingleOrDefault();
				if (order.OrderTaxPercentage != null )
				{
					salesTaxNode.Add(new XElement("FullName", TaxFullName));
				}
				foreach (var item in lineItems)
				{
					(lastLineItemNode == null ? salesTaxNode : lastLineItemNode).AddAfterSelf(
						new XElement("InvoiceLineAdd",
							new XElement("ItemRef",
								new XElement("FullName", item.InventoryName)),
							new XElement("Desc", item.FullDescription),
							new XElement("Quantity", item == null ? 1 : item.Quantity),
							new XElement("Rate", ((decimal)item.PriceEach).ToString("N2")),
							new XElement("Amount", ((decimal)(item.Quantity * item.PriceEach)).ToString("N2"))
						)
					);
				}
				if (ClassRef != null && ClassRef != String.Empty)
				{
					customerNode.AddAfterSelf(
						new XElement("ClassRef",
								new XElement("FullName", ClassRef)
						)
					);
				}
				if (TemplateRef != null && TemplateRef != String.Empty)
				{
					var element = doc.LastNode.Document.Descendants().Where(n => n.Name == "ClassRef").SingleOrDefault() == null ? customerNode : doc.LastNode.Document.Descendants().Where(n => n.Name == "ClassRef").SingleOrDefault();
					element.AddAfterSelf(
						new XElement("TemplateRef",
								new XElement("FullName", TemplateRef)
						)
					);
				}

				lastLineItemNode.AddAfterSelf(
						new XElement("InvoiceLineAdd",
							new XElement("ItemRef",
								new XElement("FullName", ShippingFullName)),
							new XElement("Desc", "Shipping"),
							new XElement("Amount", Shipping)
							)
						);
			}
			else //Query Quickbooks to see if the inventory Item Exists
			{
				doc = Inventory.ItemQuery(requestID, missingInventoryItem);
			}
			return doc;
		}
		private string Name { get { return String.Format("{0} {1} {2}", firstname, lastname, order.OrderNumber); } }
		private string FirstName { get { return firstname; } }
		private string LastName { get { return lastname; } }
		private string BillAddress1 { get { return customer.BillingAddress1 == String.Empty ? customer.ShippingAddress1 : customer.BillingAddress1; } }
		private string BillAddress2 { get { return customer.BillingAddress2 == String.Empty ? customer.ShippingAddress2 : customer.BillingAddress2; } }
		private string BillCity { get { return customer.BillingCity == String.Empty ? customer.ShippingCity : customer.BillingCity; } }
		private string BillState { get { return customer.BillingState == String.Empty ? customer.ShippingState : customer.BillingState; } }
		private string BillPostalCode { get { return customer.BillingZip == String.Empty ? customer.ShippingPostalCode : customer.BillingZip; } }
		private string BillCountry { get { return customer.BillingCountry == String.Empty ? customer.ShippingCountry : customer.BillingCountry; } }//</BillAddress>
		private string ShipAddress1 { get { return customer.ShippingAddress1; } }//<ShipAddress> <!-- optional --> 
		private string ShipAddress2 { get { return customer.ShippingAddress2; } }
		private string ShipCity { get { return customer.ShippingCity; } }
		private string ShipState { get { return customer.ShippingState; } }
		private string ShipPostalCode { get { return customer.ShippingPostalCode; } }
		private string ShipCountry { get { return customer.ShippingCountry; } }//</ShipAddress>
		//private string Phone { get { return customer.b_phone; } }
		//private string Email { get { return customer.email; } }
		private string firstname { get { return customer.BillingFirstName == String.Empty ? customer.ShippingFirstname : customer.BillingFirstName; } }
		private string lastname { get { return customer.BillingLastName == String.Empty ? customer.ShippingLastName : customer.BillingLastName; } }
		private string S_firstname { get { return customer.ShippingFirstname; } }
		private string S_lastname { get { return customer.ShippingLastName; } }
		private string Tax { get { return order.OrderTax == null ? "0.00" : ((decimal)order.OrderTax).ToString("N2"); } }
		private string Shipping { get { return order.OrderShipping == null ? "0.00" : ((decimal)order.OrderShipping).ToString("N2"); } }
		private string TaxFullName { get { return GetSalesTax(order.OrderTaxPercentage); } }
		private string ShippingFullName { get { return rep.GetClient(order.ClientID).ShippingReference; } }
		private string ClassRef { get { return rep.GetClient(order.ClientID).InvoiceClassRef; } }
		private string TemplateRef { get { return rep.GetClient(order.ClientID).InvoiceTemplateRef; } }
		
		private string GetSalesTax(decimal? rate)
		{
			if (rate != null)
			{
				var tax = rep.GetSalesTaxCodes(order.ClientID).Where(r => r.Rate == rate && r.Active != false).FirstOrDefault();
				return tax.QuickbooksFullName;
			}
			else
				return rep.GetClient(order.ClientID).SalesTaxReference;
		}

	}

}






