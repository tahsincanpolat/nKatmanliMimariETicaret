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
        public IActionResult AddToCart(int productId,int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User),productId,quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteToCart(_userManager.GetUserId(User),productId);
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
                        ProductId = i.Id,
                        Name = i.Product.Name,
                        Price = i.Product.Price,
                        ImageUrl = i.Product.Images[0].ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };

                // Ödeme 
                var payment = PaymentProcess(model);

                if(payment.Status == "success")
                {
                    SaveOrder(model,payment,userId);
                    ClearCart(cart.Id.ToString());
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

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
            buyer.GsmNumber = "+905446556565";
            buyer.Email = model.Email;
            buyer.IdentityNumber = "235235235252"; // Ön yüzde yok eklenecek.
            buyer.LastLoginDate = "2023-05-27 11:11:35";
            buyer.RegistrationDate = "2014-10-05 12:43:35";
            buyer.RegistrationAddress = model.Address;
            buyer.Ip = "85.108.129.11";
            buyer.City = model.City;
            buyer.Country = "TURKEY";
            buyer.ZipCode = "34000";
            request.Buyer = buyer;


            Address shippingAddress = new Address();
            shippingAddress.ContactName = model.FirstName;
            shippingAddress.City = model.City;
            shippingAddress.Country = "TURKEY";
            shippingAddress.Description = model.Address;
            shippingAddress.ZipCode = "34000";
            request.ShippingAddress = shippingAddress;

            Address billingAddres = new Address();
            billingAddres.ContactName = model.FirstName;
            billingAddres.City = model.City;
            billingAddres.Country = "TURKEY";
            billingAddres.Description = model.Address;
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
                basketItem.Price = item.Price.ToString();
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request,options);

            return payment;

        }

        public void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order()
            {
                OrderNumber = new Guid().ToString(),
                OrderState = EnumOrderState.completed,
                PaymentTypes = EnumPaymentTypes.CreditCart,
                PaymentId = payment.PaymentId,
                PaymentToken = Guid.NewGuid().ToString(),
                ConversionId = payment.ConversationId,
                OrderDate = new DateTime(),
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
    }
}
