using MyEmailApp.Entities;
using MyEmailApp.Services.Validators;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyEmailApp.Pages;

public partial class EmailFactoryPage : ContentPage
{
	private readonly EmailModelValidator _emailModelValidator;
	public EmailFactoryPage(EmailModelValidator emailModelValidator)
	{
		InitializeComponent();
	}

    private async void btnSave_Clicked(object sender, EventArgs e)
	{
		try
		{
            var emailModel = new EmailModel
            {
                ModelName = txtModelName.Text,
                Message = txtMessage.Text,
                Subject = txtTopic.Text
            };

			await Validate(emailModel);

			string? emailModelJson = await SecureStorage.GetAsync("EmailModels");
			List<EmailModel> models = JsonSerializer.Deserialize<List<EmailModel>>(emailModelJson!)!;
        }
		catch(Exception ex)
		{
            await DisplayAlert("Informação", ex.Message, "Ok");
        }
		finally
		{
			txtModelName.Text = string.Empty;
			txtMessage.Text = string.Empty;
			txtTopic.Text = string.Empty;
		}
	}

	private async Task Validate(EmailModel model)
	{
		var result = await _emailModelValidator.ValidateAsync(model);

        if (result is not null)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new Exception(string.Join(", \n", errors));
        }
    }
}