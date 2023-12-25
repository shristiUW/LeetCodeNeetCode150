using System;
using System.Text;
using static PracticeLeetcode.BinaryTree;
using static PracticeLeetcode.LinkedList;

namespace PracticeLeetcode
{
	public class BinaryTree
	{
		public BinaryTree()
		{

		}
		public class TreeNode
		{
			public int val;
			public TreeNode left;
			public TreeNode right;
			public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
			{
				this.val = val;
				this.left = left;
				this.right = right;
			}

		}
		public static int maxDepth(TreeNode root)
		{
			int depth = 0;
			if (root != null)
			{
				int left = maxDepth(root.left);
				int right = maxDepth(root.right);
				return (Math.Max(left, right)) + 1;


				//depth = Math.Max(maxDepth(root.left), maxDepth(root.right));

				//return depth + 1;


			}
			else
			{ return 0; }
		}
		public static void main()
		{
			TreeNode root = new TreeNode(3);
			root.left = new TreeNode(9);
			root.right = new TreeNode(20);
			root.right.left = new TreeNode(15);
			root.right.right = new TreeNode(7);

			//Console.WriteLine("Max Depth : " + maxDepthusingQueue(root));
			Console.WriteLine("Max Depth : " + maxDepth(root));
			Console.WriteLine("Max Depth : " + MaxDepth(root));
			//Console.WriteLine(maxDepthUsingStacks(root));

			TreeNode root1 = new TreeNode(5);
			root1.left = new TreeNode(4);
			root1.right = new TreeNode(7);
			root1.right.left = new TreeNode(3);
			root1.right.right = new TreeNode(8);

			if (isValidBinaryTree(root1))
			{
				Console.WriteLine("This is Binary Tree");
			}
			else
			{
				Console.WriteLine("This is not a binary tree");
			}

			levelOrder(root);
			GoodNodes(root1);
			MaxPathSum(root1);
			deserialize(serialize(root1));
			DiameterOfBinaryTree(root1);
			//balancedBinaryTree(root1);

			//[1,2,2,3,null,null,3,4,null,null,4]
			TreeNode root2 = new TreeNode(1);
			root2.left = new TreeNode(2);
			root2.right = new TreeNode(2);
			root2.left.left = new TreeNode(3);
			root2.right.right = new TreeNode(4);
			root2.left.left.left = new TreeNode(3);
			root2.right.right.right = new TreeNode(4);
			root2.left.left.left.left = new TreeNode(4);

			balancedBinaryTree(root2);
			IsbalancedBinaryTree(root2);
			TreeNode root3 = new TreeNode(1);
			root3.left = new TreeNode(2);
			root3.right = new TreeNode(3);
			root3.left.left = new TreeNode(4);
			root3.right.right = new TreeNode(5);
			root3.left.right = new TreeNode(6);
			root3.right.right = new TreeNode(7);
			InvertTree(root2);

			TreeNode subtree = new TreeNode(2);
			subtree.left = new TreeNode(4);
			subtree.right = new TreeNode(6);
			IsSubTree(root3, subtree);

			TreeNode root4 = new TreeNode(3);
			root4.left = new TreeNode(1);
			root4.left.left = new TreeNode(6);
			root4.right = new TreeNode(4);
			root4.right.left = new TreeNode(1);
			root4.right.right = new TreeNode(5);
			GoodNodesAnotherApproach(root4);
			int[] arr = { 5,3,6,2,4,1};
			TreeNode root5= null;
			for(int i=0; i<arr.Length; i++)
			{
				root5 = insertBST(root5, arr[i]);
			}
			KthSmallest(root5, 3);

			int[] preorder = new int[] { 3, 9, 20, 15, 7 };
			int[] inorder = new int[] { 9, 3, 15, 20, 7 };

			BuildTree(preorder, inorder);

		}
		static TreeNode insertBST(TreeNode root, int val)
		{
			if(root==null)
			{ return new TreeNode(val);
			}
			if(val<root.val)
			{ 
				root.left = insertBST( root.left,  val);
			}
            if (val > root.val)
            {
                root.right = insertBST(root.right, val);
            }
			return root;
        }
		public static int maxDepthUsingStacks(TreeNode root)
		{
			if (root != null)
			{
				var s = new Stack<(TreeNode, int)>();
				s.Push((root, 1));
				var height = 0;
				while (s.Count > 0)
				{
					var (curr, currentDepth) = s.Pop();
					height = Math.Max(height, currentDepth);
					if (curr.left != null) s.Push((curr.left, currentDepth + 1));
					if (curr.right != null) s.Push((curr.right, currentDepth + 1));

				}
				return height;
			}
			return 0;

		}


