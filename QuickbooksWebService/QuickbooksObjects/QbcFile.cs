using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Configuration;
using QuickbooksWebService.DomainModel;
namespace QuickbooksWebService.QuickbooksObjects
{
	public static class QwcFile
	{
		/// <summary>
		/// Returns Quickbooks Web Connector File.
		/// Expects that the Client and user have already been set up prior to calling this method.
		/// Return this document when customer verifies their account.
		/// </summary>
		/// <param name="username">User Name - User must be set up prior to completion.</param>
		/// <param name="clientID">Content Edits Client ID - Must be set up in admin or Database prior to calling this method.</param>
		/// <returns>QBC file for installation in QWC on client machine.</returns>
		public static XDocument GenerateFile(string username,int clientID )
		{
			
			var rep = new Repository();
			var client = rep.GetClient(clientID);

			var ownerID = ConfigurationManager.AppSettings["OwnerID"];
			var fileID = Guid.NewGuid().ToString();
			var doc =  new XDocument(
				//new XDeclaration("1.0","utf-8","yes"),
				new XElement("QBWCXML",
					new XElement("AppName","Infomedia Quickbooks Web Integration"),
					new XElement("AppID"),
					new XElement("AppURL",ConfigurationManager.AppSettings["AppURL"]),
					new XElement("AppDescription","Imports your online sales and customers to your Quickbooks Enterprise or Quickbooks POS."),
					new XElement("AppSupport", ConfigurationManager.AppSettings["SupportUrl"]),
					new XElement("UserName", username),
					new XElement("OwnerID",ownerID),
					new XElement("FileID","{" + fileID + "}"),
					new XElement("QBType", client.QbType),
					new XElement("Style","Document"),
					new XElement("AuthFlags","0xF")
						)
					);
			
			client.QwcFile = doc.ToString();
			client.FileIDGuid = fileID;
			rep.Save();
			return doc;
		}
	}
}