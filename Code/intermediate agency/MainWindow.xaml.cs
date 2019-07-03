using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using intermediate_agency.BD_Classes;
using System.Collections.ObjectModel;

namespace intermediate_agency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> Employees {get; set;}
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<MerchandiseType> MerchandiseTypes { get; set; }
        public ObservableCollection<MerchandiseOrder> MerchandiseOrders { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Employees = new ObservableCollection<Employee>(){
                new Employee{Id = 1, Name = "Serious Sam", Phone = "+7-900-333-33-33", Post = PostEnum.Manager},
                new Employee{Id = 2, Name = "Gordan Freeman", Phone = "+7-999-999-99-99", Post = PostEnum.Manager},
                new Employee{Id = 2, Name = "Boss The King", Phone = "+7-000-00-00-01", Post = PostEnum.Chief}
            };

            Customers = new ObservableCollection<Customer>(){
                new Customer{Id = 1, Name = "Vol De Morty", Phone = "+7-332-258-44-78", Level = ClientLevelEnum.A},
                new Customer{Id = 2, Name = "The Little Prince", Phone = "+7-332-258-45-66", Level = ClientLevelEnum.C},
                new Customer{Id = 3, Name = "Port bugger's chief", Phone = "+7-666-666-66-66", Level = ClientLevelEnum.F}
            };

            Sellers = new ObservableCollection<Seller>(){
                new Seller{Id = 1, Name = "Alice The Fox", Phone = "+7-243-334-56-44", Reliability = SellerReliabilityEnum.Reliable},
                new Seller{Id = 2, Name = "Basilio The Cat", Phone = "+7-243-334-56-45", Reliability = SellerReliabilityEnum.Unknown},
                new Seller{Id = 1, Name = "Port bugger's chief", Phone = "+7-666-666-66-66", Reliability = SellerReliabilityEnum.Unreliable}
            };

            MerchandiseTypes = new ObservableCollection<MerchandiseType>() {
                new MerchandiseType{Id = 1, Name = "Golden ring"},
                new MerchandiseType{Id = 2, Name = "Food ration"},
                new MerchandiseType{Id = 3, Name = "Magic wand"},
                new MerchandiseType{Id = 4, Name = "Phoenix feather"},
                new MerchandiseType{Id = 5, Name = "Money Tree seed"},
                new MerchandiseType{Id = 6, Name = "Alcohol"},
                new MerchandiseType{Id = 7, Name = "Star dust"},
                new MerchandiseType{Id = 8, Name = "Unicorn's tear"}
            };

            Orders = new ObservableCollection<Order>(){
                new Order{ Id = 1, Owner = Customers[0], Status = OrderStatusEnum.NotAccepted, Manager = null},
                new Order{ Id = 2, Owner = Customers[2], Status = OrderStatusEnum.Fulfilled, Manager = Employees[0]},
                new Order{ Id = 3, Owner = Customers[2], Status = OrderStatusEnum.InProgress, Manager = Employees[1]}
            };

            MerchandiseOrders = new ObservableCollection<MerchandiseOrder>(){
                new MerchandiseOrder{Order = Orders[0], MerchType = MerchandiseTypes[2], Amount = 1},
                new MerchandiseOrder{Order = Orders[0], MerchType = MerchandiseTypes[3], Amount = 5},
                new MerchandiseOrder{Order = Orders[0], MerchType = MerchandiseTypes[7], Amount = 3},
                new MerchandiseOrder{Order = Orders[1], MerchType = MerchandiseTypes[5], Amount = 10},
                new MerchandiseOrder{Order = Orders[2], MerchType = MerchandiseTypes[1], Amount = 10},
                new MerchandiseOrder{Order = Orders[2], MerchType = MerchandiseTypes[5], Amount = 25},
            };
            
            
            Orders[0].MerchOrders = new ObservableCollection<MerchandiseOrder>() {
                MerchandiseOrders[0], MerchandiseOrders[1], MerchandiseOrders[2]
            };
            Orders[1].MerchOrders = new ObservableCollection<MerchandiseOrder>() {
                MerchandiseOrders[3]
            };
            Orders[2].MerchOrders = new ObservableCollection<MerchandiseOrder>() {
                MerchandiseOrders[4], MerchandiseOrders[5]
            };

            MerchandiseTypes[1].Orders = new ObservableCollection<MerchandiseOrder>(){
                MerchandiseOrders[4]
            };
            MerchandiseTypes[2].Orders = new ObservableCollection<MerchandiseOrder>(){
                MerchandiseOrders[0]
            };
            MerchandiseTypes[3].Orders = new ObservableCollection<MerchandiseOrder>(){
                MerchandiseOrders[1]
            };
            MerchandiseTypes[5].Orders = new ObservableCollection<MerchandiseOrder>(){
                MerchandiseOrders[3], MerchandiseOrders[5]
            };
            MerchandiseTypes[7].Orders = new ObservableCollection<MerchandiseOrder>(){
                MerchandiseOrders[2]
            };

            Offers = new ObservableCollection<Offer>(){
                new Offer{Id = 1, Owner = Sellers[0], Price = 10, Type = MerchandiseTypes[1]},
                new Offer{Id = 2, Owner = Sellers[0], Price = 18, Type = MerchandiseTypes[5]},
                new Offer{Id = 4, Owner = Sellers[1], Price = 12, Type = MerchandiseTypes[1]},
                new Offer{Id = 3, Owner = Sellers[1], Price = 120, Type = MerchandiseTypes[6]},
                new Offer{Id = 4, Owner = Sellers[1], Price = 800, Type = MerchandiseTypes[7]},
                new Offer{Id = 5, Owner = Sellers[2], Price = 1200, Type = MerchandiseTypes[0]},
            };

            MerchandiseTypes[0].Offers = new ObservableCollection<Offer>(){
                Offers[5]
            };
            MerchandiseTypes[1].Offers = new ObservableCollection<Offer>(){
                Offers[0], Offers[2]
            };
            MerchandiseTypes[5].Offers = new ObservableCollection<Offer>(){
                Offers[1]
            };
            MerchandiseTypes[6].Offers = new ObservableCollection<Offer>(){
                Offers[3]
            };
            MerchandiseTypes[7].Offers = new ObservableCollection<Offer>(){
                Offers[4]
            };

            this.EmployeeList.ItemsSource = Employees;
            this.CustomersList.ItemsSource = Customers;
            this.SellersList.ItemsSource = Sellers;
            this.OrdersList.ItemsSource = Orders;
            this.OffersList.ItemsSource = Offers;
            this.MerchandisesList.ItemsSource = MerchandiseTypes;
        }
    }
}
