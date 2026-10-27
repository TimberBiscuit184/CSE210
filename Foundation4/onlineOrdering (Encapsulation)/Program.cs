public class Program
{
    public static void Main(string[] args)
    {
        Address address1 = new Address("1020 W Main St", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("Colby Tran", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Guitar Strings", "GS-100", 7.50, 3));
        order1.AddProduct(new Product("Pedal Enclosure", "PE-220", 12.00, 2));
        order1.AddProduct(new Product("Pickup Cover", "PC-310", 4.25, 4));

        Address address2 = new Address("45 Queen St", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Alex Johnson", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Tuning Pegs", "TP-450", 18.00, 1));
        order2.AddProduct(new Product("Guitar Strap", "GR-505", 22.50, 2));

        List<Order> orders = new List<Order> { order1, order2 };

        foreach (Order order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
            Console.WriteLine("----------------------------------");
        }
    }
}
