using SPR421_Shop.ViewModels;
using System.Text.Json;

namespace SPR421_Shop.Services
{
    public static class CartService
    {
        public static void AddToCart(this ISession session, int productId)
        {
            if (!session.ItemInCart(productId))
            {
                var items = session.CartItems();
                items.Add(new CartItemVM { ProductId = productId });
                session.SetCartItems(items);
            }
        }

        public static void SetCartItems(this ISession session, List<CartItemVM> items)
        {
            string json = JsonSerializer.Serialize(items);
            session.SetString(Settings.CartKey, json);
        }

        public static List<CartItemVM> CartItems(this ISession session)
        {
            var json = session.GetString(Settings.CartKey);
            if (json == null)
            {
                return new List<CartItemVM>();
            }

            var items = JsonSerializer.Deserialize<List<CartItemVM>>(json);
            return items == null ? new List<CartItemVM>() : items;
        }

        public static int GetCartCount(this ISession session)
        {
            var items = session.CartItems();
            return items.Count;
        }

        public static bool ItemInCart(this ISession session, int productId)
        {
            var items = session.CartItems();
            return items.Any(i => i.ProductId == productId);
        }
    }
}
