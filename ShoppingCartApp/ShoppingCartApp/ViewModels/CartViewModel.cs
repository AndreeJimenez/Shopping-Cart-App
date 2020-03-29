using ShoppingCartApp.Models;
using ShoppingCartApp.Services;
using ShoppingCartApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ShoppingCartApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private int fill = 0;
        static CartViewModel _instance;

        Command _ShowCommand;
        public Command ShowCommand => _ShowCommand ?? (_ShowCommand = new Command(ShowCartAction));

        Command<CartModel> _AddProductCommand;
        public Command<CartModel> AddProductCommand => _AddProductCommand ?? (_AddProductCommand = new Command<CartModel>(AddAction));

        

        ObservableCollection<CartModel> _ProductsSelected;
        public ObservableCollection<CartModel> ProductsSelected
        {
            get => _ProductsSelected;
            set => SetProperty(ref _ProductsSelected, value);
        }
        ObservableCollection<CartModel> _Products;
        public ObservableCollection<CartModel> Products
        {
            get => _Products;
            set => SetProperty(ref _Products, value);
        }

        public CartViewModel()
        {
            _instance = this;
            LoadProducts();
        }

        private async void LoadProducts()
        {
            ApiResponse response = await new ApiService().GetDataAsync<CartModel>("product");
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error loading the products", response.Message, "Ok");
                return;
            }
            Products = (ObservableCollection<CartModel>)response.Result;
        }

        public static CartViewModel GetInstance()
        {
            if (_instance == null) _instance = new CartViewModel();
            return _instance;
        }

        private void ShowCartAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CartDetailPage(ProductsSelected));
        } 

        public async void AddAction(CartModel newProduct)
        {
            if (fill == 0)
            {
                this.ProductsSelected = new ObservableCollection<CartModel> { newProduct };
                await Application.Current.MainPage.DisplayAlert("ShoppingCartApp", "Product successfully added", "Ok");
                fill = 1;
            }
            else {
                ProductsSelected.Add(newProduct);
                await Application.Current.MainPage.DisplayAlert("ShoppingCartApp", "Product successfully added", "Ok");
            }
        }

        public void RefreshProducts()
        {
            LoadProducts();
        }
    }
}