using System;
using System.Collections.Generic;
namespace QuickbooksWebService.DomainModel
{
	interface IRepository
	{
		void Add(Client client);
		void Add(FailedOrder failedOrder);
		void Add(InventoryLink link);
		void Add(QuickbooksInventory item);
		void Add(SalesTaxCode taxcode);
		void Add(ShopInventory item);
		void Add(Transaction transaction);
		void Add(IEnumerable<QuickbooksInventory> items);
		void Add(IEnumerable<ShopInventory> items);
		
		IEnumerable<shops_inventory> GetCeInventoryItems(int clientid);

		Client GetClient(int contentEditsClientID);
		IEnumerable<Client> GetAllClients();

		Customer GetCustomer(int CustomerID);
		IEnumerable<Customer> GetUnprocessedCustomers(int clientID);
		IEnumerable<Customer> GetUnprocessedCustomers(string transactionGuid);
		IEnumerable<Customer> GetUnprocessedCustomersByTransaction(string ticket);
		IEnumerable<Customer> GetCustomersByTransaction(int transactionID);
		IEnumerable<FailedOrder> GetFailedOrders(int? clientID);
		IEnumerable<OrderLineItem> GetOrderLineItems(int orderID);
		
		QuickbooksInventory GetQuickbooksInventoryItem(int clientId, string listID);
		QuickbooksInventory GetQuickbooksInventoryItem(string fullName, int clientId);
		IEnumerable<QuickbooksInventory> GetQuickbooksInventoryItems(int clientID);
		
		Order GetQuickBooksOrder(bool isCeOrder, int orderID);
		Order GetQuickBooksOrder(int ceOrderID);
		Order GetQuickBooksOrderByRequestID(int requestID);
		IEnumerable<Order> GetOrdersByTransaction(int transactionID);
		IEnumerable<Order> GetUnprocessedOrders(int clientID);
		IEnumerable<Order> GetUnprocessedOrders(string transactionGuid);
		IEnumerable<Order> GetUnprocessedOrdersByTransaction(string ticket);
		
		SalesTaxCode GetSalesTaxCode(int ClientID, int SalesTaxCodeID);
		SalesTaxCode GetSalesTaxCode(int ClientID, string name);
		IEnumerable<SalesTaxCode> GetSalesTaxCodes(int? clientID);
		
		ShopInventory GetShopInventory(int clientid,string inventoryName);
		IEnumerable<ShopInventory> GetShopInventoryItems(int clientId);
		
		IEnumerable<state> GetStates();
		
		Transaction GetTransaction(string ticket);
		webpages_Membership GetUser(int userID);
		webpages_Membership GetUser(string userName);
		void Save();
	}
}
