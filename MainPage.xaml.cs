namespace carritoapi
{
    public partial class MainPage : ContentPage
    {
        private decimal productPrice = 99.99m;
        private decimal shippingCost = 10.00m;
        private int quantity = 1;

        public MainPage()
        {
            InitializeComponent();
            UpdateTotals(); 
        }


        private void OnQuantityChanged(object sender, ValueChangedEventArgs e)
        {
            quantity = (int)e.NewValue;
            UpdateTotals();


            SemanticScreenReader.Announce($"Quantity changed to {quantity}");
        }


        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Remove Item",
                "Are you sure you want to remove this item from your cart?",
                "Yes", "No");

            if (answer)
            {

                quantity = 0;
                UpdateTotals();

                await DisplayAlert("Cart Updated", "Item has been removed from your cart.", "OK");
                SemanticScreenReader.Announce("Item removed from cart");
            }
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            if (quantity == 0)
            {
                await DisplayAlert("Empty Cart",
                    "Your cart is empty. Please add items before checking out.",
                    "OK");
                return;
            }

            bool proceed = await DisplayAlert("Checkout",
                $"Proceed to checkout with total amount of ${CalculateTotal():F2}?",
                "Yes", "No");

            if (proceed)
            {
                await DisplayAlert("Success",
                    "Proceeding to checkout...",
                    "OK");
            }
        }


        private void UpdateTotals()
        {
            decimal subtotal = quantity * productPrice;
            decimal total = subtotal + (quantity > 0 ? shippingCost : 0);

            SubtotalLabel.Text = $"${subtotal:F2}";
            ShippingLabel.Text = quantity > 0 ? $"${shippingCost:F2}" : "$0.00";
            TotalLabel.Text = $"${total:F2}";
        }

        private decimal CalculateTotal()
        {
            return (quantity * productPrice) + (quantity > 0 ? shippingCost : 0);
        }
    }
}