using System.Collections.ObjectModel;
using System.Windows;


namespace intermediate_agency
{
    /// <summary>
    /// Interaction logic for SelectOrderWindow.xaml
    /// </summary>
    public partial class SelectOrderWindow : Window
    {
        public ObservableCollection<Order> Orders { get; set; }
        private Order selectedOrder;

        //TODO: Add logic to CheckBox ShowOnlyFreeOrders to filter list of Orders   

        public SelectOrderWindow(ObservableCollection<Order> ord)
        {
            InitializeComponent();
            this.Orders = ord;

            if(Orders != null && Orders.Count != 0)
            { selectedOrder = ord[0]; }

            DataContext = this;
        }

        public Order GetSelectedOrder()
        {
            return selectedOrder;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
