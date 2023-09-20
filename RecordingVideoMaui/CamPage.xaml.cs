namespace RecordingVideoMaui;

public partial class CamPage : ContentPage
{
	public CamPage(CamViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}