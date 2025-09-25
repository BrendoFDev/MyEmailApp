using MyEmailApp.Entities;
using MyEmailApp.Services.Validators;
using System.Text.Json;

namespace MyEmailApp.Pages;

public partial class UserSettingPage : ContentPage
{
	private UserSettings userSettings;
	private readonly UserSettingValidator _validator;
	public UserSettingPage(UserSettingValidator validator)
	{
		_validator = validator;

		InitializeComponent();
		isConfigured();
	}

	private async Task isConfigured()
	{
        string? userSettingsJson = await SecureStorage.GetAsync("UserSetting");
		if (userSettingsJson is not null)
		{
			await Shell.Current.DisplayAlert("Alerta", "Suas configurações já foram feitas!", "Ok");

			userSettings = JsonSerializer.Deserialize<MyEmailApp.Entities.UserSettings>(userSettingsJson)!;
			txtEmail.Text = userSettings.Email;
			txtPassword.Text = userSettings.AppPassword;
		}
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		try
		{
            UserSettings userSetting = new UserSettings()
            {
                PersonName = txtName.Text,
                Email = txtEmail.Text,
                AppPassword = txtPassword.Text
            };

            await Validate(userSetting);

			string userSettingsJson = JsonSerializer.Serialize(userSetting);
			await SecureStorage.SetAsync("UserSetting",userSettingsJson);

			await DisplayAlert("Pronto meu parceiro!", "Configurações salvas com sucesso!", "Ok");
        }
		catch(Exception ex)
		{
			await DisplayAlert("Erro", ex.Message,"Ok");
		}
    }

	private async Task Validate(UserSettings userSettings)
	{
		var result = await _validator.ValidateAsync(userSettings);
		if(result is not null)
		{
			var errors = result.Errors.Select(x=>x.ErrorMessage).ToList();
			throw new Exception(string.Join(", \n", errors));
		}
	}
}