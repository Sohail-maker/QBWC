using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QuickbooksWebService.QuickbooksObjects
{
	public static class Shipping
	{
		public static XDocument AddWebShipping{
			get
			{
				 return	new XDocument(
					new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"8.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemOtherChargeAddRq",
							new XElement("ItemOtherChargeAdd",
							new XElement("Name","IWeb Shipping"),
							new XElement("IsActive",1),
							//new XElement("ParentRef"),
							new XElement("IsTaxIncluded",1),
							new XElement("SalesOrPurchase")
								)
							)
						)
					)
				);
			}
		}
	}
}