		public static int maxDepthusingQueue(TreeNode root)
		{
			if (root == null)
			{
				return 0;
			}
			var depth = 0;

			var queue = new Queue<TreeNode>();
			queue.Enqueue(root);
			while (queue.Count != 0)
			{
				int count = queue.Count;

				for (int i = 0; i < count; i++)
				{
					var current = queue.Dequeue();


					if (current.left != null)
					{
						queue.Enqueue(current.left);

					}
					if (current.right != null)
					{
						queue.Enqueue(current.right);
					}
				}
				depth++;
			}
			return depth;

		}
		public static bool isValidBinaryTree(TreeNode root)
		{
			return valid(root, null, null);
		}
		public static bool valid(TreeNode root, int? min, int? max)
		{
			if (root == null)
				return true;
			if (root.val <= min || root.val >= max)
				return false;
			return valid(root.left, min, root.val) && valid(root.right, root.val, max);


		}
		public static IList<IList<int>> levelOrder(TreeNode root)
		{
			if (root == null)
			{
				return null;
			}
			List<IList<int>> res = new List<IList<int>>();

			Queue<TreeNode> queue = new Queue<TreeNode>();
			queue.Enqueue(root);
			while (queue.Count != 0)
			{
				List<int> level = new List<int>();
				int count = queue.Count;
				for (int i = 0; i < count; i++)
				{
					TreeNode node = queue.Dequeue();
					if (node != null)
					{
						level.Add(node.val);
						queue.Enqueue(node.left);
						queue.Enqueue(node.right);
					}


				}
				if (level != null)
				{ res.Add(level); }


			}
			return res;
		}
		static int res = 0;
		public static int GoodNodes(TreeNode root)
		{

			HelperGoodNodes(root, root.val);
			return res;
		}

		public static void HelperGoodNodes(TreeNode node, int maxValue)
		{
			if (node == null)
				return;

			if (node.val >= maxValue)
			{
				res++;
			}
			maxValue = Math.Max(node.val, maxValue);
			HelperGoodNodes(node.left, maxValue);
			HelperGoodNodes(node.right, maxValue);

		}

		/* https://www.youtube.com/watch?v=TO5zsKtc1Ic */

		public static int MaxPathSum(TreeNode root)
		{
			if (root == null)
			{
				return 0;
			}
			int result = int.MinValue;
			dfs(root);
			int dfs(TreeNode node)
			{
				//basecase
				if (node == null)
				{
					return 0;
				}
				int left = dfs(node.left);
				int right = dfs(node.right);
				int max_straight = Math.Max(Math.Max(left, right) + node.val, node.val); //include one of left or right
				int max_othercase = Math.Max(left + right + node.val, max_straight); //include left and right both
				result = Math.Max(result, max_othercase);
				return max_straight;

			}

			return result;
		}
		// Encodes a tree to a single string.
		public static string serialize(TreeNode root)
		{
			if (root == null)
			{
				return "";
			}
			Queue<TreeNode> queue = new Queue<TreeNode>();
			StringBuilder str = new StringBuilder();
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				int count = queue.Count;
				for (int i = 0; i < count; i++)
				{
					TreeNode node = queue.Dequeue();

					if (node == null)
					{
						str.Append("N");
						str.Append(" ");
					}
					else
					{

						str.Append(node.val);
						str.Append(" ");

					}
					if (node != null)
					{
						queue.Enqueue(node.left);
						queue.Enqueue(node.right);
					}
				}


			}

			return str.ToString();

		}

		// Decodes your encoded data to tree.
		public static TreeNode deserialize(string data)
		{

			if (data == "")
			{
				return null;
			}
			string[] str = data.Split(" ");
			Queue<TreeNode> queue = new Queue<TreeNode>();
			TreeNode root = new TreeNode(Convert.ToInt32(str[0]));
			queue.Enqueue(root);
			for (int i = 1; i < str.Length - 1; i += 2)
			{
				TreeNode node = queue.Dequeue();
				if (str[i] != "N")
				{
					node.left = new TreeNode(Convert.ToInt32(str[i]));
					queue.Enqueue(node.left);
				}
				if (str[i + 1] != "N")
				{
					node.right = new TreeNode(Convert.ToInt32(str[i + 1]));
					queue.Enqueue(node.right);
				}

			}

			return root;

		}
		static public int MaxDepth(TreeNode root)
		{
			//base condition
			if (root == null)
			{ return 0; }

			int left = MaxDepth(root.left) + 1;
			int right = MaxDepth(root.right) + 1;
			return Math.Max(left, right);
		}

		static public int DiameterOfBinaryTree(TreeNode root)
		{
			int[] diameter = new int[1];
			height(root, diameter);
			return diameter[0];
		}
		static private int height(TreeNode root, int[] diameter)
		{
			if (root == null)
			{
				return 0;
			}
			int left = height(root.left, diameter);
			int right = height(root.right, diameter);
			diameter[0] = Math.Max(diameter[0], left + right);
			return 1 + Math.Max(left, right);
		}

		static bool balancedBinaryTree(TreeNode root)
		{
			if (root == null)
			{
				return true;

			}
			bool[] balance = new bool[1];
			dfsHeight(root, balance);
			return balance[0];

		}
		static int dfsHeight(TreeNode root, bool[] balance)
		{

			if (root == null)
			{
				return 0;
			}
			int left = dfsHeight(root.left, balance);
			int right = dfsHeight(root.right, balance);
			balance[0] = Math.Abs(left - right) <= 1 ? true : false;
			return Math.Max(left, right) + 1;
		}

