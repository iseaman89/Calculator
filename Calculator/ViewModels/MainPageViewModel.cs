using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using System.Data;
using NCalc;
using CommunityToolkit.Maui.Core;

namespace Calculator.ViewModels
{
	public partial class MainPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private string _topNumLabel = "0";
		[ObservableProperty]
		private string _resultLabel;

		[RelayCommand]
		public void NumAndOperationButtons(string key)
		{
			if (ResultLabel != string.Empty)
			{
				TopNumLabel = key;
				ResultLabel = string.Empty;
			}
            else if (TopNumLabel == "0") TopNumLabel = key;
			else if (TopNumLabel.Last() == ')') TopNumLabel += "*" + key;
			else TopNumLabel += key;
		}

		[RelayCommand]
		public void DeleteButton()
		{
			TopNumLabel = TopNumLabel.Remove(TopNumLabel.Length - 1);
			if (TopNumLabel == string.Empty) TopNumLabel = "0";
		}

		[RelayCommand]
		public void ClearTopLabel()
		{
			TopNumLabel = "0";
			ResultLabel = string.Empty;
		}

		[RelayCommand]
		public async void Calculate()
		{
			try
			{
                var e = new Expression(TopNumLabel);
                ResultLabel = "=" + e.Evaluate().ToString();
            }
			catch (Exception)
            {
#if ANDROID

                await Toast.Make("Invalid format used", ToastDuration.Short, 14).Show();

#else

                await Application.Current.MainPage.DisplayAlert("Warning", "Invalid format used", "Ok");

#endif
			}
		}
	}
}