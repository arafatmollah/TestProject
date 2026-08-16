using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Orders.CreateOrder
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderDomainService _orderDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderService(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            OrderDomainService orderDomainService,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDomainService = orderDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> CreateAsync(
            CreateOrderDto dto,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var products = new List<(
                ProductManagement.Domain.Entities.Product Product,
                int Quantity)>();

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(
                    item.ProductId,
                    cancellationToken);

                if (product == null)
                {
                    throw new KeyNotFoundException(
                        $"Product with id '{item.ProductId}' was not found.");
                }

                products.Add((product, item.Quantity));
            }

            var order = _orderDomainService.CreateOrder(
                userId,
                products);

            await _orderRepository.AddAsync(
                order,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,

                Items = order.OrderItems
                    .Select(item => new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    })
                    .ToList()
            };
        }
    }
}
