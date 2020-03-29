using ShoppingCartApp.Models;
using ShoppingCartApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ShoppingCartApp.ViewModels
{
    public class CartDetailViewModel : BaseViewModel
    {
        Command _CheckoutCommand;
        public Command CheckoutCommand => _CheckoutCommand ?? (_CheckoutCommand = new Command(CheckoutAction));

        public CartDetailViewModel()
        {

        }
        public CartDetailViewModel(ObservableCollection<CartModel> products)
        {
            addSelected = products;
        }

        ObservableCollection<CartModel> _addSelected;
        public ObservableCollection<CartModel> addSelected
        {
            get => _addSelected;
            set => SetProperty(ref _addSelected, value);
        }

        private async void CheckoutAction()
        {
            string IDs = "";
            for (int i = 0; i < addSelected.Count; i++)
            {
                    IDs += " " + addSelected[i].idProduct.ToString();
            }

            ApiResponse response = await new ApiService().PostDataAsync("cart", new CheckoutModel
            {
                dateCheckout = DateTime.Now,
                schoolID = "69040",
                studentName = "Jorge Hernan Reyes Salazar",
                productsIDs = IDs
            });
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("ShoppingCartApp", "ERROR Checkout", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("ShoppingCartApp", response.Message, "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("ShoppingCartApp", "Checkout Success", "Ok");
        }
    }
}
