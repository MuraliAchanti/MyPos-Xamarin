using System;
using System.Collections.Generic;
using MyPOS.Helpers;
using MyPOS.Models;

namespace MyPOS.Servcies
{
    public class CustomerRepository : ICustomerRepository
    {
        DatabaseHelper _databaseHelper;
        public CustomerRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public void DeleteCustomer(int Id)
        {
            _databaseHelper.DeleteCustomer(Id);
        }

        public void DeleteAllCustomers()
        {
            _databaseHelper.DeleteAllCustomers();
        }

        public List<Customer> GetAllCustomers()
        {
            return _databaseHelper.GetAllCustomers();
        }

        public Customer GetCustomer(int Id)
        {
            return _databaseHelper.GetCustomer(Id);
        }

        public void InsertCustomer(Customer customer)
        {
            _databaseHelper.InsertCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _databaseHelper.UpdateCustomer(customer);
        }
    }
}
