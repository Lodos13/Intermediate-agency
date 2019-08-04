using System;

using System.Collections.ObjectModel;
using System.Windows;


namespace intermediate_agency
{
    /// <summary>
    /// Interaction logic for EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        //TODO: Change table column with MerchTypes from text to combobox

        public Order Order { get; set; }
        public ObservableCollection<Customer> CustomerList { get; set; }
        public ObservableCollection<MerchandiseType> MerchList { get; set; }

        public EditOrderWindow(Order ord, ObservableCollection<Customer> custList, ObservableCollection<MerchandiseType> mList)
        {
            InitializeComponent();

            if (custList == null || mList == null)
                throw new System.Exception("List of Customers and List of merchandises must be defined");
            CustomerList = custList;
            MerchList = mList;

            if (ord == null)
                this.Order = new Order();
            else
                this.Order = ord;

            DataContext = this;
        }

        public Order GetOrder()
        {
            if (this.CustomerComboBox.SelectedIndex == -1 || this.OrderStatusComboBox.SelectedIndex == -1)
                return null;

            Order.Owner = this.CustomerComboBox.SelectedItem as Customer;
            Order.Status = (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), (string)this.OrderStatusComboBox.SelectedValue);
            Order.Manager = Order.Manager;

            return Order;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