		public static TreeNode InvertTree(TreeNode root)
		{
			//basecase
			if (root == null)
			{
				return null;
			}

			TreeNode temp = root.left;
			root.left = root.right;
			root.right = temp;
			InvertTree(root.left);
			InvertTree(root.right);
			return root;
		}

		public static bool IsSubTree(TreeNode root, TreeNode subtree)
		{
			if (subtree == null)
				return true;
			if (root == null)
			{
				return false;
			}
			if (sametree(root, subtree))
			{
				return true;

			}
			bool left = IsSubTree(root.left, subtree);
			bool right = IsSubTree(root.right, subtree);
			if (left || right)
			{
				return true;
			}
			else
			{
				return false;
			}



		}
		public static bool sametree(TreeNode root, TreeNode subtree)
		{
			if (root == null && subtree == null)
			{
				return true;
			}
			if (root != null && subtree != null && root.val == subtree.val)
			{
				bool left = sametree(root.left, subtree.left);
				bool right = sametree(root.right, subtree.right);
				return (left && right);
			}
			else
			{
				return false;
			}
		}



		static bool IsbalancedBinaryTree(TreeNode root)
		{
			if (root == null)
			{
				return true;
			}
			if (dfsHeight(root) == -1)
			{
				return false;

			}
			else
			{
				return true;
			}
			int dfsHeight(TreeNode root)
			{
				if (root == null)
				{
					return 0;
				}
				int left = dfsHeight(root.left);
				if (left == -1)
				{
					return -1;
				}
				int right = dfsHeight(root.right);
				if (right == -1)
				{
					return -1;
				}
				int height = Math.Max(left, right) + 1;
				if (Math.Abs(left - right) > 1)
					return -1;
				else
					return height;


			}




		}

		static public int GoodNodesAnotherApproach(TreeNode root)
		{
			return dfsGoodNode(root, root.val);

		}
		static public int dfsGoodNode(TreeNode root, int max)
		{
			//base condition
			if (root == null)
			{
				return 0;
			}
			int count = 0;
			if (root.val >= max)
			{
				max = root.val;
				count++;
			}

			count += dfsGoodNode(root.left, max);
			count += dfsGoodNode(root.right, max);
			return count;

		}


        //[5,3,6,2,4,null,null,1]
        static public int KthSmallest(TreeNode root, int k)
		{

			//Hint : it is binary search tree. All the smallest will be in the left side of the root.
			int index = 0; int val = 0;
            dfs(root);
			return val;
		

			void dfs(TreeNode root)
			{
				if(root==null)
				{
					return;

				}
		     dfs(root.left);
				
				index++;
				if(index==k)
				{
                    val= root.val;
				}
			 dfs(root.right);
			}
		}
        //preorder = [3,9,20,15,7], inorder = [9,3,15,20,7]
        static public TreeNode BuildTree(int[] preorder, int[] inorder)
		{

			if(!preorder.Any() || !inorder.Any())
			{
				return null;

			}

			TreeNode root = new TreeNode(preorder[0]);
			int middle = Array.IndexOf(inorder, preorder[0]);
			root.left = BuildTree(preorder[1..(middle + 1)], inorder[..middle]);
			root.right = BuildTree(preorder[(middle + 1)..], inorder[(middle+1)..]);
			return root;

        }



		// Encodes a tree to a single string.
		public string serializePractice(TreeNode root)
		{
			if(root==null)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			Queue<TreeNode> queue = new Queue<TreeNode>();
			queue.Enqueue(root);
			while(queue.Count>0)
			{
				int count = queue.Count;
				for(int i=0; i<count; i++)
				{
					TreeNode node = queue.Dequeue();
					if(node==null)
					{
						sb.Append("N");
						sb.Append(" ");
					}
					else
					{
						sb.Append(node.val);
						sb.Append(" ");
					}
					if (node != null)
					{
						queue.Enqueue(node.left);
						queue.Enqueue(node.right);
					}
                }
			}

			return sb.ToString();

		}

		// Decodes your encoded data to tree.
		public TreeNode deserializePractice(string data)
		{
			if(data.Length==0)
			{
				return null;
			}
			string[] str = data.Split(" ");
			Queue<TreeNode> queue = new Queue<TreeNode>();
			TreeNode root = new TreeNode(Convert.ToInt32(str[0]));
			queue.Enqueue(root);
			for(int i=1; i<str.Length; i+=2)
			{
				TreeNode node = queue.Dequeue();
				if (str[i] !="N")
				{
                    node.left = new TreeNode(Convert.ToInt32(str[i]));
                    queue.Enqueue(node.left);
                }
				if (str[i + 1] != "N")
				{

					node.right = new TreeNode(Convert.ToInt32(str[i + 1]));

					queue.Enqueue(node.right);
				}
            }
			return root;
		}


        }
}
    