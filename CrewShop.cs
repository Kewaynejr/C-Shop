using CrewShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class CrewShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorBinding = new BindingSource();
        private decimal storeProfit = 0;

        public CrewShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemlistBox.DataSource = itemsBinding;

            itemlistBox.DisplayMember = "Display";
            itemlistBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            ShoppingCartListbox.DataSource = cartBinding;

            ShoppingCartListbox.DisplayMember = "Display";
            ShoppingCartListbox.ValueMember = "Display";

            vendorBinding.DataSource = store.Vendors;
            VendorListbox.DataSource = vendorBinding;

            VendorListbox.DisplayMember = "Display";
            VendorListbox.ValueMember = "Display";
        }

        private void SetupData()
        {

            store.Vendors.Add(new Vendor { FirstName = "Kevin", LastName = "Regular" });
            store.Vendors.Add(new Vendor { FirstName = "John", LastName = "Whaller" });
            store.Vendors.Add(new Vendor { FirstName = "Keenan", LastName = "Jones" });
            store.Vendors.Add(new Vendor { FirstName = "Janessa", LastName = "Reynolds" });

            store.Items.Add(new Item
            {
                Title = "Boxing Gloves",
                Description = "14 oz bag gloves",
                Price = 49.99M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Weight Bench",
                Description = "Smith Machine",
                Price = 299.99M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Athletic Shorts",
                Description = "14.99",
                Price = 49.99M,
                Owner = store.Vendors[2]
            });

            store.Items.Add(new Item
            {
                Title = "Grappling Bags",
                Description = "120 lbs Heavy Grappling bag",
                Price = 149.99M,
                Owner = store.Vendors[3]
            });

            store.Items.Add(new Item
            {
                Title = "Heavy Bags",
                Description = "90 lbs Heavy bag",
                Price = 99.99M,
                Owner = store.Vendors[3]
            });

            store.Items.Add(new Item
            {
                Title = "Life Vest",
                Description = "Life Vest ages 18 and up",
                Price = 29.99M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Baseball Glove",
                Description = "Size L",
                Price = 49.99M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Tennis Racket",
                Description = "All American Racket",
                Price = 79.99M,
                Owner = store.Vendors[2]
            });

            store.Items.Add(new Item
            {
                Title = "Women's Track Footwear",
                Description = "Natasha Hastings Edition",
                Price = 69.99M,
                Owner = store.Vendors[3]
            });


            store.Name = "Crew Fitness";
        }

        private void AddToCart_Click(object sender, EventArgs e)
        {
            //what was selceted
            Item selectedItem = (Item)itemlistBox.SelectedItem;

            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            //mark item in cart as sold
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            StoreProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorBinding.ResetBindings(false);
        }
    }
}
