using OnlineShoppingStore.Data;
using OnlineShoppingStore.Models;

namespace OnlineShoppingStore.Services
{
    public class OrderService : IServiceBase<Order>
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(Order Model)
        {
            Model.CreatedDate = DateTime.Now;
            Model.DeliveryDate = DateTime.Parse(System.DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"));
            context.Orders.Add(Model);
            context.SaveChanges();
            return Model.Id;
        }

        public int Delete(int id)
        {
            context.Orders.Remove(context.Orders.FirstOrDefault(s => s.Id == id));
            context.SaveChanges();
            return 1;
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetDetails(int id)
        {
            return context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order Search(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, Order Model)
        {
            throw new NotImplementedException();
        }
    }
}
