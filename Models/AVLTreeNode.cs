namespace BalancedBook.NET.Models
{
    public class AVLTreeNode
    {
        public Order Value { get; set; }
        public AVLTreeNode Right { get; set; }
        public AVLTreeNode Left { get; set; }
        public int Height { get; set; }

        public AVLTreeNode(Order _value)
        {
            this.Value = _value;
        }
    }
}
