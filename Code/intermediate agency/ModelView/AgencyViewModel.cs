using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using PropertyChanged;

namespace intermediate_agency
{
    /// <summary>
    /// ViewModel to bind DB and UI. Realize add/Remove Logic
    /// </summary>
    [ImplementPropertyChanged]
    class AgencyViewModel : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Class fields and properties

        AgencyDBContext db;

        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<MerchandiseType> MerchandiseTypes { get; set; }
        public ObservableCollection<MerchandiseOrder> MerchandiseOrders { get; set; }

        //public IEnumerable<Person> People { get; set; }
        //public IEnumerable<Employee> Employees { get; set; }
        //public IEnumerable<Customer> Customers { get; set; }
        //public IEnumerable<Seller> Sellers { get; set; }
        //public IEnumerable<Order> Orders { get; set; }
        //public IEnumerable<Offer> Offers { get; set; }
        //public IEnumerable<MerchandiseType> MerchandiseTypes { get; set; }
        //public IEnumerable<MerchandiseOrder> MerchandiseOrders { get; set; }

        #endregion

        #region Commands definitions

        private RelayCommand refillDBCommand;
        private RelayCommand clearDBCommand;
        private RelayCommand updateDataCommand;

        private RelayCommand addPerson;
        private RelayCommand editPerson;
        private RelayCommand deletePerson;

        private RelayCommand addMerchandise;
        private RelayCommand editMerchandise;
        private RelayCommand deleteMerchandise;

        private RelayCommand addOrderToEmployee;
        private RelayCommand removeOrderFromEmployee;

        //TODO: debug this command especially binding to root context in window
        private RelayCommand addOrderToCustomer;
        //TODO: this 2 command
        private RelayCommand editOrderToCustomer;
        private RelayCommand removeOrderFromCustomer;

        #endregion

        #region Constructor and Dispose

