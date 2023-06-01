namespace OrgansDelivery.Mobile.Platforms.Android;

public static class HttpHandler
{
	public static HttpClientHandler GetInsecureHandler()
	{
		HttpClientHandler handler = new HttpClientHandler();
		handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
		return handler;
	}
}
