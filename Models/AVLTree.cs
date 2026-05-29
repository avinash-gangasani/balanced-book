using System.Xml.Linq;

namespace BalancedBook.NET.Models
{
    public class AVLTree
    {
        public AVLTreeNode Root { get; set; }
        private List<Order> Orders { get; set; }
        public int nodeCount { get; set; }

        public AVLTree()
        {
            Orders = new List<Order>();
        }

        public List<Order> Insert(Order order, int k)
        {
            if (nodeCount >= k)
            {
                AVLTreeNode node = null;
                if (order.Type == Enums.OrderType.Sell)
                {
                    node = Max(Root);
                }
                else
                {
                    node = Min(Root);
                }
                node = null;
            }

            Root = Insert(Root, order);
            nodeCount++;
            Orders.Clear();

            if (order.Type == Enums.OrderType.Sell) SortAsc(k);
            else SortDesc(k);

            return Orders;
        }

        public void Delete(int price)
        {
            Root = Delete(Root, price);
        }

        #region Private methods
        private void SortAsc(int k)
        {
            SortAsc(k, Root);
        }

        private void SortDesc(int k)
        {
            SortDesc(k, Root);
        }
        private AVLTreeNode Delete(AVLTreeNode node, double price)
        {
            if (node == null)
                return null;

            // Traverse tree
            if (price < node.Value.Price)
            {
                node.Left = Delete(node.Left, price);
            }
            else if (price > node.Value.Price)
            {
                node.Right = Delete(node.Right, price);
            }
            else
            {
                // NODE FOUND

                // Case 1: no children
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }

                // Case 2: one child
                if (node.Left == null)
                {
                    return node.Right;
                }

                if (node.Right == null)
                {
                    return node.Left;
                }

                var predecessor = Max(node.Left);

                node.Value = predecessor.Value;

                node.Left = Delete(node.Left, predecessor.Value.Price);
            }

            SetHeight(node);

            return Balance(node);
        }

        private void SortAsc(int k, AVLTreeNode node)
        {
            if (Orders.Count == k) return;

            if (node == null) return;

            SortAsc(k, node.Left);

            if (Orders.Count == k) return;
            Orders.Add(node.Value);

            SortAsc(k, node.Right);
        }

        private void SortDesc(int k, AVLTreeNode node)
        {
            if (Orders.Count == k) return;

            if (node == null) return;

            SortDesc(k, node.Right);

            if (Orders.Count == k) return;
            Orders.Add(node.Value);

            SortDesc(k, node.Left);
        }

        private AVLTreeNode Insert(AVLTreeNode node, Order order)
        {
            if (node == null)
            {
                node = new AVLTreeNode(order);
                return node;
            }

            if (node.Value.Price > order.Price)
            {
                node.Left = Insert(node.Left, order);
            }
            else if (node.Value.Price < order.Price)
            {
                node.Right = Insert(node.Right, order);
            }
            else
            {
                node.Value.Quantity += order.Quantity;
                nodeCount--;
                return node;
            }

            SetHeight(node);
            return Balance(node);
        }

        private void SetHeight(AVLTreeNode node)
        {
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private AVLTreeNode Balance(AVLTreeNode node)
        {
            if (LeftHeavy(node))
            {
                if (BalanceFactor(node.Left) < 0)
                {
                    node.Left = LeftRotate(node.Left);
                }
                var root = RightRotate(node);
                return root;
            }
            else if (RightHeavy(node))
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    node.Right = RightRotate(node.Right);
                }
                var root = LeftRotate(node);
                return root;
            }
            return node;
        }

        private int BalanceFactor(AVLTreeNode node)
        {
            return (node == null) ? 0 : Height(node.Left) - Height(node.Right);
        }

        private bool LeftHeavy(AVLTreeNode node)
        {
            return BalanceFactor(node) > 1;
        }

        private bool RightHeavy(AVLTreeNode node)
        {
            return BalanceFactor(node) < -1;
        }

        private AVLTreeNode LeftRotate(AVLTreeNode node)
        {
            var newRoot = node.Right;
            var transferred = newRoot.Left;

            newRoot.Left = node;
            node.Right = transferred;

            SetHeight(node);
            SetHeight(newRoot);

            return newRoot;
        }

        private AVLTreeNode RightRotate(AVLTreeNode node)
        {
            var newRoot = node.Left;
            var transferred = newRoot.Right;

            newRoot.Right = node;
            node.Left = transferred;

            SetHeight(node);
            SetHeight(newRoot);

            return newRoot;
        }

        private int Height(AVLTreeNode node)
        {
            if (node == null) return -1;
            return node.Height;
        }

        private AVLTreeNode Max(AVLTreeNode node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }
        private AVLTreeNode Min(AVLTreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        #endregion
    }
}
