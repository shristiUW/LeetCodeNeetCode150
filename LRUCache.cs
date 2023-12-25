using System;
namespace PracticeLeetcode
{
	
	public class LRUCache
	{

		public class Node
		{
			public int key;
            public int val;
            public Node next;
            public Node prev;
			public Node(int key, int val, Node next=null, Node prev=null)
			{
				this.key = key;
				this.val = val;
				this.next = next;
				this.prev = prev;
			}
		}
		private readonly int capacity;
		private readonly Dictionary<int, Node> dict;
		Node leftDummy;
		Node rightDummy;

		public LRUCache(int capacity)
		{
			this.capacity = capacity;
			dict   = new Dictionary<int, Node>();
			leftDummy = new Node(0, 0);
            rightDummy = new Node(0, 0);
			
        }

		private void delete(Node item)
		{
		
			item.prev.next = item.next;
			item.next.prev = item.prev;
		}
		//inser before right dummy node
		private void insert(Node item)
		{
            item.prev = rightDummy.prev;
			rightDummy.prev.next = item;
            rightDummy.prev = item;
			item.next = rightDummy;
		
		}

		public int Get(int key)
		{
			if(dict.ContainsKey(key))
			{
				delete(dict[key]);
				insert(dict[key]);
				return dict[key].val;

			}
			else
			{
				return -1;
			}
		}

		public void Put(int key, int value)
		{
			if(dict.ContainsKey(key))
			{
				delete(dict[key]);
				dict.Remove(key);
			}
			dict.Add(key, new Node(key, value));
			insert(dict[key]);
			if(dict.Count>capacity)
			{
				var lru = leftDummy.next;
				var lrukey = leftDummy.next.key;
				delete(lru);
				dict.Remove(lrukey);
			}
		}
        }
}

