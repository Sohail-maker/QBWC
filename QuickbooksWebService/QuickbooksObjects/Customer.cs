using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using QuickbooksWebService.DomainModel;

namespace QuickbooksWebService.QuickbooksObjects
{
	public class CustomerAdd
	{
		private Repository rep;
		private Customer shopper;
		private int _orderID;
		public CustomerAdd()
		{
			rep = new Repository();
		}
		public XDocument GetCustomer(int customerID,int orderID)
		{
			_orderID = orderID;
			shopper = rep.GetCustomer(customerID);
			//order = rep.GetShopperOrder(shopper.id);
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8",null),
				new XProcessingInstruction("qbxml","version=\"9.0\""),
				new XElement("QBXML",
					new XElement("QBXMLMsgsRq",new XAttribute("onError","stopOnError"),
						new XElement("CustomerAddRq",
							new XElement("CustomerAdd",
								new XElement("Name",Name),
								new XElement("IsActive",1),
								new XElement("FirstName",FirstName),
								new XElement("LastName",LastName),
								new XElement("BillAddress",
									new XElement("Addr1",BillAddress1),
									new XElement("Addr2",BillAddress2),
									new XElement("City",BillCity),
									new XElement("State",BillState),
									new XElement("PostalCode",BillPostalCode),
									new XElement("Country",BillCountry)),
								new XElement("ShipAddress",
									new XElement("Addr1",ShipAddress1),
									new XElement("Addr2",ShipAddress2),
									new XElement("City",ShipCity),
									new XElement("State",ShipState),
									new XElement("PostalCode",ShipPostalCode),
									new XElement("Country",ShipCountry))
								//new XElement("Phone",Phone),
								//new XElement("Email",Email)
							)
						)	
					)
				)
			);
			
			return doc;
		}
		private string Name{get{return String.Format("{0} {1} {2}",	firstname,lastname,_orderID);}}
		private string FullName { get { return String.Format("{0}:{1}:{2}", firstname, lastname,shopper.Orders.FirstOrDefault().OrderID); } }
		private string FirstName {get {return firstname;}}
		private string LastName {get {return lastname;}}
		private string BillAddress1 {get {return shopper.BillingAddress1 == String.Empty ? shopper.ShippingAddress1: shopper.BillingAddress1;}}
		private string BillAddress2 {get {return shopper.BillingAddress2 == String.Empty ? shopper.ShippingAddress2 : shopper.BillingAddress2;}}
		private string BillCity {get {return shopper.BillingCity == String.Empty ? shopper.ShippingCity : shopper.BillingCity;}}
		private string BillState {get {return shopper.BillingState == String.Empty ? shopper.ShippingState : shopper.BillingState;}}
		private string BillPostalCode {get {return shopper.BillingZip == String.Empty ? shopper.ShippingPostalCode : shopper.BillingZip;}}
		private string BillCountry {get {return shopper.BillingCountry == String.Empty ? shopper.ShippingCountry : shopper.BillingCountry;}}//</BillAddress>
		private string ShipAddress1 {get {return shopper.ShippingAddress1;}}//<ShipAddress> <!-- optional --> 
		private string ShipAddress2 {get {return shopper.ShippingAddress2;}}
		private string ShipCity {get {return shopper.ShippingCity;}}
		private string ShipState {get {return shopper.ShippingState;}}
		private string ShipPostalCode {get {return shopper.ShippingPostalCode;}}
		private string ShipCountry {get {return shopper.ShippingCountry;}}//</ShipAddress>
		//private string Phone {get {return shopper.b_phone;}}
		//private string Email {get {return shopper.email;}}
		private string firstname{get{return shopper.BillingFirstName == String.Empty ? shopper.ShippingFirstname : shopper.BillingFirstName;}}
		private string lastname{get{return shopper.BillingLastName == String.Empty ? shopper.ShippingLastName : shopper.BillingLastName;}}
		//private string ListID{get{return rep.GetSalesTaxCode(BillState,shopper.Orders.FirstOrDefault().ClientID) != null ? rep.GetSalesTaxCode(BillState,shopper.Orders.FirstOrDefault().ClientID).SalesTaxListID : null;}}
	}
}
 
 




 