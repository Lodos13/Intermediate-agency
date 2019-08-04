using System;

using System.Windows;
using System.Windows.Controls;


namespace intermediate_agency
{
    /// <summary>
    /// Interaction logic for EditPersonWindow.xaml
    /// This window allows you to create and edit all types of heirs of person class.
    /// This window made this way in order to try dinamic binding/changing in WPF.
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public Person Person { get; private set; }

        //TODO: Add phone number validation to input textBox

        #region Constructors

        public EditPersonWindow()
        {
            InitializeComponent();

        }

        public EditPersonWindow(Person p): this()
        {
            Person = p;
            if (Person != null)
            {
                this.PersonNameTextBox.Text = Person.Name;
                this.PhoneTextBox.Text = Person.Phone;

                if (Person is Employee)
                {
                    this.TypeOfPersonComboBox.SelectedIndex = 0;
                    this.Param1ComboBox.SelectedIndex = Convert.ToInt32(((Employee)p).Post);
                }
                if (Person is Customer)
                {
                    this.TypeOfPersonComboBox.SelectedIndex = 1;
                    this.Param1ComboBox.SelectedIndex = Convert.ToInt32(((Customer)p).Level);
                }
                if (Person is Seller)
                {
                    this.TypeOfPersonComboBox.SelectedIndex = 2;
                    this.Param1ComboBox.SelectedIndex = Convert.ToInt32(((Seller)p).Reliability);
                }
                this.TypeOfPersonComboBox.IsEnabled = false;
            }
        }

        public EditPersonWindow(string type) : this()
        {
            if (type == "Employee")
            {
                this.TypeOfPersonComboBox.SelectedIndex = 0;
            }
            if (type == "Customer")
            {
                this.TypeOfPersonComboBox.SelectedIndex = 1;
            }
            if (type == "Seller")
            {
                this.TypeOfPersonComboBox.SelectedIndex = 2;
            }
            this.TypeOfPersonComboBox.IsEnabled = false;
        }

        #endregion

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public Person GetPerson()
        {
            if (this.Param1ComboBox.SelectedIndex == -1 || this.PersonNameTextBox.Text == "" || this.PhoneTextBox.Text == "")
                return null;

            if ( (string) ((ComboBoxItem) this.TypeOfPersonComboBox.SelectedItem).Content == "Employee")
            {
                if (Person == null)
                {
                    Employee e = new Employee();
                    e.Post = (PostEnum)Enum.Parse(typeof(PostEnum), (string)this.Param1ComboBox.SelectedValue);
                    Person = e;
                }
                else
                {
                    ((Employee)Person).Post = (PostEnum)Enum.Parse(typeof(PostEnum), (string)this.Param1ComboBox.SelectedValue);
                }
            }
            if ((string)((ComboBoxItem)this.TypeOfPersonComboBox.SelectedItem).Content == "Customer")
            {
                if (Person == null)
                {
                    Customer c = new Customer();
                    c.Level = (ClientLevelEnum)Enum.Parse(typeof(ClientLevelEnum), (string)this.Param1ComboBox.SelectedValue);
                    Person = c;
                }
                else
                {
                    ((Customer)Person).Level = (ClientLevelEnum)Enum.Parse(typeof(ClientLevelEnum), (string)this.Param1ComboBox.SelectedValue);
                }
            }
            if ((string)((ComboBoxItem)this.TypeOfPersonComboBox.SelectedItem).Content == "Seller")
            {
                if(Person == null)
                {
                    Seller s = new Seller();
                    s.Reliability = (SellerReliabilityEnum)Enum.Parse(typeof(SellerReliabilityEnum), (string)this.Param1ComboBox.SelectedValue);
                    Person = s;
                }
                else
                {
                    ((Seller)Person).Reliability = (SellerReliabilityEnum)Enum.Parse(typeof(SellerReliabilityEnum), (string)this.Param1ComboBox.SelectedValue);
                }
            }

            Person.Name = this.PersonNameTextBox.Text;
            Person.Phone = this.PhoneTextBox.Text;
            return Person;
        }

        public string GetPersonType()
        {
            return this.TypeOfPersonComboBox.Text;
        }
    }
}
