using SPR421_Shop.Models;

namespace SPR421_Shop.ViewModels
{
    public class CartVM
    {
        private int count = 1;
        public required Product Product { get; set; }
        public int Count { 
            get
            {
                return count;
            }
            set
            {
                if (value < 1)
                {
                    count = 1;
                }
                else if(value > Product.Amount)
                {
                    count = Product.Amount;
                }
                else
                {
                    count = value;
                }
            }
        }
        public double TotalPrice => Product.Price * Count;
    }
}
