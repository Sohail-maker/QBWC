using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using WebMatrix.Data;

namespace QuickbooksWebService.DomainModel
{
	
	public class Repository : IRepository
	{
		#region Private Datacontext Classes
		private readonly ContentManagerDataContext CmDb;
		private readonly QuickBooksDataContext QbDb;
		#endregion

		#region Initializer
		public Repository()
		{
			CmDb = new ContentManagerDataContext(ConfigurationManager.ConnectionStrings["ContentManager"].ConnectionString);
			QbDb = new QuickBooksDataContext(ConfigurationManager.ConnectionStrings["QuickBooks"].ConnectionString);
		}

		#endregion

		#region Add
		public void Add(Client client)
		{
			QbDb.Clients.InsertOnSubmit(client);
		}

		public void Add(FailedOrder failedOrder)
		{
			QbDb.FailedOrders.InsertOnSubmit(failedOrder);
		}

		public void Add(InventoryLink link)
		{
			QbDb.InventoryLinks.InsertOnSubmit(link);
		}

		public void Add(QuickbooksInventory item)
		{
			QbDb.QuickbooksInventories.InsertOnSubmit(item);
		}

		public void Add(SalesTaxCode taxcode)
		{
			QbDb.SalesTaxCodes.InsertOnSubmit(taxcode);
		}

		public void Add(ShopInventory item)
		{
			QbDb.ShopInventories.InsertOnSubmit(item);
		}

		public void Add(Transaction transaction)
		{
			QbDb.Transactions.InsertOnSubmit(transaction);
		}

		public void Add(IEnumerable<QuickbooksInventory> items)
		{
			QbDb.QuickbooksInventories.InsertAllOnSubmit(items);
		}


		public void Add(IEnumerable<ShopInventory> items)
		{
			QbDb.ShopInventories.InsertAllOnSubmit(items);
		}

		#endregion

		#region Shop Inventory and CE Shop Inventory
		public IEnumerable<shops_inventory> GetCeInventoryItems(int clientid)
		{
			return CmDb.shops_inventories.Where(i => i.clientID == clientid && (i.status != false));
		}

		public ShopInventory GetShopInventory(int clientid,string inventoryName)
		{
			return GetShopInventoryItems(clientid).Where(s => s.Name == inventoryName).SingleOrDefault();
		}

		public IEnumerable<ShopInventory> GetShopInventoryItems(int clientId)
		{
			return QbDb.ShopInventories.Where(c => c.ClientID == clientId);
		}
		#endregion

		#region Client
		public Client GetClient(int contentEditsClientID)
		{
			return QbDb.Clients.Where(c => c.ContentEditsClientID == contentEditsClientID).SingleOrDefault();
		}

		public IEnumerable<Client> GetAllClients()
		{
			return QbDb.Clients;
		}
		#endregion
		
		#region Customer
		public IEnumerable<Customer> GetUnprocessedCustomers(int clientID)
		{
			var orders = QbDb.Orders.Where(c => c.ClientID == clientID);
			IEnumerable<Customer> customers = from order in orders	
														select order.Customer;
			return customers.Where(c => !c.Reported);
		}

		public IEnumerable<Customer> GetCustomersByTransaction(int transactionID)
		{
			return QbDb.Customers.Where(c => c.TransactionID == transactionID);
		}

		public Customer GetCustomer(int CustomerID)
		{
			return QbDb.Customers.Where(c => c.CustomerID == CustomerID).SingleOrDefault();
		}

		public IEnumerable<Customer> GetUnprocessedCustomersByTransaction(string ticket)
		{
			var transaction = QbDb.Transactions.Where(t => t.Guid == ticket).FirstOrDefault();
			return QbDb.Customers.Where(c => c.TransactionID == transaction.TransactionID && !c.Reported);
		}

		public IEnumerable<Customer> GetUnprocessedCustomers(string transactionGuid)
		{
			return QbDb.Customers.Where(c => c.Transaction.Guid == transactionGuid && !c.Reported);
		}
		#endregion

		#region Order
		public IEnumerable<Order> GetUnprocessedOrders(int clientID)
		{
			return QbDb.Orders.Where(o => !o.Reported && o.ClientID == clientID);
		}

		public IEnumerable<Order> GetOrdersByTransaction(int transactionID)
		{
			return QbDb.Orders.Where(c => c.TransactionID == transactionID);
		}
		
