namespace OrgansDelivery.Mobile.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();
		
		lblUserName.Text = "User Name";
		lblUserEmail.Text = "User Email";
		lblUserRole.Text = "User Role";
	}
}