        public AgencyViewModel()
        {
            db = new AgencyDBContext();

            this.UpdateDataInView(null);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion

        #region Command's properties

        public RelayCommand RefillDBCommand
        {
            get
            {
                return refillDBCommand ?? (refillDBCommand = new RelayCommand(RefillDB));
            }
        }
        public RelayCommand ClearDBCommand
        {
            get
            {
                return clearDBCommand ?? (clearDBCommand = new RelayCommand(ClearDb));
            }
        }
        public RelayCommand UpdateDataCommand
        {
            get
            {
                return updateDataCommand ?? (updateDataCommand = new RelayCommand(UpdateDataInView));
            }
        }

        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ?? (addPerson = new RelayCommand(AddPersonInDB));
            }
        }

        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ?? (editPerson = new RelayCommand(EditPersonInDB));
            }
        }

        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ?? (deletePerson = new RelayCommand(DeletePersonInDB));
            }
        }

        public RelayCommand AddMerchandise
        {
            get
            {
                return addMerchandise ??
                    (addMerchandise = new RelayCommand((o) =>
                    {
                        EditMerchandiseWindow emw = new EditMerchandiseWindow();
                        if (emw.ShowDialog() == true)
                        {
                            MerchandiseType mtype = new MerchandiseType() { Name = emw.GetMerchaName() };
                            db.MerchandiseTypes.Add(mtype);
                            db.SaveChanges();
                            MerchandiseTypes = new ObservableCollection<MerchandiseType>(db.MerchandiseTypes.Local.ToList());
                        }
                    }));
            }
        }

        public RelayCommand EditMerchandise
        {
            get
            {
                return editMerchandise ??
                    (editMerchandise = new RelayCommand((SelectedItem) =>
                    {
                        MerchandiseType mtype = SelectedItem as MerchandiseType;
                        if (mtype == null)
                        {
                            MessageBox.Show("Merchandise is not selected");
                            return;
                        }
                        mtype = db.MerchandiseTypes.Find(mtype.Id);
                        if (mtype == null)
                        {
                            MessageBox.Show("Merchandise doesn't exist in Database!");
                            return;
                        }

                        EditMerchandiseWindow emw = new EditMerchandiseWindow(mtype.Name);
                        if (emw.ShowDialog() == true)
                        {
                            if (mtype.Name == emw.GetMerchaName())
                                return;
                            mtype.Name = emw.GetMerchaName();
                            db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand DeleteMerchandise
        {
            get
            {
                return deleteMerchandise ??
                    (deleteMerchandise = new RelayCommand((SelectedItem) =>
                    {
                        MerchandiseType mtype = SelectedItem as MerchandiseType;
                        if (mtype == null)
                        {
                            MessageBox.Show("Merchandise is not selected");
                            return;
                        }
                        mtype = db.MerchandiseTypes.Find(mtype.Id);
                        if (mtype == null)
                        {
                            MessageBox.Show("Merchandise doesn't exist in Database!");
                            return;
                        }

                        db.MerchandiseTypes.Remove(mtype);
                        db.SaveChanges();
                        MerchandiseTypes = new ObservableCollection<MerchandiseType>(db.MerchandiseTypes.Local.ToList());
                    }));
            }
        }

        public RelayCommand AddOrderToEmployee
        {
            get
            {
                return addOrderToEmployee ??
                    (addOrderToEmployee = new RelayCommand((SelectedItem) =>
                    {
                        Employee SelectedEmployee = SelectedItem as Employee;
                        if (SelectedEmployee == null)
                        { return; }

                        if (SelectedEmployee == null)
                        {
                            MessageBox.Show("Employee is not selected!");
                            return;
                        }

                        SelectOrderWindow sow = new SelectOrderWindow(this.Orders);

                        if (sow.ShowDialog() == true)
                        {
                            if (sow.GetSelectedOrder() == null)
                                return;

                            Order o = sow.GetSelectedOrder();
                            Employee e = db.Employees.Find(o.Manager.Id);
                            if (e != null)
                            { e.Orders.Remove(o); }
                            SelectedEmployee.Orders.Add(o);

                            db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand RemoveOrderFromEmployee
        {
            get
            {
                return removeOrderFromEmployee ??
                    (removeOrderFromEmployee = new RelayCommand((SelectedItem) =>
                    {
                        Order SelectedOrder = SelectedItem as Order;
                        if (SelectedOrder == null)
                            return;
                        Employee emp = db.Employees.Find(SelectedOrder.Manager.Id);
                        if (emp == null)
                            return;
                        emp.Orders.Remove(SelectedOrder);
                        db.SaveChanges();
                    }));
            }
        }

        public RelayCommand AddOrderToCustomer
        {
            get
            {
                return addOrderToCustomer ??
                    (addOrderToCustomer = new RelayCommand((SelectedItem) => 
                    {
                        tempFunct(SelectedItem);
                    }));
            }
        }

        private void tempFunct(Object SelectedItem)
        {
            Customer SelectedCustomer = SelectedItem as Customer;
            if (SelectedCustomer == null)
                return;

            EditOrderWindow eow = new EditOrderWindow(new Order(), new ObservableCollection<Customer>() { SelectedCustomer }, this.MerchandiseTypes);
            //EditOrderWindow eow = new EditOrderWindow(new Order(), this.Customers, this.MerchandiseTypes);
            if (eow.ShowDialog() == true)
            {
                Order resOrder = eow.GetOrder();
                if (resOrder == null)
                    return;
                SelectedCustomer.Orders.Add(resOrder);
            }
        }



        #endregion

        private void RefillDB(object o)
        {
            ClearDb(o);
            FillDb(o);

        }

        private void ClearDb(object o)
        {
            db.People.RemoveRange(db.People);
            db.Employees.RemoveRange(db.Employees);
            db.Customers.RemoveRange(db.Customers);
            db.Sellers.RemoveRange(db.Sellers);
            db.Orders.RemoveRange(db.Orders);
            db.Offers.RemoveRange(db.Offers);
            db.MerchandiseOrders.RemoveRange(db.MerchandiseOrders);
            db.MerchandiseTypes.RemoveRange(db.MerchandiseTypes);
            db.SaveChanges();

            UpdateDataInView(null);
        }

        private void FillDb(object o)
        {
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

            db.Employees.AddRange(Employees);
            db.Customers.AddRange(Customers);
            db.Sellers.AddRange(Sellers);
            db.MerchandiseTypes.AddRange(MerchandiseTypes);
            db.Orders.AddRange(Orders);
            db.Offers.AddRange(Offers);
            db.MerchandiseOrders.AddRange(MerchandiseOrders);
            db.SaveChanges();
        }

        private void UpdateDataInView(object o)
        {
            //People = db.People.Local.ToBindingList();
            //Employees = db.Employees.Local.ToBindingList();
            //Customers = db.Customers.Local.ToBindingList();
            //Sellers =db.Sellers.Local.ToBindingList();
            //Orders = db.Orders.Local.ToBindingList();
            //Offers = db.Offers.Local.ToBindingList();
            //MerchandiseTypes = db.MerchandiseTypes.Local.ToBindingList();
            //MerchandiseOrders = db.MerchandiseOrders.Local.ToBindingList();

            People = new ObservableCollection<Person>(db.People.ToList());
            Employees = new ObservableCollection<Employee>(db.Employees.ToList());
            Customers = new ObservableCollection<Customer>(db.Customers.ToList());
            Sellers = new ObservableCollection<Seller>(db.Sellers.ToList());
            Orders = new ObservableCollection<Order>(db.Orders.ToList());
            Offers = new ObservableCollection<Offer>(db.Offers.ToList());
            MerchandiseTypes = new ObservableCollection<MerchandiseType>(db.MerchandiseTypes.ToList());
            MerchandiseOrders = new ObservableCollection<MerchandiseOrder>(db.MerchandiseOrders.ToList());
        }

        private void AddPersonInDB(object o)
        {
            String PersonType = o as String;
            EditPersonWindow edw = new EditPersonWindow(PersonType);
            if(edw.ShowDialog() == true)
            {
                Person p = edw.GetPerson();
                if(p is null)
                {
                    MessageBox.Show("Can't add a person. Incorrect data");
                    return;
                }

                string type = edw.GetPersonType();
                
                switch(type)
                {
                    case "Employee":
                        db.Employees.Add((Employee)p);
                        db.SaveChanges();
                        People = new ObservableCollection<Person>(db.People.Local.ToList());
                        Employees = new ObservableCollection<Employee>(db.Employees.Local.ToList());
                        break;
                    case "Customer":
                        db.Customers.Add((Customer)p);
                        db.SaveChanges();
                        People = new ObservableCollection<Person>(db.People.ToList());
                        Customers = new ObservableCollection<Customer>(db.Customers.Local.ToList());
                        break;
                    case "Seller":
                        db.Sellers.Add((Seller)p);
                        db.SaveChanges();
                        People = new ObservableCollection<Person>(db.People.ToList());
                        Sellers = new ObservableCollection<Seller>(db.Sellers.Local.ToList());
                        break;
                    default:
                        break;
                }
                
#if DEBUG
                string s = String.Format("Debug message: Added person, Type of person - {0}, Name - {1}, Phone - {2}", type, p?.Name, p?.Phone);
                MessageBox.Show(s);
#endif
            }
        }

        private void EditPersonInDB(object obj)
        {
            Person p = null;
            if (obj == null)
            {
                MessageBox.Show("Person is not selected");
                return;
            }

            if (obj is Employee)
            {
                Employee emp = obj as Employee;
                p = new Employee()
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Phone = emp.Phone,
                    Post = emp.Post
                };
            }
            else if (obj is Customer)
            {
                Customer cust = obj as Customer;
                p = new Customer()
                {
                    Id = cust.Id,
                    Name = cust.Name,
                    Phone = cust.Phone,
                    Level = cust.Level
                };
            }
            else if (obj is Seller)
            {
                Seller sel = obj as Seller;
                p = new Seller()
                {
                    Id = sel.Id,
                    Name = sel.Name,
                    Phone = sel.Phone,
                    Reliability = sel.Reliability
                };
            }


            EditPersonWindow edw = new EditPersonWindow(p);
            if (edw.ShowDialog() == true)
            {
                p = edw.GetPerson();
                if (p is null)
                {
                    MessageBox.Show("Can't edit a person. Incorrect data");
                    return;
                }

                string type = edw.GetPersonType();

                switch (type)
                {
                    case "Employee":
                        Employee emp = db.Employees.Find(p.Id);
                        if(emp != null)
                        {
                            emp.Name = p.Name;
                            emp.Phone = p.Phone;
                            emp.Post = ((Employee)p).Post;
                            //db.Entry(emp).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        break;
                    case "Customer":
                        Customer cust = db.Customers.Find(p.Id);
                        if(cust != null)
                        {
                            cust.Name = p.Name;
                            cust.Phone = p.Phone;
                            cust.Level = ((Customer)p).Level;
                            //db.Entry(cust).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        break;
                    case "Seller":
                        Seller sel = db.Sellers.Find(p.Id);
                        if(sel != null)
                        {
                            sel.Name = p.Name;
                            sel.Phone = p.Phone;
                            sel.Reliability = ((Seller)p).Reliability;
                            //db.Entry(sel).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        break;
                    default:
                        break;
                }

#if DEBUG
                string s = String.Format("Debug message: Edited person, Type of person - {0}, Name - {1}, Phone - {2}", type, p?.Name, p?.Phone);
                MessageBox.Show(s);
#endif
            }
        }

        private void DeletePersonInDB(object obj)
        {
            if (obj == null)
            {
                MessageBox.Show("Person is not selected");
                return;
            }

            if (obj is Employee)
            {
                Employee emp = obj as Employee;
                db.Employees.Remove(emp);
                db.SaveChanges();
                People = new ObservableCollection<Person>(db.People.Local.ToList());
                Employees = new ObservableCollection<Employee>(db.Employees.Local.ToList());
            }
            else if (obj is Customer)
            {
                Customer cust = obj as Customer;
                db.Customers.Remove(cust);
                db.SaveChanges();
                People = new ObservableCollection<Person>(db.People.ToList());
                Customers = new ObservableCollection<Customer>(db.Customers.Local.ToList());
            }
            else if (obj is Seller)
            {
                Seller sel = obj as Seller;
                db.Sellers.Remove(sel);
                db.SaveChanges();
                People = new ObservableCollection<Person>(db.People.ToList());
                Sellers = new ObservableCollection<Seller>(db.Sellers.Local.ToList());
            }
        }



    }
}
