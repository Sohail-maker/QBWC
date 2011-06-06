using System;
using System.Linq;
using System.Xml.Linq;
using QuickbooksWebService.DomainModel;

namespace QuickbooksWebService.QuickbooksObjects
{
	public class Invoice
	{
		private Repository rep;
		private shops_shopper shopper;
		private shops_shoppersOrder order;
		private XDocument doc;
		private bool itemsExist = true;
		private string missingInventoryItem;
		private int requestID;
		private XNode lastLineItemNode { get { return doc.LastNode.Document.Descendants().Where(n => n.Name == "InvoiceLineAdd").LastOrDefault(); } }
		
		public Invoice()
		{
			rep = new Repository();
		}
		public XDocument GetInvoice(int shopperID,int orderID)
		{
		
			shopper = rep.GetShopper(shopperID);
			order = rep.GetShopperOrder(shopper.id,orderID);
			requestID = rep.GetQuickBooksOrder(order.id).RequestID;
			
			var lineItems = rep.GetInventoryItems(order.cartID);
			foreach(var item in lineItems)
			{
				var shopInventory = rep.GetShopInventoryItems(shopper.clientID).Where(i => i.Name == item.InventoryName);
				var exists = shopInventory.Any();
				if (!exists)
				{
					itemsExist = false;
					missingInventoryItem = item.InventoryName;
				}
				item.InventoryName = shopInventory.SingleOrDefault().QuickbooksInventoryID != null ? shopInventory.SingleOrDefault().QuickbooksInventory.FullName : item.InventoryName;
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
										new XElement("FullName",Name)),
									new XElement("TxnDate",String.Format("{0:yyyy-MM-dd}",DateTime.Now)),
									new XElement("BillAddress",
										new XElement("Addr1", FirstName + " " + LastName),
										new XElement("Addr2", BillAddress1),
										new XElement("Addr3", BillAddress2),
										new XElement("City", BillCity),
										new XElement("State", BillState),
										new XElement("PostalCode", BillPostalCode),
										new XElement("Country", BillCountry)),
									new XElement("ShipAddress",
										new XElement("Addr1", S_firstname + " " + S_lastname ),
										new XElement("Addr2", ShipAddress1),
										new XElement("Addr3", ShipAddress2),
										new XElement("City", ShipCity),
										new XElement("State", ShipState),
										new XElement("PostalCode", ShipPostalCode),
										new XElement("Country", ShipCountry)),
									new XElement("ItemSalesTaxRef")
								)
							)
						)
					)
				);
				var customerNode = doc.LastNode.Document.Descendants().Where(n => n.Name == "CustomerRef").SingleOrDefault();
				var salesTaxNode = doc.LastNode.Document.Descendants().Where(n => n.Name == "ItemSalesTaxRef").SingleOrDefault();
				if(order.orderTax != null && order.orderTax > 0)
				{
					salesTaxNode.Add(new XElement("FullName",TaxFullName));
				}
				foreach (var item in lineItems)
				{
					(lastLineItemNode == null ? salesTaxNode : lastLineItemNode).AddAfterSelf(
						new XElement("InvoiceLineAdd",
							new XElement("ItemRef",
								new XElement("FullName",item.InventoryName)),
							new XElement("Desc",item.Desc),
							new XElement("Quantity",item.Quantity == null ? 1 : item.Quantity),
							new XElement("Rate", ((decimal)item.Rate).ToString("N2")),
							new XElement("Amount", ((decimal)item.Amount).ToString("N2"))
						)
					);
				}
				if(ClassRef != null && ClassRef != String.Empty)
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
								new XElement("FullName",ShippingFullName)),
							new XElement("Desc","Shipping"),
							new XElement("Amount", Shipping)
							)
						);
			}
			else //Query Quickbooks to see if the inventory Item Exists
			{
				doc = Inventory.ItemQuery(requestID,missingInventoryItem);
			}
			return doc;
		}
		private string Name { get { return String.Format("{0} {1} {2}", firstname, lastname, order.id); } }
		private string FirstName { get { return firstname; } }
		private string LastName { get { return lastname; } }
		private string BillAddress1 { get { return shopper.b_address == String.Empty ? shopper.s_address : shopper.b_address; } }
		private string BillAddress2 { get { return shopper.b_address2 == String.Empty ? shopper.s_address2 : shopper.b_address2; } }
		private string BillCity { get { return shopper.b_city == String.Empty ? shopper.s_city : shopper.b_city; } }
		private string BillState { get { return shopper.b_state == String.Empty ? shopper.s_state : shopper.b_state; } }
		private string BillPostalCode { get { return shopper.b_zip == String.Empty ? shopper.s_zip : shopper.b_zip; } }
		private string BillCountry { get { return shopper.b_country == String.Empty ? shopper.s_country : shopper.b_country; } }//</BillAddress>
		private string ShipAddress1 { get { return shopper.s_address; } }//<ShipAddress> <!-- optional --> 
		private string ShipAddress2 { get { return shopper.s_address2; } }
		private string ShipCity { get { return shopper.s_city; } }
		private string ShipState { get { return shopper.s_state; } }
		private string ShipPostalCode { get { return shopper.s_zip; } }
		private string ShipCountry { get { return shopper.s_country; } }//</ShipAddress>
		private string Phone { get { return shopper.b_phone; } }
		private string Email { get { return shopper.email; } }
		private string firstname { get { return shopper.b_firstname == String.Empty ? shopper.s_firstname : shopper.b_firstname; } }
		private string lastname { get { return shopper.b_lastname == String.Empty ? shopper.s_lastname : shopper.b_lastname; } }
		private string S_firstname { get { return shopper.s_firstname; } }
		private string S_lastname { get { return shopper.s_lastname; } }
		private string Tax { get { return order.orderTax == null ? "0.00" : ((decimal)order.orderTax).ToString("N2"); } }
		private string Shipping {get {return order.orderShipping == null ? "0.00" :  ((decimal)order.orderShipping).ToString("N2");}}
		private string TaxFullName {get {return rep.GetClient(order.clientID).SalesTaxReference;}}
		private string ShippingFullName {get{return rep.GetClient(order.clientID).ShippingReference;}}
		private string ClassRef {get{return rep.GetClient(order.clientID).InvoiceClassRef;}}	
		private string TemplateRef {get{return rep.GetClient(order.clientID).InvoiceTemplateRef;}}
		
		
		}
	
}






