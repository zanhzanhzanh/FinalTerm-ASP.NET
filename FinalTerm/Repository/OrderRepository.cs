﻿using AutoMapper;
using FinalTerm.Common.HandlingException;
using FinalTerm.Dto;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;

namespace FinalTerm.Repository {
    public class OrderRepository : BaseRepository<Order>, IOrderRepository {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, ICustomerRepository customerRepository, IProductRepository productRepository, IMapper mapper) : base(context) {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<Order>> GetOrdersByPhone(string phone) {
            List<Order> foundEntity = await _context.Orders
                 .Include(i => i.OrderItems)
                 .ThenInclude(i => i.Product)
                 .Where(i => i.Customer.Phone == phone)
                 .ToListAsync();

            return foundEntity;
        }

        public async Task<Order> AddTransaction(CreateOrderDto rawOrder) {
            Customer takeRawCustomer = new Customer() {
                Phone = rawOrder.Phone,
                Name = rawOrder.Name,
                Address = rawOrder.Address,
            };

            // Validation for Customer
            Customer? foundCustomer = await _customerRepository.GetByPhone(takeRawCustomer.Phone);
            // if not found Customer -> create Customer
            if (foundCustomer == null) {
                // Must have Phone and Name
                if (takeRawCustomer.Name == "") throw new ApiException((int) HttpStatusCode.UnprocessableEntity, "Field Name must not empty");
            }

            // Validation for OrderItems And Product
            if(rawOrder.OrderItems.Count() == 0) throw new ApiException((int)HttpStatusCode.UnprocessableEntity, "Empty List of OrderItems");
            // List OrderItems
            List<OrderItem> orderItems = new();
            int totalMoney = 0;
            foreach (var item in rawOrder.OrderItems) {
                Product product = await _productRepository.GetById(item.ProductId);

                int totalInOrderItem = item.Quantity * product.RetailPrice;
                orderItems.Add(new OrderItem {
                    Product = product,
                    Quantity = item.Quantity,
                    TotalAmount = totalInOrderItem
                });
                totalMoney += totalInOrderItem;
            }

            Order order = _mapper.Map<Order>(rawOrder);
            
            order.TotalAmount = totalMoney;
            order.OrderItems = orderItems;

            // Check if not receive enough money
            if (order.CashBackAmount < 0) throw new ApiException((int)HttpStatusCode.UnprocessableEntity, "Customer doesn't give enough money");

            order.Customer = foundCustomer ?? await _customerRepository.Add(_mapper.Map<Customer>(takeRawCustomer));

            return await Add(order);
        }
    }
}
