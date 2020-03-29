using ShoppingCartApp.Models;
using ShoppingCartApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartDetailPage : ContentPage
    {
        public CartDetailPage()
        {
            InitializeComponent();
            BindingContext = new CartDetailViewModel();
        }

        public CartDetailPage(ObservableCollection<CartModel> products)
        {
            InitializeComponent();
            BindingContext = new CartDetailViewModel(products);
        }
    }
}