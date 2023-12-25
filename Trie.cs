using System;
using System.Text;
namespace PracticeLeetcode
{

	public class Trie
	{
       static public void main()
        {
            char[][] board = new char[][]
            {
                new char[]{ 'o','a','a','n' },
                new char[]{ 'e', 't', 'a', 'e' },
                new char[]{ 'i', 'h', 'k', 'r' },
                new char[]{ 'i', 'f', 'l', 'v' }
            };
            string[] words = new string[] {"eat","oath"};
            Trie trie = new Trie();
            trie.FindWords(board, words);
        }

		public class TrieNode
		{
			public Dictionary<char, TrieNode> children { get; set; }
			public bool isWordEnd { get; set; }
            
            public TrieNode()
			{
				children = new Dictionary<char, TrieNode>();
				isWordEnd = false;
			}
            

        }
        public TrieNode root { get; set; }
        public Trie()
		{
            root = new TrieNode();
        }
        public void AddWord(string word)
        {

            TrieNode curr = root;
            foreach (char c in word)
            {
                if (!curr.children.ContainsKey(c))
                {
                    curr.children.Add(c, new TrieNode());
                }
                
                    curr = curr.children[c];
                
            }
            curr.isWordEnd = true;
        }
        public bool Search(string word)
        {
            var curr = root;
            foreach (char c in word)
            {
                if (curr.children.ContainsKey(c))
                {
                    curr = curr.children[c];
                }
                else
                {
                    return false;
                }
            }
                return curr.isWordEnd;

        }

        public bool StartsWith(string prefix)
        {
            var curr = root;
            foreach(char c in prefix)
            {
                if(!curr.children.ContainsKey(c))
                {
                    return false;
                }
                else
                {
                    curr = curr.children[c];

                }

            }
            return true;
        }


            public IList<string> FindWords(char[][] board, string[] words)
        {
            Trie trie = new Trie();
            

            foreach (string word in words)
			{
                trie.AddWord(word);
			}
            int rows = board.Length;
            int columns = board[0].Length;
            List<string> res = new List<string>();
            HashSet<(int,int)> visited = new HashSet<(int, int)>();
            StringBuilder str = new StringBuilder();
            for(int i=0; i<rows; i++)
            {
                for(int j=0;j<columns; j++)
                {

                    dfs(i, j, trie.root, str);
                }
            }
            return res;
           void dfs(int r , int c, TrieNode node, StringBuilder word)
            {
                if(r<0 || c<0 ||r>=rows ||c >=columns || visited.Contains((r,c))|| !node.children.ContainsKey(board[r][c]))
                    {
                    return;
                }
                visited.Add((r, c));
                node = node.children[board[r][c]];
                word.Append(board[r][c]);
                if(node.isWordEnd)
                {
                    res.Add(word.ToString());
                    return;
                }
                dfs(r + 1, c, node, word);
                dfs(r, c + 1, node, word);
                dfs(r - 1, c, node, word);
                dfs(r, c - 1, node, word);
                visited.Remove((r, c));
                word.Clear();


            }



        }

    }
}


