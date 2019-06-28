using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace intermediate_agency.BD_Classes
{
    public abstract class AbstractPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        //TODO: Add validation to Phone
    }


    public class Employee : AbstractPerson
    {
        public PostEnum Post { get; set; }

        public ICollection<Order> Orders { get; set; }

        //TODO: Add methods to add and remove Orders
    }


    public class Customer : AbstractPerson
    {
        public ClientLevelEnum Level { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Level = ClientLevelEnum.F;
            Orders = new List<Order>();
        }

        //TODO: Add methods to add and remove Orders
    }


    public class Seller : AbstractPerson
    {
        public SellerReliabilityEnum Reliability { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public Seller()
        {
            Reliability = SellerReliabilityEnum.Unknown;
            Offers = new List<Offer>();
        }

        //TODO: Add methods to Add and Remove Offers
    }


    public class Order
    {
        public int ID { get; set; }

        //1 to 0-1 no cascade delete
        public Employee Manager { get; set; }

        //1 to many with cascade delete
        public Customer Owner { get; set; }

        public OrderStatusEnum Status { get; set; }

        public ICollection<MerchandiseOrder> MerchOrders { get; set; }

        public Order()
        {
            Status = OrderStatusEnum.NotAccepted;
            MerchOrders = new List<MerchandiseOrder>();
        }

        //TODO: Add verifications that check that the manager/owner exist
        //TODO: Add methods to Add and Remove MerchandiseOrders;
    }


    public class MerchandiseOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MerchTypeId { get; set; }
        public MerchandiseType MerchType { get; set; }

        public int Amount { get; set; }

        public bool IsComplited { get; set; }

        public MerchandiseOrder()
        {
            Amount = 1;
            IsComplited = false;
        }
    }


    public class Offer
    {
        public int ID { get; set; }

        //TODO: Add verification that checks that the owner exists 
        public Seller Owner { get; set; }

        public int TypeId { get; set; }
        public MerchandiseType Type { get; set; }

        public Decimal Price { get; set; }
    }


    public class MerchandiseType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Offer> Offers { get; set; }
        public ICollection<MerchandiseOrder> Orders { get; set; }
    }
}
