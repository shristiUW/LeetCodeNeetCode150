using System;
namespace PracticeLeetcode
{
    public class PriorityQueue
    {
        int k;
        private PriorityQueue<int, int> minHeap { get; set; }
       

        public PriorityQueue(int k, int[] nums)
        {
            this.k = k;
            minHeap = new PriorityQueue<int, int>();
            foreach (int num in nums)
            {
                if(minHeap.Count<k)
                {
                    minHeap.Enqueue(num, num);
                    
                }
                else if (minHeap.Peek() >num)
                {
                    minHeap.Dequeue();
                    minHeap.Enqueue(num, num);
                }
             }




        }

        public int Add(int val)
        {
            if(minHeap.Count<k)
            {
                minHeap.Enqueue(val, val);

            }
            else if(minHeap.Peek()<val)
            {
                minHeap.Dequeue();
                minHeap.Enqueue(val, val);
            }
            return minHeap.Peek();
        }
        static public void main()
        {
            int[] nums = new int[] { 3, 2, 1, 5, 6, 4 };
            int k = 2;
            FindKthLargest(nums, k);

        }

       static public int FindKthLargest(int[] nums, int k)
        {
             k = nums.Length - k;
            return quickSelect(0, nums.Length-1);
            int quickSelect(int l, int r)
            {
                int pivot = nums[r];
                int p = l;
                for(int i=l;i<r; i++)
                {
                    if (nums[i]<=pivot)
                    {
                        int temp1 = nums[i];
                        nums[i] = nums[l];
                        nums[l] = temp1;
                        p++;
                    }

                }
                int temp = nums[p];
                nums[p] = nums[r];
                nums[r] = temp;
                if(k<p)
                {
                    return quickSelect(l, p - 1);
                }
                else if(k>p)
                {
                    return quickSelect(p + 1, r);

                }
                else
                {
                    return nums[p];
                }

            }
            


        }


        }


}

