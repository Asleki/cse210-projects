using System;

public class Program
{
    public static void Main(string[] args)
    {
        // --- Order 1 ---
        Console.WriteLine("--- Order 1 Details ---");

        // Create Address and Customer for Order 1
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Customer customer1 = new Customer("Alice Wagner", address1);

        // Create Products for Order 1
        Product product1_1 = new Product("Laptop", "LP001", 1200.00, 1);
        Product product1_2 = new Product("Mouse", "MS005", 25.00, 2);

        // Create Order 1 and add products
        Order order1 = new Order(customer1);
        order1.AddProduct(product1_1);
        order1.AddProduct(product1_2);

        // Display Order 1 details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.CalculateTotalPrice():0.00}\n");

        // --- Order 2 ---
        Console.WriteLine("--- Order 2 Details ---");

        // Create Address and Customer for Order 2
        Address address2 = new Address("Kenyatta Avenue, CBD", "Nairobi", "Nairobi County", "Kenya");
        Customer customer2 = new Customer("Bob Johnson", address2); 
        // Create Products for Order 2
        Product product2_1 = new Product("Keyboard", "KB010", 75.00, 1);
        Product product2_2 = new Product("Monitor", "MN003", 300.00, 1);
        Product product2_3 = new Product("Webcam", "WC001", 50.00, 1);

        // Create Order 2 and add products
        Order order2 = new Order(customer2);
        order2.AddProduct(product2_1);
        order2.AddProduct(product2_2);
        order2.AddProduct(product2_3);

        // Display Order 2 details
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.CalculateTotalPrice():0.00}\n");
    }
}