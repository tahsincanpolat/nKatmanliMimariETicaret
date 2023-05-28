using ETICARET.Business.Abstract;
using ETICARET.WebUI.Identity;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using ETICARET.Entities;
using OrderItem = ETICARET.Entities.OrderItem;
using ETICARET.WebUI.Extensions;

namespace ETICARET.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private IProductService _productService;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, IOrderService orderService, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _orderService = orderService;
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    ImageUrl = i.Product.Images[0].ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteToCart(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    ImageUrl = i.Product.Images[0].ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };

            return View(orderModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            ModelState.Remove("CartModel");

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.Product.Id,
                        Name = i.Product.Name,
                        Price = i.Product.Price,
                        ImageUrl = i.Product.Images[0].ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };

                // Ödeme 
                var payment = PaymentProcess(model);

                if (payment.Status == "success")
                {
                    SaveOrder(model, payment, userId);
                    ClearCart(cart.Id.ToString());
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Order Completed",
                        Message = "Congratulations, your order has been received.",
                        Css = "success"
                    });
                    return View("Success");
                }
            }
            return View("Success");

        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            options.ApiKey = "sandbox-cNnJEaoyNt0sCREL4nOq8PajTLQwWeXz";
            options.SecretKey = "sandbox-cmJxJfaGlVarqNV3c5ZQcMTwVNh8qswx";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.PaidPrice = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = model.CartModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExprationMonth;
            paymentCard.ExpireYear = model.ExprationYear;
            paymentCard.Cvc = model.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;
            // Ödeme servisine statik değerler yolladık
            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905446556565";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "235235235252";
            buyer.LastLoginDate = "2023-05-27 11:11:35";
            buyer.RegistrationDate = "2014-10-05 12:43:35";
            buyer.RegistrationAddress = "Caferağa mah. Kadıköy/İstanbul";
            buyer.Ip = "85.108.129.11";
            buyer.City = "İstanbul";
            buyer.Country = "TURKEY";
            buyer.ZipCode = "34000";
            request.Buyer = buyer;


            Address shippingAddress = new Address();
            shippingAddress.ContactName = "John";
            shippingAddress.City = "İstanbul";
            shippingAddress.Country = "TURKEY";
            shippingAddress.Description = "Caferağa mah. Kadıköy/İstanbul";
            shippingAddress.ZipCode = "34000";
            request.ShippingAddress = shippingAddress;

            Address billingAddres = new Address();
            billingAddres.ContactName = "John";
            billingAddres.City = "İstanbul";
            billingAddres.Country = "TURKEY";
            billingAddres.Description = "Caferağa mah. Kadıköy/İstanbul";
            billingAddres.ZipCode = "34000";
            request.BillingAddress = billingAddres;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Elektronik"; // _productService.GetProductDetails(item.ProductId).ProductCategories.FirstOrDefault().ToString()
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = (item.Price * item.Quantity).ToString().Split(",")[0];
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);

            return payment;

        }

        public void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                OrderState = EnumOrderState.completed,
                PaymentTypes = EnumPaymentTypes.CreditCart,
                PaymentId = payment.PaymentId,
                PaymentToken = Guid.NewGuid().ToString(),
                ConversionId = payment.ConversationId,
                OrderDate = DateTime.Now,
                OrderNote = "Zile Basma", // Modelden alacak / Ödev
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                UserId = userId
            };

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };

                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);
        }

        public void ClearCart(string cartId)
        {
            _cartService.ClearCart(cartId);
        }

        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders(_userManager.GetUserId(User));

            var orderListModel = new List<OrderListModel>();

            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.OrderNote = order.OrderNote;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.Email = order.Email;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.Images[0].ImageUrl

                }).ToList();

                orderListModel.Add(orderModel);
            }

            return View(orderListModel);
        }
    }
}
