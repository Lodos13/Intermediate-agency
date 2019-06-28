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

            this.EmployeeList.ItemsSource = Employees;
            this.CustomersList.ItemsSource = Customers;
            this.SellersList.ItemsSource = Sellers;
        }
    }
}
