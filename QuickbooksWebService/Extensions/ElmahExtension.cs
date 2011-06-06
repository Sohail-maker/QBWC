using System;
using System.Web.Services.Protocols;
using Elmah;
using System.Web;
using System.Text;

namespace QuickbooksWebService.Extensions
{
	public class ElmahExtension : SoapExtension
	{
		public override object GetInitializer(Type serviceType)
		{
			return null;
		}

		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			return null;
		}

		public override void Initialize(object initializer)
		{
		}

		public override void ProcessMessage(SoapMessage message)
		{
			if(message.Stage == SoapMessageStage.AfterSerialize && message.Exception != null)
			{
				Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(message.Exception,HttpContext.Current));
			}
		}
	}
}