using System;
using static PracticeLeetcode.LinkedList;

namespace PracticeLeetcode
{
	public class LinkedList
	{
		public class node
		{
			public node next;
			public int val;
			public node(int val)
			{
				this.val = val;
			}
		}
		public LinkedList()
		{


		}
		static node getMiddle(node head)
		{
			if (head == null || head.next == null)
			{
				return head;
			}
			node slow = head;
			node fast = head.next;
			while (fast != null)
			{
				fast = fast.next;
				if (fast != null)
					fast = fast.next;
				slow = slow.next;
			}
			return slow;

		}
		static int getLength(node head)
		{
			int length = 0;
			while (head != null)
			{
				length++;
				head = head.next;

			}
			int midlength = length / 2;
			return midlength;

		}
		static node getMiddleDifferentSol(node head)
		{
			int midlength = getLength(head);


			int count = 0;
			node middleNode = head;
			while (count < midlength)
			{
				middleNode = middleNode.next;
				count++;
			}
			return middleNode;

		}
		static void removeDuplicates(node head)
		{
			HashSet<int> hs = new HashSet<int>();
			//pick element one by one
			node current = head;
			node previous = null;
			while (current != null)
			{
				if (hs.Contains(current.val))
				{ previous.next = current.next; }
				else
				{
					hs.Add(current.val);
					previous = current;

				}
				current = current.next;
			}
		}
		//print linkedlist
		static void printList(node head)
		{
			while (head != null)
			{
				Console.Write(head.val + " ");
				head = head.next;
			}
		}


		//the linked list
		//	10->12->11->11->12->11->10
		public static void linkedlist()
		{

			node start = new node(10);
			start.next = new node(12);
			start.next.next = new node(11);
			start.next.next.next = new node(11);
			start.next.next.next.next = new node(12);
			start.next.next.next.next.next = new node(11);
			start.next.next.next.next.next.next = new node(10);
			Console.WriteLine("Linkedlist before removing duplicates");

			node middle = getMiddle(start);
			Console.WriteLine("Middle node " + middle.val);
			printList(start);
			removeDuplicates(start);

			Console.WriteLine("\nLinked list after removing duplicates");
			node mid = getMiddleDifferentSol(start);
			Console.WriteLine("Middle node " + mid.val);
			printList(start);

			ListNode list1 = new ListNode(1);
			list1.next = new ListNode(2);
			list1.next.next = new ListNode(3);
			ListNode list2 = new ListNode(1);
			list2.next = new ListNode(4);
			list2.next.next = new ListNode(5);
			ListNode[] lists = new ListNode[] { list1, list2, list1 };
			//MergeKLists(lists);
			ListNode reverse = new ListNode(1);
			reverse.next = new ListNode(2);
			reverse.next.next = new ListNode(3);
			ReorderList(reverse);
			ReverseListWithListNodeInput(reverse);
			ReverseList(reverse);
			ReverseListRecursive(reverse);
			Node.main();

			ListNode l1 = new ListNode(2);
			l1.next = new ListNode(4);
			l1.next.next = new ListNode(3);
			ListNode l2 = new ListNode(5);
			l2.next = new ListNode(6);
			l2.next.next = new ListNode(4);
			AddTwoNumbers(l1, l2);



		}
		public class ListNode
		{
			public int val;
			public ListNode? next;
			public ListNode(int val = 0, ListNode? next = null)
			{
				this.val = val;
				this.next = next;
			}

		}

		//Merge K sorted List
		public static ListNode MergeKLists(ListNode[] lists)
		{
			//base conditions
			if (lists == null || lists.Length == 0)
			{ return null; }
			if (lists.Length == 1)
			{
				return lists[0];
			}
			ListNode merged = lists[0];
			for (int i = 1; i < lists.Length; i++)
			{
				merged = MergeTwoList(merged, lists[i]);

			}
			return merged;
		}

		static ListNode MergeTwoList(ListNode list1, ListNode list2)
		{
			ListNode res = new ListNode();
			ListNode currentNode = res;
			//basecase
			if (list1 == null)
			{
				return list2;
			}
			if (list2 == null)
			{
				return list1;
			}
			while (list1 != null && list2 != null)
			{
				if (list1.val <= list2.val)
				{
					currentNode.next = list1;
					list1 = list1.next;
				}
				else
				{
					currentNode.next = list2;
					list2 = list2.next;
				}
				currentNode = currentNode.next;
			}
			if (list1 == null)
			{
				currentNode.next = list2;
			}
			if (list2 == null)
			{
				currentNode.next = list1;
			}
			return res.next;
		}

