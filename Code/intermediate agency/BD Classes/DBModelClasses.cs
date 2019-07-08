using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace intermediate_agency.BD_Classes
{
    public class Person : INotifyPropertyChanged
    {
        private string name;
        private string phone;

        public int Id { get; set; }

        public string Name { 
            get{return name;}
            set 
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Phone {
            get { return phone; }
            set 
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        //TODO: Add validation to Phone

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class Employee : Person
    {
        private PostEnum post;
        private ObservableCollection<Order> orders;

        public PostEnum Post {
            get { return post; }
            set 
            {
                post = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Order> Orders {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        public Employee()
        {
            post = PostEnum.Manager;
            Orders = new ObservableCollection<Order>();
        }

        //TODO: Add methods to add and remove Orders

        //INotifyPropertyChanged is realised in base class
    }


    public class Customer : Person
    {
        private ClientLevelEnum level;
        private ObservableCollection<Order> orders;

        public ClientLevelEnum Level {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Order> Orders {
            get { return orders; }
            set 
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        public Customer()
        {
            Level = ClientLevelEnum.F;
            Orders = new ObservableCollection<Order>();
        }

        //TODO: Add methods to add and remove Orders

        //INotifyPropertyChanged is realised in base class
    }


    public class Seller : Person
    {
        private SellerReliabilityEnum reliability;
        private ObservableCollection<Offer> offers;

        public SellerReliabilityEnum Reliability {
            get { return reliability; }
            set
            {
                reliability = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Offer> Offers {
            get { return offers; }
            set
            {
                offers = value;
                OnPropertyChanged();
            }
        }

        public Seller()
        {
            Reliability = SellerReliabilityEnum.Unknown;
            Offers = new ObservableCollection<Offer>();
        }

        //TODO: Add methods to Add and Remove Offers

        //INotifyPropertyChanged is realised in base class
    }


    public class Order : INotifyPropertyChanged
    {
        private Employee manager;
        private Customer owner;
        private OrderStatusEnum status;
        private ObservableCollection<MerchandiseOrder> merchOrders;

        public int Id { get; set; }


        //1 to 0-1 no cascade delete
        public Employee Manager {
            get { return manager; }
            set 
            {
                manager = value;
                OnPropertyChanged();
            }
        }
        //1 to many with cascade delete
        public Customer Owner {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged();
            }
        }
        public OrderStatusEnum Status {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MerchandiseOrder> MerchOrders {
            get { return merchOrders; }
            set
            {
                merchOrders = value;
                OnPropertyChanged();
            }
        }

        public Order()
        {
            Status = OrderStatusEnum.NotAccepted;
            merchOrders = new ObservableCollection<MerchandiseOrder>();
        }

        //TODO: Add verifications that check that the manager/owner exist
        //TODO: Add methods to Add and Remove MerchandiseOrders;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class MerchandiseOrder : INotifyPropertyChanged
    {
        private int amount;
        private bool isCopmlited;

        //no need to display changes
        public int OrderId { get; set; }
        public Order Order { get; set;}

        //no need to display changes
        public int MerchTypeId { get; set; }
        public MerchandiseType MerchType { get; set;}


        public int Amount {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }
        public bool IsComplited {
            get { return isCopmlited; }
            set
            {
                isCopmlited = value;
                OnPropertyChanged();
            }
        }

        public MerchandiseOrder()
        {
            Amount = 1;
            IsComplited = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class Offer : INotifyPropertyChanged
    {
        private decimal price;

        public int Id { get; set; }

        //TODO: Add verification that checks that the owner exists 
        //no need to display changes
        public Seller Owner { get; set; }

        //no need to display changes
        public int TypeId { get; set; }
        public MerchandiseType Type { get; set; }


        public Decimal Price {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class MerchandiseType : INotifyPropertyChanged
    {

        private string name;

        public int Id { get; set; }
        public string Name {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        //no need to display changes (but it's not certain)
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<MerchandiseOrder> Orders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
