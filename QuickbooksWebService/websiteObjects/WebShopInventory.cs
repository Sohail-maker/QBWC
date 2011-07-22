using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickbooksWebService.DomainModel;

namespace QuickbooksWebService.WebsiteObjects
{
	public class WebShopInventory
	{

		public static void GetCeInventory(webpages_Membership user)
		{
			var rep = new Repository();
			var infomediainventory = rep.GetCeInventoryItems(user.Client.ContentEditsClientID);
			var items = from inv in infomediainventory
						select new ShopInventory()
						{
							ClientID = user.ClientID ?? 0,
							Name = inv.title
						};
			foreach (var item in items)
			{
				if (rep.GetShopInventory(user.ClientID ?? 0, item.Name) == null)
					rep.Add(item);
			}
			rep.Save();
		}

		public static void GetShopInventory()
		{
			throw new NotImplementedException();
		}
	}
}