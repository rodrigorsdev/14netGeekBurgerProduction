using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace GeekBurger.Production.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }

        public static implicit operator Order(GeekBurguer.Orders.Contract.NewOrderMessage model)
        {
            if (model == null)
                return null;

            return new Order
            {
                OrderId = model.OrderId,
                Status = "New"
            };
        }

        public static implicit operator Order(GeekBurguer.Orders.Contract.OrderChangedMessage model)
        {
            if (model == null)
                return null;

            return new Order
            {
                OrderId = model.OrderId,
                Status = GetEnumDescription(model.State)
            };
        }

        public static string GetEnumDescription(Enum en)
        {
            Type tipo = en.GetType();

            MemberInfo[] memInfo = tipo.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}