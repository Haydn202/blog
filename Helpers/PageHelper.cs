namespace blog.Helpers;

public static class PageHelper
{
    public static IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
    {
        int midPoint = pageNumber < 3 ? 3
            : pageNumber > pageCount - 2 ? pageCount - 2
            : pageNumber;

        int lower = midPoint - 2;
        int upper = midPoint + 2;
        
        if (pageCount <= 25)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                yield return i;
            }
        }
        else
        {
            if (lower != 1)
            {
                yield return 1;
                if (lower - 1 > 1)
                {
                    yield return -1;
                }
            }
        
            for (int i = lower; i <= upper; i++)
            {
                yield return i;
            }

            if (upper != pageCount)
            {
                if (pageCount - upper > 1)
                {
                    yield return -1;
                }
                yield return pageCount;
            }   
        }
    }
}