		public IEnumerable<Order> GetUnprocessedOrdersByTransaction(string ticket)
		{
			var transaction = QbDb.Transactions.Where(t => t.Guid == ticket).SingleOrDefault();
			return QbDb.Orders.Where(c => c.TransactionID == transaction.TransactionID && !c.Reported);
		}

		public IEnumerable<Order> GetUnprocessedOrders(string transactionGuid)
		{
			var trans = QbDb.Transactions.Where(t => t.Guid == transactionGuid).SingleOrDefault().TransactionID;
			return QbDb.Orders.Where(o => o.TransactionID == trans && !o.Reported);
		}

		public Order GetQuickBooksOrder(int ceOrderID)
		{
			return GetQuickBooksOrder(true,ceOrderID);
		}

		public Order GetQuickBooksOrder(bool isCeOrder,int orderID)
		{
			if(isCeOrder)
				return QbDb.Orders.Where(o => o.OrderNumber == orderID).SingleOrDefault();

			return QbDb.Orders.Where(o => o.OrderID == orderID).SingleOrDefault();
		}

		public Order GetQuickBooksOrderByRequestID(int requestID)
		{
			return QbDb.Orders.Where(o => o.RequestID == requestID).SingleOrDefault();
		}
		#endregion

		#region Transaction
		public Transaction GetTransaction(string ticket)
		{
			return QbDb.Transactions.Where(t => t.Guid == ticket).SingleOrDefault();
		}
		#endregion
		
		#region Sales Tax Codes
		public SalesTaxCode GetSalesTaxCode(int ClientID ,string name )
		{
			return QbDb.SalesTaxCodes.Where(t => t.QuickbooksFullName == name && t.ClientID == ClientID).SingleOrDefault();
		}

		public SalesTaxCode GetSalesTaxCode(int ClientID, int SalesTaxCodeID)
		{
			return QbDb.SalesTaxCodes.Where(s => s.ClientID == ClientID && s.SalesTaxCodeID == SalesTaxCodeID).SingleOrDefault();
		}

		public IEnumerable<SalesTaxCode> GetSalesTaxCodes(int? clientID)
		{
			return QbDb.SalesTaxCodes.Where(t => t.ClientID == (clientID ?? 0));
		}
		#endregion
		
		#region State
		public IEnumerable<state> GetStates()
		{
			return QbDb.states;
		}
		#endregion

		#region Failed Orders
		public IEnumerable<FailedOrder> GetFailedOrders(int? clientID)
		{
			return QbDb.FailedOrders.Where(f => f.ClientID == clientID);
		}
		#endregion

		#region OrderLineItems
		
		public IEnumerable<OrderLineItem> GetOrderLineItems(int orderID)
		{
			return QbDb.OrderLineItems.Where(o => o.OrderID == orderID);
		}

		#endregion

		#region Quickbooks Inventory
		
		public IEnumerable<QuickbooksInventory> GetQuickbooksInventoryItems(int clientID)
		{
			return QbDb.QuickbooksInventories.Where(q => q.ClientID == clientID);
		}

		public QuickbooksInventory GetQuickbooksInventoryItem(int clientId,string listID)
		{
			return QbDb.QuickbooksInventories.Where(i => (i.ClientID == clientId) && (i.ListID == listID)).SingleOrDefault();
		}

		public QuickbooksInventory GetQuickbooksInventoryItem(string fullName, int clientId)
		{
			return QbDb.QuickbooksInventories.Where(i => (i.ClientID == clientId) && (i.FullName == fullName)).SingleOrDefault();
		}
		
		#endregion

		#region User Methods
		public webpages_Membership GetUser(int userID)
		{
			return QbDb.webpages_Memberships.Where(u => u.UserID == userID).SingleOrDefault();
		}

		public webpages_Membership GetUser(string userName)
		{
			UserProfile user = QbDb.UserProfiles.Where(u => u.Email.ToLower() == userName.ToLower()).SingleOrDefault();
			return QbDb.webpages_Memberships.Where(u => u.UserID == user.UserID).SingleOrDefault();
		}

		#endregion
		
		#region Utilities
		public void Save()
		{
			QbDb.SubmitChanges();
			CmDb.SubmitChanges();
		}
		#endregion
		
	}
}
