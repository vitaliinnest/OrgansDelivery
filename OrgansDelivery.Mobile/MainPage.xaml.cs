using OrgansDelivery.Mobile.Resources.Locales;
using System.Globalization;

namespace OrgansDelivery.Mobile;

public partial class MainPage : ContentPage
{
	int count = 0;

	public LocalizationResourceManager LocalizationResourceManager { get; } = LocalizationResourceManager.Instance;

	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName.ToLower() switch
		{
			"en" => new CultureInfo("uk-UA"),
			"uk" => new CultureInfo("en-US"),
			_ => throw new NotImplementedException()
		};

		LocalizationResourceManager.Instance.SetCulture(switchToCulture);

		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