		static public ListNode ReverseList(ListNode head)
		{

			Stack<int> stack = new Stack<int>();
			while (head != null)
			{
				stack.Push(head.val);
				head = head.next;
			}
			ListNode reverse = new ListNode();
			ListNode ptr = reverse;

			while (stack.Count > 0)
			{
				ptr.next = new ListNode(stack.Pop());

				ptr = ptr.next;



			}

			return reverse;
		}

		static public ListNode ReverseListRecursive(ListNode head)
		{
			//base condition
			if (head.next == null || head == null)
			{
				return head;
			}

			ListNode newHead = ReverseListRecursive(head.next);
			head.next.next = head;
			head.next = null;
			return newHead;

		}
		static public ListNode ReverseListWithListNodeInput(ListNode head)
		{

			Stack<ListNode> stack = new Stack<ListNode>();
			while (head != null)
			{
				stack.Push(head);
				head = head.next;
			}
			ListNode reverse = stack.Pop();
			ListNode ptr = reverse;

			while (stack.Count > 0)
			{
				ptr.next = stack.Pop();

				ptr = ptr.next;



			}
			ptr.next = null;

			return reverse;
		}

		static public void ReorderList(ListNode head)
		{

			ListNode slow = head;
			ListNode fast = head.next;
			// Find the middle of the list
			while (fast != null && fast.next != null)
			{
				slow = slow.next;
				fast = fast.next.next;

			}
			// Split the list into two halves
			ListNode partition2 = slow.next;
			slow.next = null;

			// Reverse the second half of the list
			ListNode prev = null;
			ListNode curr = partition2;
			ListNode next = null;
			while (curr != null)
			{
				next = curr.next;
				curr.next = prev;
				prev = curr;
				curr = next;

			}
			//Merge two halves alterntavie
			ListNode partition1 = head;
			partition2 = prev;
			while (partition2 != null)
			{
				ListNode temp1 = partition1.next;
				ListNode temp2 = partition2.next;
				partition1.next = partition2;
				partition2.next = temp1;
				partition1 = temp1;
				partition2 = temp2;
			}
		}
		public class Node
		{
			int val;
			Node next;
			Node random;
			Node(int val)
			{
				this.val = val;
				this.next = null;
				this.random = null;
			}



			static public Node CopyRandomList(Node head)
			{
				Dictionary<Node, Node> dict = new Dictionary<Node, Node>();

				Node curr = head;
				//create new nodes and add them to dictionary with original node
				while (curr != null)
				{
					Node copy = new Node(curr.val);
					dict.Add(curr, copy);
					curr = curr.next;
				}
				//create random and next links for thesenodes
				curr = head;
				while(curr!=null)
				{
					if(dict.ContainsKey(curr))
					{
						dict[curr].next =curr.next==null? null: dict[curr.next];
						dict[curr].random = curr.random==null? null : dict[curr.random];
						curr = curr.next;
					}
					
				}
				curr = head;

				return dict[curr];


			}

			static public void main()
			{
                //[[7,null],[13,0],[11,4],[10,2],[1,0]]
                Node node1 = new Node(7);
				node1.next = new Node(13);
                node1.next.next = new Node(11);
				node1.next.next.next = new Node(10);
                node1.random = null;
			    node1.next.random = node1.next.next;
                node1.next.next.random = node1.next.next.next;
                node1.next.next.next.random = node1;


				CopyRandomList(node1);
			

			



            }
		}

		static public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			int carry = 0;
			ListNode dummy = new ListNode(0);
			ListNode l3 = dummy;
           
            while (l1 != null || l2 != null ||carry !=0)
			{
				int l1val = (l1 == null) ? 0 : l1.val;
				int l2val = (l2 == null) ? 0 : l2.val;
				int digitsum = l1val + l2val + carry;
				
					carry = digitsum / 10;
					digitsum = digitsum % 10;

				
                l3.next = new ListNode(digitsum);
				l3 = l3.next;
				if(l1!=null)
				l1 = l1.next;
				if(l2!=null)
				l2 = l2.next;


            }
			return dummy.next;

		}


        }
    }

