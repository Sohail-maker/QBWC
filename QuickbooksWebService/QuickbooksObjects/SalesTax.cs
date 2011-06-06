using System.Xml.Linq;

namespace QuickbooksWebService.QuickbooksObjects
{
	public static class SalesTax
	{
		public static XDocument ItemSalesTaxQueryRq
		{
			get 
			{
				return	new XDocument(
					new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"6.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemSalesTaxQueryRq")
						)
					)
				);
			}
		}

		public static XDocument ItemSalesTaxGroupQueryRq
		{
			get
			{
				return new XDocument(
					new XDeclaration("1.0", "utf-8", null),
					new XProcessingInstruction("qbxml", "version=\"6.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemSalesTaxGroupQueryRq")
						)
					)
				);
			}
		}

		public static XDocument AddWebSalesTax
		{
			get 
			{
				return new XDocument(
					new XDeclaration("1.0","utf-9",null),
					new XProcessingInstruction("qbxml","version=\"9.0\""),
					new XElement("QBXML",
						new XElement("QBXMLMsgsRq", new XAttribute("onError", "stopOnError"),
						new XElement("ItemSalesTaxAddRq",
							new XElement("ItemSalesTaxAdd",
								new XElement("Name","iWeb-Sales Tax")
								)
							)
						)
					)
				);
			}
		}
	}
}