using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class OrderModel
    {
        private AppDbContext _db;
        public OrderModel(AppDbContext ctx)
        {
            _db = ctx;
        }


        public int AddOrder(Dictionary<string, object> items, string user)
        {
            int orderId = -1;
            using (_db)
            {

                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order();
                        order.UserId = user;
                        order.OrderDate = System.DateTime.Now;
                        order.OrderAmount = 0;
                        


                        
                        foreach(var key in items.Keys)
                        {
                            ProductViewModel item  = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if(item.QtyOnHand > 0)
                            {
                                order.OrderAmount += (item.MSRP * item.QtyOnHand);
                                
                            }

                        }
                        _db.Orders.Add(order);
                        _db.SaveChanges();


                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.QtyOnHand > 0)
                            {
                                OrderLineItem orderItem = new OrderLineItem();
                                Product prod = new Product();
                                orderItem.QtyOrdered = item.QtyOnHand;
                                
                                orderItem.QtyBackOrdered = item.QtyOnBackOrder;
                                orderItem.ProductId = item.Id;
                                orderItem.OrderId = order.Id;
                                orderItem.SellingPrice = item.MSRP;


                                if(orderItem.QtyOrdered > prod.QtyOnHand)
                                {
                                    orderItem.QtyBackOrdered += orderItem.QtyOrdered - prod.QtyOnHand;
                                    prod.QtyOnBackOrder += orderItem.QtyOrdered - prod.QtyOnHand;
                                    orderItem.QtySold = prod.QtyOnHand;

                                }
                                else
                                {
                                    prod.QtyOnHand = prod.QtyOnHand - orderItem.QtyOrdered;
                                    orderItem.QtySold = item.QtyOnHand;
                                }
                                _db.Products.Update(prod);

                                _db.OrderLineItems.Add(orderItem);
                                _db.SaveChanges();

                            }

                        }


                        _trans.Commit();
                        orderId = order.Id;

                    }

                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }

            }

            return orderId ;

        }




    }
}
