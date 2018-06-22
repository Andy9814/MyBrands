using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBrands.Models;

namespace MyBrands.Models
{
    public class OrderModel
    {
        // database context 
        private AppDbContext _db;
        public OrderModel(AppDbContext ctx)
        {
            _db = ctx;
        }
          
        
        // AddOrder method 
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
                      
                        order.OrderAmount = 0.0M;


                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item  = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if(item.QtyOnHand > 0)
                            {
                                order.OrderAmount += (item.MSRP * item.QtyOnHand);
                                
                                
                            }

                        }
                        _db.Orders.Add(order);
                        
                        _db.SaveChanges();

                        // for each loop through items
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.QtyOnHand > 0)
                            {
                                // get the orderLineItem.
                                OrderLineItem orderItem = new OrderLineItem();
                                orderItem.ProductId = item.Id;
                                Product prod = _db.Products.Find(orderItem.ProductId);
                                orderItem.QtyOrdered = item.QtyOnHand;
                                orderItem.SellingPrice = item.MSRP;


                                orderItem.OrderId = order.Id;

                                
                                if (orderItem.QtyOrdered > prod.QtyOnHand)
                                {
                                    
                                    orderItem.QtyBackOrdered += orderItem.QtyOrdered - prod.QtyOnHand;
                                    prod.QtyOnBackOrder += orderItem.QtyOrdered - prod.QtyOnHand;
                                    orderItem.QtySold = prod.QtyOnHand;
                                    prod.QtyOnHand = 0;
                                 
                                }
                                else
                                {
                                    prod.QtyOnHand = prod.QtyOnHand - orderItem.QtyOrdered;
                                    orderItem.QtySold = item.QtyOnHand;
                                    orderItem.QtyOrdered = item.QtyOnHand;
                                    orderItem.QtyBackOrdered = 0;
                                } //_db.Products.Update(prod);

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



        public List<Order> GetByUserId(string userId)
        {
            return _db.Orders.Where(order => order.UserId == userId).ToList<Order>();
        }

        // get the orderdetails for the order.js
        public List<orderViewModel> GetOrderDetails(int cid, string uid)
        {
            List<orderViewModel> allDetails = new List<orderViewModel>();
            // LINQ way of doing INNER JOINS
            var results = from c in _db.Set<Order>()
                          join ci in _db.Set<OrderLineItem>() on c.Id equals ci.OrderId
                          join p in _db.Set<Product>() on ci.ProductId equals p.Id
                          where (c.UserId == uid && c.Id == cid)
                          select new orderViewModel
                          {
                              OrderId = c.Id,
                              UserId = uid,
                              ProductId = ci.ProductId,
                              ProductName = ci.Product.ProductName,
                              QtyBackOrdered = ci.QtyBackOrdered,
                              QtyOrdered = ci.QtyOrdered,
                              QtySold = ci.QtySold,
                              MSRP = ci.SellingPrice,
                              SellingPrice = ci.SellingPrice * ci.QtyOrdered,
                              Subtotal = c.OrderAmount,
                              Tax = Math.Round((c.OrderAmount * 0.13M), 2),
                              OrderTotal = Math.Round((c.OrderAmount + (c.OrderAmount * 0.13M)), 2),
                              Description = p.Description,
                              DateCreated = c.OrderDate.ToString("yyyy/MM/dd - hh:mm tt")
                          };
            allDetails = results.ToList<orderViewModel>();
            return allDetails;
        }
    }

}










