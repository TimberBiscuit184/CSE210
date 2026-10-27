public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    private const double USAShipping = 5.0;
    private const double InternationalShipping = 35.0;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        total += _customer.LivesInUSA() ? USAShipping : InternationalShipping;
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "PACKING LABEL";
        foreach (Product product in _products)
        {
            label += $"\n{product.GetName()} (ID: {product.GetProductId()})";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"SHIPPING LABEL\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}
