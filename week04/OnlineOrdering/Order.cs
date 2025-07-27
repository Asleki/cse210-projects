// Order.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    private List<Product> _products;
    private Customer _customer;
    private const double US_SHIPPING_COST = 5.00;
    private const double INTERNATIONAL_SHIPPING_COST = 35.00;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalPrice()
    {
        double totalProductCost = _products.Sum(p => p.GetTotalCost());
        double shippingCost = _customer.LivesInUSA() ? US_SHIPPING_COST : INTERNATIONAL_SHIPPING_COST;
        return totalProductCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in _products)
        {
            label += $"Product: {product.GetName()}, ID: {product.GetProductId()}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}