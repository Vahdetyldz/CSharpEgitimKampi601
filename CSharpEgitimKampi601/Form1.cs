using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                Balance = decimal.Parse(txtBalance.Text),
                PurchaseAmount = int.Parse(txtPurchaseAmount.Text),
            };

            customerOperations.AddCustomer(customer);
            MessageBox.Show("Ekleme işlemi başarılı");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            customerOperations.DeleteCustomer(customerId);
            MessageBox.Show("Silme işlemi başarılı");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            var updateCustomer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                Balance = decimal.Parse(txtBalance.Text),
                PurchaseAmount = int.Parse(txtPurchaseAmount.Text),
                CustomerId = id
            };
            customerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("Güncelleme işlemi başarılı");
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            var customer = customerOperations.GetCustomerById(id);
            dataGridView1.DataSource = new List<Customer> { customer };
        }
    }
}
