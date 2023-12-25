﻿using System;
namespace PracticeLeetcode
{
    public class ArraySum
    {
        public ArraySum()
        { }

        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            Array.ForEach<int>(nums, i => Console.WriteLine(i));
            IList<IList<int>> sol = new List<IList<int>>();
            int l, r;
            int target;
            for (int i = 0; i < nums.Length - 2; i++)
            {
                target = nums[i] * -1;
                l = i + 1;
                r = nums.Length - 1;
                while (l < r)
                {
                    if (nums[l] + nums[r] > target)
                    {
                        --r;

                    }
                    else if (nums[l] + nums[r] < target)
                    {
                        ++l;
                    }
                    else
                    {

                        List<int> result = new List<int>() { nums[i], nums[l], nums[r] };

                        if (!sol.Any(c => c.SequenceEqual(result)))
                        {
                            sol.Add(result);
                        }
                        else { break; }

                    }


                }
            }
            return sol;

        }




        public static int MaxSubArray(int[] nums)
        {
            int MaxSum = 0;
            int sum = 0; ;
            int start = 0;
            int ansStart = -1;
            int ansEnd = -1;

            for (int i = 0; i < nums.Length; i++)
            {

                if (sum < 0)
                {
                    sum = 0;
                }
                if (sum == 0)
                {
                    start = i;
                }
                sum = sum + nums[i];


                MaxSum = Math.Max(sum, MaxSum);

                ansStart = start;
                if (MaxSum == sum)
                {
                    ansEnd = i;
                }

            }
            for (int i = ansStart; i <= ansEnd; i++)
            {
                Console.WriteLine(nums[i]);
            }
            return MaxSum;
        }

        //BruteForce approach for max sub array

        //All subarray of a array
        public static void MaxSubArrayBruteForce(int[] nums)
        {
            List<List<int>> list = new List<List<int>>();
            int maxsum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < nums.Length; j++)
                {
                    // //All subarray of a array
                    /*  List<int> subList = new List<int>();
                     for(int k=i; k<=j; k++)
                      {

                          subList.Add(nums[k]);
                      }

                      list.Add(subList);*/

                    sum += nums[j];
                    maxsum = Math.Max(maxsum, sum);
                }

            }
        }

        public static int maxArea(int[] heights)
        {
            int r = heights.Length - 1;
            int l = 0;
            int area = 0;
            int maxArea = 0;

            while (l < r)
            {
                area = (r - l) * Math.Min(heights[l], heights[r]);
                maxArea = Math.Max(area, maxArea);

                if (heights[l] > heights[r])
                {
                    r--;
                }
                else
                {
                    l++;

                }

            }
            Console.WriteLine("maxArea" + maxArea);
            return maxArea;


        }

        public static void subArrayPrint(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i; j < nums.Length; j++)
                {
                    string str = "";
                    for (int k = i; k <= j; k++)
                    {
                        str += nums[k] + ",";

                    }
                    Console.WriteLine(str);
                }
            }

        }
        public static List<int> getPrefixScores(List<int> arr)
        {

            List<int> res = new List<int>();
            for (int i = 0; i < arr.Count; i++)
            {
                List<int> prefixArray = new List<int>();
                for (int j = 0; j <= i; j++)
                {
                    prefixArray.Add(arr[j]);
                }

                int score = 0;
                for (int j = 0; j < prefixArray.Count; j++)
                {
                    int max = prefixArray.Max();
                    prefixArray[j] = prefixArray[j] + max;
                    score += prefixArray[j];
                }
                res.Add(score % (10 ^ 9 + 7));

            }
            return res;

        }
        public static void main()
        {
            int[] digits = new int[] { 1, 2, 9 };
            PlusOne(digits);
            int[] nums = new int[] { 1, 1, 1, 2, 2, 3 };
            int k = 2;
            TopKFrequent(nums, k);
            int[] prices = new int[] { 7, 1, 5, 3, 6, 4 };
            MaxProfit(prices);

        }

        public static int[] PlusOne(int[] digits)
        {
            int n = digits.Length - 1;
            for (int i = n; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                else
                {
                    digits[i] = 0;
                }

            }
            int[] newdigits = new int[n + 2];
            newdigits[0] = 1;
            return newdigits;

        }

        static public int[] TopKFrequent(int[] nums, int k)
        {
            List<int> res = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                {
                    dict.Add(nums[i], 1);

                }
                else
                {
                    dict[nums[i]]++;
                }
            }
            foreach (var keyValuePair in dict.OrderByDescending(x => x.Value))
            {

                if (k > 0)
                {
                    res.Add(keyValuePair.Key);
                    k--;
                }

            }

            return res.ToArray();
        }



        public bool IsValidSudoku(char[][] board)
        {

            Dictionary<int, HashSet<int>> row = new Dictionary<int, HashSet<int>>();
            Dictionary<int, HashSet<int>> col = new Dictionary<int, HashSet<int>>();
            Dictionary<(int, int), HashSet<int>> box = new Dictionary<(int, int), HashSet<int>>();
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] != '.')
                    {
                        if (row[i].Contains(board[i][j]) || col[j].Contains(board[i][j]) || box[(i / 3, j / 3)].Contains(board[i][j]))
                        {
                            return false;
                        }
                        else
                        {
                            row[i].Add(board[i][j]);
                            row[j].Add(board[i][j]);
                            box[(i / 3, j / 3)].Add(board[i][j]);
                        }
                    }
                }
            }


            return true;

        }

        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        static public int MaxProfit(int[] prices)
        {
            int maxProfit = 0;
            int l = 0; int r = l + 1;
           

            while (r < prices.Length)
            {
               
                if (prices[l] <prices[r])
                {
                    int profit = prices[r] - prices[l];
                    maxProfit = Math.Max(profit, maxProfit);
                    r++;
                  


                }
                else
                {
                    l = r;
                    r++;
                }
               
            }

            return maxProfit;
        }

    }

}
