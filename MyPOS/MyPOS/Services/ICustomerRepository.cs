using System;
using System.Collections.Generic;
using MyPOS.Models;

namespace MyPOS.Servcies
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();

        //Get Specific Customer data
        Customer GetCustomer(int Id);

        // Delete all Customers Data
        void DeleteAllCustomers();

        // Delete Specific Customer
        void DeleteCustomer(int Id);

        // Insert new Customer to DB 
        void InsertCustomer(Customer customer);

        // Update Customer Data
        void UpdateCustomer(Customer customer);
    }
}
