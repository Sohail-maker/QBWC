using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickbooksWebService.DomainModel;

/// <summary>
/// Summary description for Comparison
/// </summary>
public class InventoryComparison : IEqualityComparer<QuickbooksInventory>
{
	public bool Equals(QuickbooksInventory x, QuickbooksInventory y)
	{
		return x.FullName == y.FullName;
	}

	public int GetHashCode(QuickbooksInventory obj)
	{
		return obj.FullName.GetHashCode();
	}
}