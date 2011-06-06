using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QuickbooksWebService.QuickbooksObjects
{
	public static class Inventory
	{
		public static XDocument ItemNonInventoryFullQueryRq()
		{
			return new XDocument(
				new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"6.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemNonInventoryQueryRq")
					)
				)
			);
		}

		public static XDocument ItemInventoryFullQueryRq()
		{
			return new XDocument(
				new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"6.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemInventoryQueryRq")
					)
				)
			);
		}

		public static XDocument ItemQuery(int requestID,string missingInventoryItem)
		{
			return new XDocument(
						new XDeclaration("1.0", "utf-8", null),
							new XProcessingInstruction("qbxml", "version=\"6.0\""),
							new XElement("QBXML",
								new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
								new XElement("ItemInventoryQueryRq",
									new XAttribute("requestID", requestID),
									new XElement("NameFilter",
										new XElement("MatchCriterion", "Contains"),
										new XElement("Name", missingInventoryItem)
									)
								)
							)
						)
					);
		}

	}
}